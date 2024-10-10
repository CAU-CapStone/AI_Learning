using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// knn 퀴즈 예제 코드
/// </summary>
public class KnnExample : MonoBehaviour
{
    //데이터
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
    
    //knn 객체 생성 및 필요한 variable들
    private Knn<(double, double)> knn = new();
    private double fish_length = 35;
    private double fish_weight = 700;
   
    public ListScatterPlot scatterChart; //scatterChart 객체
    
    //connection 순서 나타내는 bool (노드가 2개라 bool 사용)
    bool dataConnectionOrder = false; //데이터가 같은 index의 노드에 연결되어있으면 true
    bool predictionConnectionOrder = false; // prediction이 같은 index의 노드에 연결되어있으면 true
    
    static string buttonHierarchyPath = "Canvas/GoButton";
    public static List<ConnectionLine> Connections = new List<ConnectionLine>(); //모든 연결 화살표를 저장할 리스트

    void Start()
    {
        //버튼 눌렀을 때 knnQuiz 함수 실행하도록 연결
        GameObject.Find(buttonHierarchyPath).GetComponent<Button>().onClick.AddListener(KnnQuiz);
        
        //scatterChart 객체 찾아서 비활성화
        scatterChart = GameObject.Find("ChartCanvas/ScatterChart").GetComponent<ListScatterPlot>();
        scatterChart.transform.parent.gameObject.SetActive(false);
    }

    /// <summary>
    /// knn driver함수. knn 퀴즈의 핵심 파츠
    /// </summary>
    public void KnnQuiz()
    {
        //모든 노드가 연결되었는지 확인. 노드가 많아지면 바뀌어야 하는 숫자.
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
                else if (index.Item1 != 2)
                {
                    //만약 0번 노드가 0번 노드에 연결되어있으면 dataConnectionOrder true
                    dataConnectionOrder = index.Item1.Equals(index.Item2);
                }
            }
            else if(connect.currentConnectionType == ConnectionManager.ConnectionType.Prediction)
            {
                //만약 0번 노드가 0번 노드에 연결되어있으면 predictionConnectionOrder true
                predictionConnectionOrder = index.Item1.Equals(index.Item2);
            }
        }

        //scatterChart 초기화
        scatterChart.transform.parent.gameObject.SetActive(true);
        scatterChart.setTitle("Fish");

        //데이터 블록 커넥션. 만약 length가 0번째에 연결되어있으면 length가 x축, weight가 y축, 반대면 weight가 x축, length가 y축
        List<(double, double)> features = new();
        List<(double, double)> bream = new();
        List<(double, double)> smelt = new();
        if (dataConnectionOrder)
        {
            scatterChart.setAxisName("Length", "Weight");
            for (int i = 0; i < bream_length.Count; i++)
                bream.Add((bream_length[i], bream_weight[i]));
            for (int i = 0; i < smelt_length.Count; i++)
                smelt.Add((smelt_length[i], smelt_weight[i]));
        }
        else
        {
            scatterChart.setAxisName("Weight", "Length");
            for (int i = 0; i < bream_length.Count; i++)
                bream.Add((bream_weight[i], bream_length[i]));
            for (int i = 0; i < smelt_length.Count; i++)
                smelt.Add((smelt_weight[i], smelt_length[i]));
        }
        
        //scatterChart에 학습 데이터 시각화
        scatterChart.setData("bream",bream);
        scatterChart.setData("smelt",smelt);
        
        //학습 데이터 knn에 넣기
        features.AddRange(bream);
        features.AddRange(smelt);

        List<int> targets = new();
        for (int i = 0; i < bream_length.Count; i++)
            targets.Add(0);
        for (int i = 0; i < smelt_length.Count; i++)
            targets.Add(1);

        knn.Fit(features, targets);
        
        ////////////////////////
        //knn prediction code///
        ////////////////////////
        //prediction block 커넥션
        (int target, List< (double,double)>feature) predict;
        
        //만약 predictionConnectionOrder true면 length가 x축, weight가 y축, 반대면 weight가 x축, length가 y축으로 predict 후 분류 plot.
        if (predictionConnectionOrder)
        {
            predict = knn.Predict((fish_length,fish_weight));
            if (predict.target == 0)
            {
                scatterChart.setData("bream_predicted", new List<(double, double)>() { (fish_length, fish_weight) });
            }
            else if (predict.target == 1)
            {
                scatterChart.setData("smelt_predicted", new List<(double, double)>() { (fish_length, fish_weight) });
            }
        }
        else
        {
            predict = knn.Predict((fish_weight,fish_length));
            if (predict.target == 0)
            {
                scatterChart.setData("bream_predicted", new List<(double, double)>() { (fish_weight, fish_length) });
            }
            else if (predict.target == 1)
            {
                scatterChart.setData("smelt_predicted", new List<(double, double)>() { (fish_weight, fish_length) });
            }
        }
    }
}
