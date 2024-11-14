using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quiz4UIHandler : MonoBehaviour
{
    public GameObject ColorButton;
    public Button redButton;
    public Button greenButton;
    public Button blueButton;

    public GameObject ResultUI;
    public Button retryButton;
    public Button finishButton;
    public TextMeshProUGUI answerMessage;

    public Quiz4House house;
    public FindNearestHouse findNearestHouse;
    public Quiz4 quiz4;

    private void Awake()
    {

        redButton?.onClick.AddListener(()=> changeHouseColor(Color.red));
        greenButton?.onClick.AddListener(()=> changeHouseColor(Color.green));
        blueButton?.onClick.AddListener(()=> changeHouseColor(Color.blue));

        retryButton?.onClick.AddListener(retryQuiz);
        finishButton?.onClick.AddListener(finishQuiz);
        
    }

    public void showColorButton()
    {
        ColorButton.SetActive(true);
    }

    public void showResultUI(bool correct)
    {
        ResultUI.SetActive(true);
        if(correct)
        {
            answerMessage.text = "Correct!";
        }
        else
        {
            answerMessage.text = "Wrong!";
        }
    }

    public void changeHouseColor(Color newColor)
    {
        house.submitAnswerColor(newColor);
        ColorButton.SetActive(false);
    }

    private void setChildActive(bool active)
    {
        ColorButton.SetActive(active);
        ResultUI.SetActive(active);
    }

    public void retryQuiz()
    {
        findNearestHouse.initNearestHouse();
        ResultUI.SetActive(false);
    }

    public void finishQuiz()
    {
        quiz4.endQuiz();
        ResultUI.SetActive(false);
    }

}
