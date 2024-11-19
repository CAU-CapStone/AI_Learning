using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LightBulb : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10f;

    private void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
    /*void Start()
    {
        StartCoroutine(Spin());
    }

    IEnumerator Spin()
    {
        while (true)
        {
            float rotation = 0;
            while (rotation<720)
            {
                rotation += rotationSpeed * Time.deltaTime;
                transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
            }
            yield return new WaitForSeconds(1f);
        }
    }*/
}
