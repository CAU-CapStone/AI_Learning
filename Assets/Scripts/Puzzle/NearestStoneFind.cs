using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NearestStoneFind : MonoBehaviour
{
    public Quiz3 quiz3;

    private List<GameObject> stones = new List<GameObject>();
    private List<GameObject> nearestStones;
    public GameObject referenceStone;
    private int nearestK = 3;
    private int quizCount = 3;

    private int selectedK = 0;
    private int currentQuizCount = 0;

    public List<Transform> referenceStonePos; 

    private void Start()
    {
        foreach (Transform child in transform)
        {
            stones.Add(child.gameObject);
        }

        initNearestStone();
    }

    private void initNearestStone()
    {
        ResetStonesColor();
        PlaceReferenceStone();
        SetClosestStones();
        selectedK = 0;
        currentQuizCount++;
    }

    private void PlaceReferenceStone()
    {
        /*
                float randomX = Random.Range(-4.2f, 0.8f);
                float y = 1.0f;
                float randomZ = Random.Range(-1f, -4f);
        */
        referenceStone.transform.localPosition = referenceStonePos[currentQuizCount].localPosition;
    }

    private void SetClosestStones()
    {
        nearestStones = stones.OrderBy(stone => Vector3.Distance(referenceStone.transform.position, stone.transform.position))
            .Take(nearestK).ToList();
        foreach(GameObject nearestStone in nearestStones) 
        {
            StoneClickHandler s = nearestStone.GetComponent<StoneClickHandler>();
            s.isAnswer= true;
        }
    }

    private void ResetStonesColor()
    {
        if(nearestStones == null) return;
        foreach(GameObject stone in nearestStones)
        {
            stone.GetComponent<StoneClickHandler>().ResetColor();
        }
    }

    public void NearestStoneClicked()
    {
        selectedK++;
        if(selectedK == nearestK)
        {
            if(currentQuizCount==quizCount)
            {
                quiz3.endQuiz();
            }
            else
            {
                initNearestStone();
            }
        }
    }
}
