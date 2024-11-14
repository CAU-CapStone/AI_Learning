using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiz5_PlayerControll : MonoBehaviour
{
    public Quiz5 quiz;

    public bool isDetectable = false;
    
    [Header("Colors")]
    public Quiz5Color color = Quiz5Color.None;
    public Material redMaterial;
    public Material greenMaterial;
    public Material blueMaterial;
    
    [Header("positions")]
    public Transform startPosition;
    public Transform endPosition;
    public List<Transform> movePoints = new();
    
    public int currentPoint = 0;
    public float speed = 0.3f;
    
    // Start is called before the first frame update
    void Start()
    {
        quiz = QuizDictionary.Instance.GetQuiz("Quiz5") as Quiz5;
        transform.position = startPosition.position;
    }

    // Update is called once per frame
    void Update()
    {
        isDetectable = currentPoint > 0 && currentPoint < movePoints.Count;
        
        if (currentPoint > movePoints.Count)
        {
            quiz.endQuiz();
        }
        else if (currentPoint == movePoints.Count)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition.position, speed * Time.deltaTime);
        }
        else if (Vector3.Distance(transform.position, movePoints[currentPoint].position) < 0.1f)
        {
            currentPoint++;
        }
        else
        {   
            transform.position = Vector3.MoveTowards(transform.position, movePoints[currentPoint].position, speed * Time.deltaTime);
        }
    }

    public void ChangeColorToRed()
    {
        color = Quiz5Color.Red;
        GetComponent<Renderer>().material = redMaterial;
    }
    
    public void ChangeColorToGreen()
    {
        color = Quiz5Color.Green;
        GetComponent<Renderer>().material = greenMaterial;
    }
    
    public void ChangeColorToBlue()
    {
        color = Quiz5Color.Blue;
        GetComponent<Renderer>().material = blueMaterial;
    }
}
