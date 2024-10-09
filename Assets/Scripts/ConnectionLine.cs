using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 연결 시켰을때 나타는 화살표 - 연결선 관련 함수
/// </summary>
public class ConnectionLine : MonoBehaviour
{
    public RectTransform startPoint;  // Outlet
    public RectTransform endPoint;    // Inlet
    public ConnectionManager.ConnectionType currentConnectionType;
    
    void Start()
    {
        currentConnectionType = ConnectionManager.ConnectionType.None;
    }
    
    /// <summary>
    /// 새로운 선 생성
    /// </summary>
    /// <param name="start">Outlet</param>
    /// <param name="end">Inlet</param>
    /// <param name="type">Type of coneection</param>
    public void NewLine(RectTransform start, RectTransform end, ConnectionManager.ConnectionType type)
    {
        currentConnectionType = type;
        startPoint = start;
        endPoint = end;
        KnnExample.Connections.Add(this);
    }

    /// <summary>
    /// outlet과 inlet의 index를 반환
    /// </summary>
    /// <returns>(outlet index,inlet index)</returns>
    public Tuple<int,int> GetIndex()
    {
        return new Tuple<int,int> (startPoint.parent.GetSiblingIndex(), endPoint.parent.GetSiblingIndex());
    }
    
    /// <summary>
    /// 선 지우기 함수. 화살표를 클릭하면 호출된다.
    /// </summary>
    public void RemoveArrow()
    {
        //outlet, inlet의 태그를 다시 설정한다.
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
        //연결 리스트에서 제거하고 화살표를 삭제한다.
        KnnExample.Connections.Remove(this);
        Destroy(gameObject);
    }
}
