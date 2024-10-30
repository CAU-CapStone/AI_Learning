using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControll : MonoBehaviour
{
    public Camera camera;
    public float speed = 5f;
    public float rotationSpeed = 1f;
    
    private CharacterController controller;
    private Vector2 moveDirection = Vector2.zero;
    private Vector3 lookDirection = Vector3.zero;

    private Player _player;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.isAllowedToMove == false)
        {
            return;
        }
        controller.transform.Rotate(lookDirection);
        lookDirection = Vector3.zero;
        
        var move = transform.right * moveDirection.x + transform.forward * moveDirection.y;
        controller.Move(speed * Time.deltaTime * move);
        
        //SetCameraPosition();
    }

    void OnMove(InputValue value)
    {
        Vector2 value2 = value.Get<Vector2>();
        //움직일때는 커서 보이지 않게 하기
        if (value2.magnitude > 0)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
        moveDirection = value2;
    }

    void OnRoll(InputValue value)
    {
        //대화 중에는 움직이기 방지
        if(DialogueManager.Instance.isDialogue)
        {
            return;
        }
        Vector2 value2 = value.Get<Vector2>();
        lookDirection += new Vector3(0, value2.x, 0) * rotationSpeed;
    }
    
    void OnInteract()
    {
        if(DialogueManager.Instance.isDialogue)
        {
            return;
        }
        Transform t = _player._currentTrigger;
        if (t != null)
        {
            if(t.CompareTag("NPC"))
            {
                Debug.Log("Interacting with NPC");
                t.GetComponent<Interaction>().Activate();
            }
            else if(t.CompareTag("Portal"))
            {
                Debug.Log("Interacting with Portal");
                t.GetComponent<Interaction>().Activate();
            }
        }
    }

    void OnDialogue(InputValue value)
    {
        if(DialogueManager.Instance.isDialogue)
        {
            DialogueManager.Instance.ShowNextDialogue();
        }
    }

    //카메라 위치 조정
    Vector3 _delta = new Vector3(0.0f, 5.0f, -1.5f);
    void SetCameraPosition()
    {
        camera.transform.position = transform.position - transform.forward * 5 + transform.up * 1.5f;
        camera.transform.LookAt(transform.position + transform.up * 1.5f);
    }
}
