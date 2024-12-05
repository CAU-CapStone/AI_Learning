using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DTQuiz1UIHandler : MonoBehaviour
{
    public DTQuiz1_New dtQuiz1;

    public Button submitQuiz;
    public Button retryButton;
    public Button finishButton;

    public TextMeshProUGUI resultText;
    public GameObject resultUI;
    private void Awake()
    {
        submitQuiz?.onClick.AddListener(OnSubmitButtonClicked);
        retryButton?.onClick.AddListener(OnRetryButtonClicked);
        finishButton?.onClick.AddListener(OnFinishButtonClicked);
    }


    public void OnSubmitButtonClicked()
    {
        bool quizResult = dtQuiz1.checkQuizResult();
        if (quizResult)
        {
            resultText.text = "정답입니다!";
            finishButton.gameObject.SetActive(true);
            retryButton.gameObject.SetActive(false);
            resultUI.SetActive(true);
        }
        else
        {
            resultText.text = "오답입니다!";
            finishButton.gameObject.SetActive(false);
            retryButton.gameObject.SetActive(true);
            resultUI.SetActive(true);
        }
        submitQuiz.gameObject.SetActive(false);
    }

    public void OnFinishButtonClicked()
    {
        resultUI.SetActive(false);
        dtQuiz1.endQuiz();
    }

    public void OnRetryButtonClicked()
    {
        dtQuiz1.resetQuiz();
    }

    public void resetUI()
    {
        resultText.text = "���� or ����";
        finishButton.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        resultUI.SetActive(false);
        submitQuiz.gameObject.SetActive(true);
    }
}
