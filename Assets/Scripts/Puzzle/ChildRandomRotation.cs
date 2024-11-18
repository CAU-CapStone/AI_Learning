using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildRandomRotation : MonoBehaviour
{
    private void OnEnable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;

            float randomX = child.transform.eulerAngles.x;
            float randomY = Random.Range(0f, 360f);
            float randomZ = Random.Range(0f, 360f);
            child.transform.rotation = Quaternion.Euler(randomX, randomY, randomZ);
        }
    }
}
