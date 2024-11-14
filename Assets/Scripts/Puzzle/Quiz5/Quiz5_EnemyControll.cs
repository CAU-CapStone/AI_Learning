using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiz5_EnemyControll : MonoBehaviour
{
    public Quiz5 quiz;

    public GameObject gage;
    
    public Quiz5Color color;
    public float detectionRange = 1.0f;
    
    public Quiz5_PlayerControll player;
    
    public bool detecting = false;

    public float nowDetectionGage = 0.0f;
    public float detectionGageRate = 0.4f;
    public float maxDetectionGage = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        quiz = QuizDictionary.Instance.GetQuiz("Quiz5") as Quiz5;
    }

    // Update is called once per frame
    void Update()
    {
        detecting = (color != player.color &&
                     Vector3.Distance(transform.position, player.transform.position) <= detectionRange);
        
        if (detecting)
        {
            nowDetectionGage += detectionGageRate * Time.deltaTime;

            gage.SetActive(true);
            gage.transform.localScale = new Vector3(nowDetectionGage / maxDetectionGage, 0.5f, 1);

            if (nowDetectionGage >= maxDetectionGage)
            {
                quiz.failQuiz();
            }
        }
        else
        {
            nowDetectionGage = 0.0f;
            
            gage.SetActive(false);
        }
    }
}
