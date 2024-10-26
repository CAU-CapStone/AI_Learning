using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs;  // 생성할 아이템 프리팹 배열
    public Transform table;           // 테이블 오브젝트 (부모)
    public Transform[] spawnPoints;  // 테이블 자식 위치 오브젝트들의 Transform 배열
    public int currentIndex = 0;     // 현재 위치 인덱스
    private List<GameObject> spawnedItems = new List<GameObject>();

    void Awake()
    {
        // 테이블의 자식 오브젝트들의 Transform을 가져와서 배열에 저장
        spawnPoints = GetTableChildTransforms();
    }

    

    // 테이블 자식 오브젝트들의 Transform을 배열로 가져오는 함수
    Transform[] GetTableChildTransforms()
    {
        int childCount = table.childCount;
        Transform[] childTransforms = new Transform[childCount];
        for (int i = 0; i < childCount; i++)
        {
            childTransforms[i] = table.GetChild(i); // 각 자식의 Transform 가져옴
        }
        return childTransforms;        
    }

    // 아이템을 하나씩 생성하는 함수 (필요한 경우 사용)
    public void SpawnNextItem(QuizBasket basket)
    {
        if (currentIndex < spawnPoints.Length && itemPrefabs.Length > 0)
        {
            // 랜덤하게 아이템 선택 (또는 순서대로 생성)
            GameObject randomItemPrefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
            Instantiate(randomItemPrefab, spawnPoints[currentIndex].position, Quaternion.identity);
            currentIndex++; // 다음 위치로 이동
        }
        else
        {
            Debug.LogWarning("아이템을 더 이상 생성할 수 없습니다. 위치가 부족하거나 아이템이 없습니다.");
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
