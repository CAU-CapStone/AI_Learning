using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiz5_GageControll : MonoBehaviour
{
    public Camera puzzleCamera;

    private Transform gageBar;
    private Transform mark;
    
    void Start()
    {
        puzzleCamera = CameraManager.Instance.puzzleCamera;
        mark = transform.Find("Exclamation_Up");
        gageBar = transform.Find("Quad");
        
        Vector3 targetPosition = puzzleCamera.transform.position;
        targetPosition.x = transform.position.x;
        transform.LookAt(targetPosition);
        transform.eulerAngles += new Vector3(180, 0, 0);

        mark.transform.eulerAngles += new Vector3(180, 0, 0);
    }

    public void UpdateGage(float percent)
    {
        gageBar.localScale = new Vector3(percent, 0.9f, 1);
        gageBar.localPosition = new Vector3(0.45f * (1 - percent), 0, -0.1f);
    }
}
