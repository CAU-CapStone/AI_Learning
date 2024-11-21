using UnityEngine;

public class PortalInteraction : Interaction
{
    public Transform destPortal;
    
    public override void Activate()
    {
        if (name.Equals("KnnTrigger"))
        {
            if (!GameManager.Instance.isClearKnn)
            {
                return;
            }
            else
            {
                GameManager.Instance.SetQuestText("새로운 마을을 탐험해보자");
                SoundManager.Instance.PlaySoundOneShot("Warp");
                GameManager.Instance.SetPlayerLocation(destPortal,true);
                SoundManager.Instance.ToDTMap();
                return;
            }
        }
        
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

        if (name.Equals("DtTrigger"))
        {
            SoundManager.Instance.PlaySoundOneShot("Warp");
            return;
        }
        
        //발소리 바꾸기 위해 집 안인지 밖인지 구분
        SoundManager.Instance.ChangeFootstep(transform.parent.name.Equals("MapTriggers"));
        SoundManager.Instance.PlaySoundOneShot("DoorOpenSound");
        GameManager.Instance.SetPlayerLocation(destPortal,true);
    }
}