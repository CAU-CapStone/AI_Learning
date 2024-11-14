using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiz5_GageControll : MonoBehaviour
{
    public Camera puzzleCamera;
    
    void Start()
    {
        Vector3 targetPosition = puzzleCamera.transform.position;
        targetPosition.x = transform.position.x;
        transform.LookAt(targetPosition);
        transform.eulerAngles += new Vector3(180, 0, 0);
    }
}
