using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConnectionManager : MonoBehaviour
{
   public RectTransform linePrefab;    // Prefab for the line (Image)
    public GraphicRaycaster raycaster;  // Reference to the Canvas' GraphicRaycaster
    public Canvas canvas;               // Reference to the Canvas
    private bool isDragging = false;    // Track whether dragging is in progress
    private RectTransform currentLine;  // The line currently being drawn
    private RectTransform outlet;       // The outlet where dragging started
    private Vector3 startMousePosition; // Start position of the mouse
    public EventSystem eventSystem;    // Reference to the EventSystem
    private ConnectionType currentConnectionType;
    
    public enum ConnectionType
    {
        None,
        Data,
        Prediction
    }

    void Start()
    {
        currentConnectionType = ConnectionType.None;
        // Assign these manually or through the Inspector
        if (raycaster == null)
            raycaster = FindObjectOfType<GraphicRaycaster>();

        if (eventSystem == null)
            eventSystem = FindObjectOfType<EventSystem>();
    }
    
    void Update()
    {
        if(!isDragging && Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }
        if (isDragging && currentLine != null)
        {
            // Continuously update the line position while dragging
            Vector3 cursorPosition = Input.mousePosition;
            DrawLine(outlet.position, cursorPosition);
        }

        // Detect when the mouse button is released
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            OnMouseUp();
        }
    }

    // When an Outlet is clicked, start dragging
    public void OnOutletClick(RectTransform outletTransform)
    {
        outlet = outletTransform;
        isDragging = true;

        // Instantiate the line prefab and attach it to the Canvas
        currentLine = Instantiate(linePrefab, canvas.transform);
        currentLine.gameObject.SetActive(true);

        // Initialize the line's position
        DrawLine(outlet.position, outlet.position);
    }

    // Handle mouse button release (stop dragging)
    public void OnMouseUp()
    {
        isDragging = false;

        // Use GraphicRaycaster to check if the mouse is over an Inlet
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("DataInlet") && currentConnectionType == ConnectionType.Data)
            {
                // Successfully connected to an Inlet
                RectTransform inlet = result.gameObject.GetComponent<RectTransform>();
                DrawLine(outlet.position, inlet.position);  // Finalize the connection
                /*ConnectionLine connectionLine = currentLine.GetComponent<ConnectionLine>();
                connectionLine.startPoint = outlet;
                connectionLine.endPoint = inlet;
                connectionLine.currentConnectionType = ConnectionType.Data;*/
                Debug.Log("Connected to: " + result.gameObject.name);
                outlet.gameObject.tag = "Untagged";  // Reset the Outlet tag
                return;
            }
            else if (result.gameObject.CompareTag("PredictionInlet") && currentConnectionType == ConnectionType.Prediction)
            {
                // Successfully connected to an Inlet
                RectTransform inlet = result.gameObject.GetComponent<RectTransform>();
                DrawLine(outlet.position, inlet.position);  // Finalize the connection
                /*ConnectionLine connectionLine = currentLine.GetComponent<ConnectionLine>();
                connectionLine.startPoint = outlet;
                connectionLine.endPoint = inlet;
                connectionLine.currentConnectionType = ConnectionType.Prediction;*/
                Debug.Log("Connected to: " + result.gameObject.name);
                outlet.gameObject.tag = "Untagged";  // Reset the Outlet tag
                return;
            }
        }

        // If not dropped on an Inlet, destroy the line
        Destroy(currentLine.gameObject);
        Debug.Log("No valid Inlet found, line removed.");
    }

    // Function to draw a line (using a UI Image) between two points
    private void DrawLine(Vector3 start, Vector3 end)
    {
        //currentLine.GetComponent<ConnectionLine>().DrawArrow(start, end);
        Vector3 direction = end - start;
        float distance = direction.magnitude;

        // Set the position of the line to be the midpoint between the start and end points
        currentLine.position = (start + end) / 2;

        // Set the width of the line based on the distance between the two points
        currentLine.sizeDelta = new Vector2(distance, currentLine.sizeDelta.y);

        // Rotate the line to point from the start to the end
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        currentLine.rotation = Quaternion.Euler(0, 0, angle);
    }

    // Detect clicks on Outlets using GraphicRaycaster
    public void OnMouseDown()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        Debug.Log("Raycast hit " + results.Count + " UI elements.");
        foreach (RaycastResult result in results)
        {
            Debug.Log("Hit UI Element: " + result.gameObject.name);
            if (result.gameObject.CompareTag("DataOutlet"))
            {
                currentConnectionType = ConnectionType.Data;
                Debug.Log("Outlet clicked: " + result.gameObject.name);
                RectTransform outletRect = result.gameObject.GetComponent<RectTransform>();
                OnOutletClick(outletRect);  // Start the drag from the Outlet
                return;
            }
            else if (result.gameObject.CompareTag("PredictionOutlet"))
            {
                currentConnectionType = ConnectionType.Prediction;
                Debug.Log("Outlet clicked: " + result.gameObject.name);
                RectTransform outletRect = result.gameObject.GetComponent<RectTransform>();
                OnOutletClick(outletRect);  // Start the drag from the Outlet
                return;
            }
            else if (result.gameObject.CompareTag("Arrow"))
            {
                result.gameObject.GetComponent<ConnectionLine>().RemoveArrow();
                return;
            }
        }
    }
}