using UnityEngine;

public class DT_PortalInteraction : Interaction
{
    public Transform destPortal;
    
    public override void Activate()
    {
        base.Activate();
        if (name.Equals("DoorTrigger1") && !GameManager.Instance.isReadBook)
        {
            DialogueManager.Instance.SetDialogue(0, 0);
            return;
        }

        if (name.Equals("House3Trigger") || name.Equals("House4Trigger"))
        {
            DialogueManager.Instance.SetDialogue(8, 8);
            return;
        }
        
        SoundManager.Instance.PlaySoundOneShot("DoorOpenSound");
        GameManager.Instance.SetPlayerLocation(destPortal,true);
    }
}