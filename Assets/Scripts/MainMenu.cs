using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public void StartGameButton()
    {
        gameObject.SetActive(false);
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }
}
