using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Data : MonoBehaviour
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
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < bream_length.Count; i++)
        {
            Instantiate(redDot, new Vector3((float)bream_length[i] / 2, 0, (float)bream_weight[i] / 100), Quaternion.identity);
        }
        
        for (int i = 0; i < smelt_length.Count; i++)
        {
            Instantiate(blueDot, new Vector3((float)smelt_length[i] / 2, 0, (float)smelt_weight[i] / 100), Quaternion.identity);
        }

        List<(double, double)> features = new();
        for (int i = 0; i < bream_length.Count; i++)
            features.Add((bream_length[i], bream_weight[i]));
        for (int i = 0; i < smelt_length.Count; i++)
            features.Add((smelt_length[i], smelt_weight[i]));

        List<int> targets = new();
        for (int i = 0; i < bream_length.Count; i++)
            targets.Add(0);
        for (int i = 0; i < smelt_length.Count; i++)
            targets.Add(1);
        
        knn.Fit(features, targets);
        
        var predict = knn.Predict((fish_length, fish_weight));

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
