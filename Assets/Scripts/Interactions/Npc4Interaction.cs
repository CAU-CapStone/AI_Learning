using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc4Interaction : Interaction
{
    public override void Activate()
    {
        DialogueManager.Instance.SetDialogue(33, 33);
    }
}
