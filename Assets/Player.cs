using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform _currentTrigger;
    void Start()
    {
        _currentTrigger = null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            _currentTrigger = other.transform;
        }
        else if (other.CompareTag("Portal"))
        {
            _currentTrigger = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == _currentTrigger)
        {
            _currentTrigger = null;
        }
    }
}
