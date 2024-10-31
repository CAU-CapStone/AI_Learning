using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalInteraction : Interaction
{
    public Transform destPortal;
    
    public override void Activate()
    {
        if (name.Equals("DoorTrigger1") && !GameManager.Instance.isReadBook)
        {
            DialogueManager.Instance.SetDialogue(0, 0);
            return;
        }

        if (name.Equals("House3Trigger"))
        {
            DialogueManager.Instance.SetDialogue(8, 8);
            return;
        }
        
        if (name.Equals("House4Trigger") && !GameManager.Instance.isClearPuzzle2)
        {
            DialogueManager.Instance.SetDialogue(8, 8);
            return;
        }
        SoundManager.Instance.PlaySoundOneShot("DoorOpenSound");
        GameManager.Instance.SetPlayerLocation(destPortal.position, destPortal.rotation);
    }
}
