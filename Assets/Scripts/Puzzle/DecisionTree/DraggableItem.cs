using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 originalPosition;
    public Transform correctArea;

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        Ray ray = CameraManager.Instance.puzzleCamera.ScreenPointToRay(eventData.position);
        Plane xzPlane = new Plane(Vector3.up, Vector3.zero);
        float distance;

        if (xzPlane.Raycast(ray, out distance))
        {
            Vector3 point = ray.GetPoint(distance);
            transform.position = new Vector3(point.x, originalPosition.y, point.z);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (IsInCorrectArea())
        {
            transform.position = new Vector3(correctArea.position.x, originalPosition.y, correctArea.position.z);
        }
        else
        {
            transform.position = originalPosition;
        }
    }

    private bool IsInCorrectArea()
    {
        float distance = Vector3.Distance(transform.position, correctArea.position);
        return distance < 1.0f;
    }
}