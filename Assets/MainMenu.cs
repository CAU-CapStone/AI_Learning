using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public void StartGameButton()
    {
        GameManager.Instance.StartGame();
        gameObject.SetActive(false);
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }
}
