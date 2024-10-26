using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public virtual void Activate()
    {
        Debug.Log(gameObject.name + " activated");
    }
}
