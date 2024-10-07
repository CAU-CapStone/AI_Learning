using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionLine : MonoBehaviour
{
    public RectTransform startPoint;  // Outlet
    public RectTransform endPoint;    // Inlet
    public ConnectionManager.ConnectionType currentConnectionType;
    
    void Start()
    {
        currentConnectionType = ConnectionManager.ConnectionType.None;
    }
    
    public void NewLine(RectTransform start, RectTransform end, ConnectionManager.ConnectionType type)
    {
        currentConnectionType = type;
        startPoint = start;
        endPoint = end;
        KnnExample.Connections.Add(this);
    }

    public Tuple<int,int> GetIndex()
    {
        return new Tuple<int,int> (startPoint.parent.GetSiblingIndex(), endPoint.parent.GetSiblingIndex());
    }
    
    //선 지우기
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

        KnnExample.Connections.Remove(this);
        Destroy(gameObject);
    }
}
