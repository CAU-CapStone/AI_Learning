using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemModifier : MonoBehaviour
{
    public QuizBasket smallBasket;
    public QuizBasket largeBasket;
    public bool randomScale;

    private void OnEnable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;

            float randomX = Random.Range(0f, 360f);
            float randomY = Random.Range(0f, 360f);
            float randomZ = Random.Range(0f, 360f);
            child.transform.rotation = Quaternion.Euler(randomX, randomY, randomZ);

            int randomScale = Random.Range(0, 2);
            if(randomScale == 0)
            {
                largeBasket.AnswerCount++;
                
                child.transform.localScale = getNewScale(largeBasket);
            }
            else
            {
                smallBasket.AnswerCount++;
                
                child.transform.localScale = getNewScale(smallBasket);
            }

        }
    }

    private Vector3 getNewScale(QuizBasket answerBasket)
    {
        Vector3 newScale = new Vector3();
        if (randomScale)
        {
            float minScale = answerBasket.AnswerScale * (1 - QuizBasket.AnswerScaleRange);
            float maxScale = answerBasket.AnswerScale * (1 + QuizBasket.AnswerScaleRange);
            newScale.x = Random.Range(minScale, maxScale);
            newScale.y = Random.Range(minScale, maxScale);
            newScale.z = Random.Range(minScale, maxScale);
        }
        else
        {
            newScale.x = answerBasket.AnswerScale;
            newScale.y = answerBasket.AnswerScale;
            newScale.z = answerBasket.AnswerScale;
        }

        return newScale;
    }
}
