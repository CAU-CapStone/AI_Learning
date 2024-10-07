using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KnnExample : MonoBehaviour
{
    private Knn<(double, double)> knn = new();
    
    private List<double> bream_length = new()
    {
        25.4, 26.3, 26.5, 29.0, 29.0, 29.7, 29.7, 30.0, 30.0, 30.7, 31.0, 31.0,
        31.5, 32.0, 32.0, 32.0, 33.0, 33.0, 33.5, 33.5, 34.0, 34.0, 34.5, 35.0,
        35.0, 35.0, 35.0, 36.0, 36.0, 37.0, 38.5, 38.5, 39.5, 41.0, 41.0
    };

    private List<double> bream_weight = new()
    {
        242.0, 290.0, 340.0, 363.0, 430.0, 450.0, 500.0, 390.0, 450.0, 500.0, 475.0, 500.0,
        500.0, 340.0, 600.0, 600.0, 700.0, 700.0, 610.0, 650.0, 575.0, 685.0, 620.0, 680.0,
        700.0, 725.0, 720.0, 714.0, 850.0, 1000.0, 920.0, 955.0, 925.0, 975.0, 950.0
    };

    private List<double> smelt_length = new()
    {
        9.8, 10.5, 10.6, 11.0, 11.2, 11.3, 11.8, 11.8, 12.0, 12.2, 12.4, 13.0, 14.3, 15.0
    };

    private List<double> smelt_weight = new()
    {
        6.7, 7.5, 7.0, 9.7, 9.8, 8.7, 10.0, 9.9, 9.8, 12.2, 13.4, 12.2, 19.7, 19.9
    };

    private double fish_length = 10;
    private double fish_weight = 8;
    
    public Camera camera;
    public GameObject redDot;
    public GameObject blueDot;
    public GameObject greenDot;
    public GameObject redDotSquare;
    public GameObject blueDotSquare;
    
    //connection 순서 나타내는 bool (노드가 2개라 bool 사용)
    bool dataConnectionOrder = false;
    bool predictionConnectionOrder = false;
    
    static string buttonHierarchyPath = "Canvas/GoButton";
    public static List<ConnectionLine> Connections = new List<ConnectionLine>();

    void Start()
    {
        //버튼 눌렀을 때 knnQuiz 함수 실행하도록 연결
        GameObject.Find(buttonHierarchyPath).GetComponent<Button>().onClick.AddListener(KnnQuiz);
    }

    public void KnnQuiz()
    {
        //모든 노드가 연결되었는지 확인
        if (Connections.Count < 5)
        {
            Debug.Log("Not all nodes are connected");
            return;
        }

        //모든 커넥션 iterate해서 정보 추출
        foreach (var connect in Connections)
        {
            Tuple<int, int> index = connect.GetIndex();
            Debug.Log("Connection: " + index.Item1 + " -> " + index.Item2 + " Type: " + connect.currentConnectionType);
            if(connect.currentConnectionType == ConnectionManager.ConnectionType.Data)
            {
                //만약 type끼리 연결이 안되어있으면 return
                if (index.Item1 == 2 && index.Item2 != 2)
                {
                    Debug.Log("Types must be connected together");
                    return;
                }

                //만약 0번 노드가 0번 노드에 연결되어있으면 dataConnectionOrder true
                dataConnectionOrder = index.Item1.Equals(index.Item2);
            }
            else if(connect.currentConnectionType == ConnectionManager.ConnectionType.Prediction)
            {
                //만약 0번 노드가 0번 노드에 연결되어있으면 predictionConnectionOrder true
                predictionConnectionOrder = index.Item1.Equals(index.Item2);
            }
        }

        //knn learning code///
        
        //data parsing
        for (int i = 0; i < bream_length.Count; i++)
        {
            Instantiate(redDot, new Vector3((float)bream_length[i] / 2, 0, (float)bream_weight[i] / 100), Quaternion.identity);
        }
        
        for (int i = 0; i < smelt_length.Count; i++)
        {
            Instantiate(blueDot, new Vector3((float)smelt_length[i] / 2, 0, (float)smelt_weight[i] / 100), Quaternion.identity);
        }

        //데이터 블록 커넥션. 만약 length가 0번째에 연결되어있으면 length가 x축, weight가 y축, 반대면 weight가 x축, length가 y축
        List<(double, double)> features = new();
        if (dataConnectionOrder)
        {
            for (int i = 0; i < bream_length.Count; i++)
                features.Add((bream_length[i], bream_weight[i]));
            for (int i = 0; i < smelt_length.Count; i++)
                features.Add((smelt_length[i], smelt_weight[i]));
        }
        else
        {
            for (int i = 0; i < bream_length.Count; i++)
                features.Add((bream_weight[i], bream_length[i]));
            for (int i = 0; i < smelt_length.Count; i++)
                features.Add((smelt_weight[i], smelt_length[i]));
        }
        
        List<int> targets = new();
        for (int i = 0; i < bream_length.Count; i++)
            targets.Add(0);
        for (int i = 0; i < smelt_length.Count; i++)
            targets.Add(1);

        knn.Fit(features, targets);
        
        //knn prediction code
        
        GameObject.Find("Canvas").SetActive(false);
        
        
        //prediction block 커넥션
        var predict = knn.Predict((fish_length, fish_weight));
        if (!predictionConnectionOrder)
        {
            predict = knn.Predict((fish_weight,fish_length));
        }
        
        switch (predict.Item1)
        {
            case 0:
                Instantiate(redDotSquare, new Vector3((float)fish_length / 2, 0, (float)fish_weight / 100), Quaternion.identity);
                break;
                
            case 1:
                Instantiate(blueDotSquare, new Vector3((float)fish_length / 2, 0, (float)fish_weight / 100), Quaternion.identity);
                break;
        }

        foreach (var feature in predict.Item2)
        {
            Instantiate(greenDot, new Vector3((float)feature.Item1 / 2, 0, (float)feature.Item2 / 100), Quaternion.identity);
        }
        camera.transform.position = new Vector3(10, 10, 4);
    }
}
