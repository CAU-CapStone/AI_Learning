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
    public GameObject quiz4;
    public GameObject quiz5;
    public GameObject dtquiz1;
    public GameObject dtquiz2;

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
        if (quiz4 != null) quizDictionary["Quiz4"] = quiz4.GetComponent<IQuiz>();
        if (quiz5 != null) quizDictionary["Quiz5"] = quiz5.GetComponent<IQuiz>();
        if (dtquiz1 != null) quizDictionary["DTQuiz1"] = dtquiz1.GetComponent<IQuiz>();
        if (dtquiz2 != null) quizDictionary["DTQuiz2"] = dtquiz2.GetComponent<IQuiz>();
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
