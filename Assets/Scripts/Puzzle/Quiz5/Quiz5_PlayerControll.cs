using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiz5_PlayerControll : MonoBehaviour
{
    public Quiz5 quiz;

    public bool isDetectable = false;
    
    [Header("Colors")]
    public Quiz5Color color = Quiz5Color.None;

    public GameObject originForm;
    private Animator originFormAnimator;
    private static readonly int IsMove = Animator.StringToHash("IsMove");
    public GameObject redForm;
    public GameObject greenForm;
    public GameObject blueForm;
    
    [Header("positions")]
    public Transform startPosition;
    public Transform endPosition;
    public List<Transform> movePoints = new();
    private Transform nowDest;
    
    public int currentPoint = 0;
    public float speed = 0.3f;
    
    // Start is called before the first frame update
    void Start()
    {
        quiz = QuizDictionary.Instance.GetQuiz("Quiz5") as Quiz5;
        quiz.OnQuizRetry += Initialize;
        
        originFormAnimator = originForm.GetComponent<Animator>();
        
        Initialize();
    }

    private void Initialize()
    {
        isDetectable = false;
        transform.position = startPosition.position;
        transform.rotation = Quaternion.identity;
        nowDest = movePoints[0];
        currentPoint = 0;
        ChangeColorToOrigin();
    }

    // Update is called once per frame
    void Update()
    {
        originFormAnimator.SetBool(IsMove, quiz.isStart);
        
        if (quiz.isStart)
        {
            isDetectable = currentPoint > 0 && currentPoint < movePoints.Count;
        
            if (currentPoint > movePoints.Count)
            {
                quiz.isStart = false;
                quiz.endQuiz();
            }
            else if (Vector3.Distance(transform.position, nowDest.position) < 0.1f)
            {
                currentPoint++;
                nowDest = currentPoint < movePoints.Count ? movePoints[currentPoint] : endPosition;
            }
            else
            {   
                transform.position = Vector3.MoveTowards(transform.position, nowDest.position, speed * Time.deltaTime);
                transform.LookAt(nowDest);
            }
        }
    }

    public void ChangeColorToOrigin()
    {
        color = Quiz5Color.None;
        DisableAllForm();
        originForm.SetActive(true);
    }
    
    public void ChangeColorToRed()
    {
        color = Quiz5Color.Red;
        DisableAllForm();
        redForm.SetActive(true);
    }
    
    public void ChangeColorToGreen()
    {
        color = Quiz5Color.Green;
        DisableAllForm();
        greenForm.SetActive(true);
    }
    
    public void ChangeColorToBlue()
    {
        color = Quiz5Color.Blue;
        DisableAllForm();
        blueForm.SetActive(true);
    }

    private void DisableAllForm()
    {
        originForm.SetActive(false);
        redForm.SetActive(false);
        greenForm.SetActive(false);
        blueForm.SetActive(false);
    }
}
