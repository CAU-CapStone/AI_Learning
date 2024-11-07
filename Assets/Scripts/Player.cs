using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform _currentTrigger;
    TMP_Text _interactionText;
    const string _NPC_INTERACTION = "Shift를 눌러 대화";
    const string _PORTAL_INTERACTION = "Shift를 눌러 이동";

    void Awake()
    {
        _currentTrigger = null;
        _interactionText = GameObject.Find("InteractionText").GetComponentInChildren<TMP_Text>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(GameManager.Instance.isAllowedToMove == false)
        {
            return;
        }
        if (other.CompareTag("NPC"))
        {
            _currentTrigger = other.transform;
            _interactionText.text = _NPC_INTERACTION;
        }
        else if (other.CompareTag("Portal"))
        {
            _currentTrigger = other.transform;
            _interactionText.text = _PORTAL_INTERACTION;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == _currentTrigger)
        {
            _currentTrigger = null;
            _interactionText.text = "";
        }
    }
}
