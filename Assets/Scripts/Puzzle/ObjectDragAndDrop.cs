using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDragAndDrop : MonoBehaviour
{
    private Camera mainCamera;
    private GameObject selectedObject;
    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 initialPosition;

    public LayerMask dragLayerMask;

    public QuizBasket smallBasket;
    public QuizBasket largeBasket;

    private Plane dragPlane;

    void Start()
    {
        mainCamera = Camera.main;
    }


    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, dragLayerMask))
            {
                if (hit.collider != null)
                {
                    selectedObject = hit.collider.gameObject;
                    initialPosition = selectedObject.transform.position;
                    isDragging = true;
                    dragPlane = new Plane(Vector3.up, selectedObject.transform.position);

                    float distance;
                    if(dragPlane.Raycast(ray,out distance))
                    {
                        Vector3 hitPoint = ray.GetPoint(distance);
                        offset = selectedObject.transform.position - hitPoint;
                    }
                }
            }
        }

        if (isDragging)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            float distance;

            if (dragPlane.Raycast(ray, out distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);
                Vector3 newPosition = hitPoint- offset;

                newPosition.y = selectedObject.transform.position.y;
                selectedObject.transform.position = newPosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;

                
                float smallBasketDistance = Vector3.Distance(selectedObject.transform.position, smallBasket.transform.position);
                Debug.Log("작은 바구니 사이 거리: "+smallBasketDistance);
                float largeBasketDistance = Vector3.Distance(selectedObject.transform.position, largeBasket.transform.position);
                Debug.Log("큰 바구니 사이 거리: " + largeBasketDistance);

                if (smallBasketDistance < 2.0f && smallBasket.TryAddItem(selectedObject))
                {                    
                    Debug.Log("돌멩이를 작은 바구니에 넣었습니다!");
                }
                else if(largeBasketDistance < 2.0f && largeBasket.TryAddItem(selectedObject))
                {
                    Debug.Log("돌멩이를 큰 바구니에 넣었습니다!");
                }
                else
                {
                    selectedObject.transform.position = initialPosition;
                }
                


                selectedObject = null;
            }
        }
    }

}

