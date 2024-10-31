using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDragAndDrop : MonoBehaviour
{
    public Camera puzzleCamera;
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
        puzzleCamera = Camera.main;
    }


    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = puzzleCamera.ScreenPointToRay(Input.mousePosition);
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
            Ray ray = puzzleCamera.ScreenPointToRay(Input.mousePosition);
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
                float largeBasketDistance = Vector3.Distance(selectedObject.transform.position, largeBasket.transform.position);

                if (smallBasketDistance < 2.0f && smallBasket.TryAddItem(selectedObject))
                {
                    Vector3 selectedPosition = selectedObject.transform.position;
                    selectedPosition.y = 0.7f;
                    selectedObject.transform.position = selectedPosition;
                    selectedObject.layer = LayerMask.GetMask("Default");
                    //Debug.Log("�����̸� ���� �ٱ��Ͽ� �־����ϴ�!");
                    SoundManager.Instance.PlaySoundOneShot("SmallStoneSound", 0.5f);
                }
                else if(largeBasketDistance < 2.0f && largeBasket.TryAddItem(selectedObject))
                {
                    Vector3 selectedPosition = selectedObject.transform.position;
                    selectedPosition.y = 0.7f;
                    selectedObject.transform.position = selectedPosition;
                    selectedObject.layer = LayerMask.GetMask("Default");
                    //Debug.Log("�����̸� ū �ٱ��Ͽ� �־����ϴ�!");
                    SoundManager.Instance.PlaySoundOneShot("LargeStoneSound", 0.5f);
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

