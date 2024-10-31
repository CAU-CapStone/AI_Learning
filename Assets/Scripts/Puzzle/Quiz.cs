using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiz : MonoBehaviour
{
    public TableItemSpawner tableItemSpawner;
    public QuizBasket smallItemBasket;
    public QuizBasket largeItemBasket;
    public int itemCount=15;    //TableItemSpawner의 위치개수보다 많으면 무시됨
    private void OnEnable()
    {
        SpawnItems();
    }

    private void Update()
    {
        if (smallItemBasket.isFull() && largeItemBasket.isFull())
        {
            Debug.Log("Quiz Clear");
        }
    }

    private void OnDisable()
    {
        tableItemSpawner.DeleteAllSpawnedItems();
        smallItemBasket.resetAnswer();
        largeItemBasket.resetAnswer();
    }

    public void SpawnItems()
    {
        for(int i=0; i<itemCount; i++)
        {
            QuizBasket randomBasket = getRandomBasket();
            tableItemSpawner.SpawnNextItem(randomBasket);
            randomBasket.AnswerCount++;
        }
    }

    private QuizBasket getRandomBasket()
    {
        return Random.Range(0,2) == 0 ? smallItemBasket : largeItemBasket;
    }
}
