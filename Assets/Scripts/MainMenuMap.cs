using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMap : MonoBehaviour
{
    private bool isTutorialDialogueDone = false;

    void Start()
    {
        //TODO:: 대사 추가 후 대사 완료하면 isDialogueDone을 true로 변경. Start() 삭제
        isTutorialDialogueDone = true;
    }

    void OnInteract()
    {
        if (isTutorialDialogueDone)
        {
            //go to k-nn scene
            SceneManager.LoadScene(1);
        }
    }
}
