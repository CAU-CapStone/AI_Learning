using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Quiz5_EnemyControll : MonoBehaviour
{
    public Quiz5 quiz;

    public Quiz5_GageControll gage;
    public Transform soldier;
    private Animator animator;
    private static readonly int IsAim = Animator.StringToHash("IsAim");
    
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
        quiz.OnQuizRetry += Initialize;
        soldier = transform.Find("Soldier");
        animator = soldier.GetComponent<Animator>();
        Initialize();
    }

    private void Initialize()
    {
        detecting = false;
        animator.SetBool(IsAim, false);
        nowDetectionGage = 0.0f;
        gage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (quiz.isStart && player.isDetectable)
        {
            detecting = (color != player.color &&
                         Vector3.Distance(transform.position, player.transform.position) <= detectionRange);

            if (detecting)
            {
                nowDetectionGage += detectionGageRate * Time.deltaTime;
                
                soldier.LookAt(player.transform);
                animator.SetBool(IsAim, true);

                gage.gameObject.SetActive(true);
                gage.UpdateGage(nowDetectionGage / maxDetectionGage);

                if (nowDetectionGage >= maxDetectionGage)
                {
                    quiz.isStart = false;
                    quiz.failQuiz();
                }
            }
            else
            {
                animator.SetBool(IsAim, false);
                nowDetectionGage = 0.0f;
                gage.gameObject.SetActive(false);
            }
        }
    }
}
