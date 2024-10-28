using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizDictionary : MonoBehaviour
{
    public static QuizDictionary Instance { get; private set; }

    private Dictionary<string, IQuiz> quizDictionary = new Dictionary<string, IQuiz>();

    [Header("퀴즈 오브젝트")]
    public GameObject quiz1;
    public GameObject quiz2;
    public GameObject quiz3;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        InitializeQuizzes();
    }

    private void InitializeQuizzes()
    {
        if (quiz1 != null) quizDictionary["Quiz1"] = quiz1.GetComponent<IQuiz>();
        if (quiz2 != null) quizDictionary["Quiz2"] = quiz2.GetComponent<IQuiz>();
        if (quiz3 != null) quizDictionary["Quiz3"] = quiz3.GetComponent<IQuiz>();
    }

    public IQuiz GetQuiz(string quizName)
    {
        if (quizDictionary.TryGetValue(quizName, out IQuiz quiz))
        {
            return quiz;
        }
        Debug.LogWarning($"Quiz '{quizName}' not found.");
        return null;
    }
}
