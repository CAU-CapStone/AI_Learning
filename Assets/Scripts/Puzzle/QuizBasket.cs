using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizBasket : MonoBehaviour
{
    public int AnswerCount = 0;
    public int currentItemCount = 0;
    public float AnswerScale = 0;
    public static float AnswerScaleRange = 0.3f;
    public float minAnswerScale;
    public float maxAnswerScale;

    private void Awake()
    {
        minAnswerScale = AnswerScale * (1 - AnswerScaleRange);
        maxAnswerScale = AnswerScale * (1 + AnswerScaleRange);
    }

    //�Ⱦ�
    private float CalculateItemVolume(GameObject item)
    {
        Renderer rend = item.GetComponent<Renderer>();
        if (rend != null)
        {
            Vector3 itemSize = rend.bounds.size;
            return itemSize.x * itemSize.y * itemSize.z;
        }
        return 0.0f;
    }

    public bool IsItemVolumeAcceptable(GameObject item)
    {
        bool xAnswer = item.transform.localScale.x >= minAnswerScale
            && item.transform.localScale.x <= maxAnswerScale;

        bool yAnswer = item.transform.localScale.y >= minAnswerScale
            && item.transform.localScale.y <= maxAnswerScale;

        bool zAnswer = item.transform.localScale.z >= minAnswerScale
            && item.transform.localScale.z <= maxAnswerScale;

        return xAnswer&& yAnswer && zAnswer;
    }

    public bool TryAddItem(GameObject item)
    {
        if (IsItemVolumeAcceptable(item))
        {
            if (currentItemCount < AnswerCount)
            {
                currentItemCount++;
                Debug.Log("�������� �ٱ��Ͽ� �����ϴ�! ���� �ٱ����� ������ ��: " + currentItemCount);
                return true;
            }
            else
            {
                Debug.Log("�ٱ��ϰ� ���� á���ϴ�! �� �̻� �������� ���� �� �����ϴ�.");
                return false;
            }
        }
        else
        {
            Debug.Log("�������� ���ǰ� �ٱ��ϰ� ����ϴ� ������ �ƴմϴ�.");
            return false;
        }
    }

    public bool isFull()
    {
        return AnswerCount == currentItemCount ? true : false;
    }

    public void resetAnswer()
    {
        AnswerCount= 0;
        currentItemCount= 0;
    }

}
