using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Animator animator;
        
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if( animator != null )
        {
            if(animator.enabled == false)
            {
                animator.enabled = true;
            }
            //animator.Play("Transition",0,0);   
            animator.SetBool("Hover",true);
        }            
    }
        
    public void OnPointerExit(PointerEventData eventData)
    {
        Unhighlight();
    }

    public void Unhighlight()
    {
        if( animator != null )
        {
            if(animator.enabled == false)
            {
                animator.enabled = true;
            }
            //animator.Play("Transition",0,0);   
            animator.SetBool("Hover",false);
        }
    }
}
