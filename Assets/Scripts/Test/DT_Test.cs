using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DT_Test : Interaction
{
    public Transform pos;
    
    public override void Activate()
    {
        GameManager.Instance.isReadBook = true;
        GameManager.Instance.isClearPuzzle1 = true;
        GameManager.Instance.isClearPuzzle2 = true;
        GameManager.Instance.isClearPuzzle3 = true;
        GameManager.Instance.isClearPuzzle4 = true;
        GameManager.Instance.isClearPuzzle5 = true;
        GameManager.Instance.isClearKnn = true;
        
        GameManager.Instance.SetNpcLightBulbActive(GameManager.Instance.npc1, false);
        GameManager.Instance.SetNpcLightBulbActive(GameManager.Instance.npc2, false);
        GameManager.Instance.SetNpcLightBulbActive(GameManager.Instance.npc3, false);
        GameManager.Instance.SetNpcLightBulbActive(GameManager.Instance.npc4, false);

        GameManager.Instance.frontBookPortal.SetActive(false);
        GameManager.Instance.house1InPortal.SetActive(false);
        GameManager.Instance.house2InPortal.SetActive(false);
        GameManager.Instance.house2OutPortal.SetActive(false);
        GameManager.Instance.house4InPortal.SetActive(false);
        GameManager.Instance.house4OutPortal.SetActive(false);
        GameManager.Instance.npc1Portal.SetActive(false);
        GameManager.Instance.npc2Portal.SetActive(false);
        GameManager.Instance.npc3Portal.SetActive(false);
        GameManager.Instance.npc4Portal.SetActive(false);
        GameManager.Instance.npc2Portal2.SetActive(false);
        GameManager.Instance.knnPortal.SetActive(true);
        
        GameManager.Instance.npc3.SetActive(true);
        GameManager.Instance.SetGameObjectLocation(GameManager.Instance.npc2, GameManager.Instance.npc2TeleportPosition);
        
        SoundManager.Instance.ChangeFootstep(false);
        GameManager.Instance.SetQuestText("새로운 마을을 탐험해보자");
        SoundManager.Instance.PlaySoundOneShot("Warp");
        GameManager.Instance.SetPlayerLocation(pos, true);
        SoundManager.Instance.ToDTMap();
    }
}
