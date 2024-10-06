using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionLine : MonoBehaviour
{
    public RectTransform startPoint;  // Outlet
    public RectTransform endPoint;    // Inlet
    public LineRenderer lineRenderer;
    public ConnectionManager.ConnectionType currentConnectionType;
    
    void Start()
    {
        currentConnectionType = ConnectionManager.ConnectionType.None;
        // Assign these manually or through the Inspector
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();
    }

    // Call this method when an arrow connection is made
    public void DrawArrow()
    {
        Vector3 start = startPoint.position;  // Start point (Outlet)
        Vector3 end = endPoint.position;      // End point (Inlet)

        // Create two points for the line: (Outlet -> Vertical Alignment -> Inlet)
        Vector3 middlePoint = new Vector3(start.x, end.y, 0);  // Align vertically with the inlet

        // Set the positions for the line renderer
        lineRenderer.positionCount = 3;  // Three points for the two-segment line
        lineRenderer.SetPosition(0, start);        // Start point (Outlet)
        lineRenderer.SetPosition(1, middlePoint);  // Vertical alignment point
        lineRenderer.SetPosition(2, end);          // End point (Inlet)
    }
    public void DrawArrow(Vector3 start, Vector3 end)
    {
        Vector3 middlePoint = new Vector3(start.x, end.y, 0);  // Align vertically with the inlet

        // Set the positions for the line renderer
        lineRenderer.positionCount = 3;  // Three points for the two-segment line
        lineRenderer.SetPosition(0, start);        // Start point (Outlet)
        lineRenderer.SetPosition(1, middlePoint);  // Vertical alignment point
        lineRenderer.SetPosition(2, end);          // End point (Inlet)
    }
    // Call this method to hide the arrow
    public void HideArrow()
    {
        lineRenderer.enabled = false;
    }

    public void RemoveArrow()
    {
        if (currentConnectionType == ConnectionManager.ConnectionType.Data)
        {
            startPoint.gameObject.tag = "DataOutlet";
            endPoint.gameObject.tag = "DataInlet";
        }
        else if (currentConnectionType == ConnectionManager.ConnectionType.Prediction)
        {
            startPoint.gameObject.tag = "PredictionOutlet";
            endPoint.gameObject.tag = "PredictionInlet";
        }
        Destroy(gameObject);
    }
}
