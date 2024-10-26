using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs;  // ������ ������ ������ �迭
    public Transform table;           // ���̺� ������Ʈ (�θ�)
    public Transform[] spawnPoints;  // ���̺� �ڽ� ��ġ ������Ʈ���� Transform �迭
    public int currentIndex = 0;     // ���� ��ġ �ε���
    private List<GameObject> spawnedItems = new List<GameObject>();

    void Awake()
    {
        // ���̺��� �ڽ� ������Ʈ���� Transform�� �����ͼ� �迭�� ����
        spawnPoints = GetTableChildTransforms();
    }

    

    // ���̺� �ڽ� ������Ʈ���� Transform�� �迭�� �������� �Լ�
    Transform[] GetTableChildTransforms()
    {
        int childCount = table.childCount;
        Transform[] childTransforms = new Transform[childCount];
        for (int i = 0; i < childCount; i++)
        {
            childTransforms[i] = table.GetChild(i); // �� �ڽ��� Transform ������
        }
        return childTransforms;        
    }

    // �������� �ϳ��� �����ϴ� �Լ� (�ʿ��� ��� ���)
    public void SpawnNextItem(QuizBasket basket)
    {
        if (currentIndex < spawnPoints.Length && itemPrefabs.Length > 0)
        {
            // �����ϰ� ������ ���� (�Ǵ� ������� ����)
            GameObject randomItemPrefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
            Instantiate(randomItemPrefab, spawnPoints[currentIndex].position, Quaternion.identity);
            currentIndex++; // ���� ��ġ�� �̵�
        }
        else
        {
            Debug.LogWarning("�������� �� �̻� ������ �� �����ϴ�. ��ġ�� �����ϰų� �������� �����ϴ�.");
        }
    }

    public void DeleteAllSpawnedItems()
    {
        foreach (GameObject item in spawnedItems)
        {
            if (item != null)
            {
                Destroy(item);
            }
        }
        spawnedItems.Clear();
        currentIndex = 0;
    }
}
