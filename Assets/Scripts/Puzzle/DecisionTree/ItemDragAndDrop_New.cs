using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDragAndDrop_New : MonoBehaviour
{
    private GameObject selectedObject;
    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 initialPosition;

    public LayerMask dragLayerMask;

    private Plane dragPlane;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = CameraManager.Instance.puzzleCamera.ScreenPointToRay(Input.mousePosition);
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
                    if (dragPlane.Raycast(ray, out distance))
                    {
                        Vector3 hitPoint = ray.GetPoint(distance);
                        offset = selectedObject.transform.position - hitPoint;
                    }
                }
            }
        }

        if (isDragging)
        {
            Ray ray = CameraManager.Instance.puzzleCamera.ScreenPointToRay(Input.mousePosition);
            float distance;

            if (dragPlane.Raycast(ray, out distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);
                Vector3 newPosition = hitPoint + offset;

                newPosition.y = selectedObject.transform.position.y;
                selectedObject.transform.position = newPosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                selectedObject = null;
            }
        }
    }

}
