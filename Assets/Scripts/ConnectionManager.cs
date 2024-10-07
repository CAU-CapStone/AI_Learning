using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConnectionManager : MonoBehaviour
{
   public RectTransform linePrefab;    
    public GraphicRaycaster raycaster;  
    public Canvas canvas;               
    private bool isDragging = false;    
    private RectTransform currentLine;  
    private RectTransform outlet;     
    private Vector3 startMousePosition; 
    public EventSystem eventSystem;
    private ConnectionType currentConnectionType;

    //연결 타입
    public enum ConnectionType
    {
        None,
        Data,
        Prediction
    }

    void Start()
    {
        currentConnectionType = ConnectionType.None;
        //레이캐스터가 없으면 찾아서 할당
        if (raycaster == null)
            raycaster = FindObjectOfType<GraphicRaycaster>();

        //캔버스가 없으면 찾아서 할당
        if (eventSystem == null)
            eventSystem = FindObjectOfType<EventSystem>();
    }
    
    void Update()
    {
        //처음 누르기 시작하면 OnMouseDown 호출
        if(!isDragging && Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }
        if (isDragging && currentLine != null)
        {
            //드래그 중일 때 선 그리기
            Vector3 cursorPosition = Input.mousePosition;
            DrawLine(outlet.position, cursorPosition);
        }

        // 마우스 버튼 떼면 드래그 종료
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            OnMouseUp();
        }
    }

    //아웃렛 클릭시 호출
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

    //마우스 떼면 호출
    public void OnMouseUp()
    {
        isDragging = false;

        // 포인터가 inlet에 있는지 확인
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        foreach (RaycastResult result in results)
        {
            //data inlet에 드랍했을 때
            if (result.gameObject.CompareTag("DataInlet") && currentConnectionType == ConnectionType.Data)
            {
                RectTransform inlet = result.gameObject.GetComponent<RectTransform>();
                DrawLine(outlet.position, inlet.position); // 선 그리기 (확정)
                ConnectionLine connectionLine = currentLine.GetComponent<ConnectionLine>();
                connectionLine.NewLine(outlet, inlet, ConnectionType.Data);
                Debug.Log("Connected to: " + result.gameObject.name);
                outlet.gameObject.tag = "Untagged";// tag reset
                inlet.gameObject.tag = "Untagged"; // tag reset
                return;
            }
            //prediction inlet에 드랍했을 때
            else if (result.gameObject.CompareTag("PredictionInlet") && currentConnectionType == ConnectionType.Prediction)
            {
                RectTransform inlet = result.gameObject.GetComponent<RectTransform>();
                DrawLine(outlet.position, inlet.position);  // 선 그리기 (확정)
                ConnectionLine connectionLine = currentLine.GetComponent<ConnectionLine>();
                connectionLine.NewLine(outlet, inlet, ConnectionType.Prediction);
                Debug.Log("Connected to: " + result.gameObject.name);
                outlet.gameObject.tag = "Untagged";  // tag reset
                inlet.gameObject.tag = "Untagged"; // tag reset
                return;
            }
        }

        // Inlet에 드랍하지 않았을 때, 삭제
        Destroy(currentLine.gameObject);
        Debug.Log("No valid Inlet found, line removed.");
    }

    //선 그리기
    private void DrawLine(Vector3 start, Vector3 end)
    {
        Vector3 direction = end - start;
        float distance = direction.magnitude;
        //선 위치 조정
        currentLine.position = (start + end) / 2;
        
        //사이즈 조정
        currentLine.sizeDelta = new Vector2(distance, currentLine.sizeDelta.y);
        //방향 조정
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        currentLine.rotation = Quaternion.Euler(0, 0, angle);
    }

    //마우스 누르면 호출
    public void OnMouseDown()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);
        foreach (RaycastResult result in results)
        {
            Debug.Log("Hit UI Element: " + result.gameObject.name);
            if (result.gameObject.CompareTag("DataOutlet"))
            {
                currentConnectionType = ConnectionType.Data;
                RectTransform outletRect = result.gameObject.GetComponent<RectTransform>();
                OnOutletClick(outletRect);  // outlet에서 드래그 시작
                return;
            }
            else if (result.gameObject.CompareTag("PredictionOutlet"))
            {
                currentConnectionType = ConnectionType.Prediction;
                RectTransform outletRect = result.gameObject.GetComponent<RectTransform>();
                OnOutletClick(outletRect);  // outlet에서 드래그 시작
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