using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScatterPlot : MonoBehaviour
{
    public GameObject scatterPlot;
    List<(double, double)> bream_data = new()
    {
        (26.3, 290.0), (26.5, 340.0), (29.0, 363.0), (29.0, 430.0),
        (29.7, 450.0), (29.7, 500.0), (30.0, 390.0), (30.0, 450.0),
        (30.7, 500.0), (31.0, 475.0), (31.0, 500.0), (31.5, 500.0),
        (32.0, 340.0), (32.0, 600.0), (32.0, 600.0), (33.0, 700.0),
        (33.0, 700.0), (33.5, 610.0), (33.5, 650.0), (34.0, 575.0),
    };

    List<(double,double)> smelt_data = new() {
        (34.0, 685.0), (34.5, 620.0), (35.0, 680.0), (35.0, 725.0),
        (35.0, 720.0), (36.0, 714.0), (37.0, 1000.0), (38.5, 920.0),
        (38.5, 955.0), (39.5, 925.0), (41.0, 975.0), (41.0, 950.0),
        (10.5, 7.5), (10.6, 7.0), (11.0, 9.7), (11.3, 8.7),
        (11.8, 10.0), (11.8, 9.9), (12.0, 9.8), (12.2, 12.2),
        (12.4, 13.4), (13, 12.2), (14.3, 19.7), (15.0, 19.9),
    };

    void Start()
    {
        ListScatterPlot chart = scatterPlot.GetComponent<ListScatterPlot>();
        chart.setTitle("Fish Market");
        chart.setAxisName("Weight", "Length");
        chart.setData("bream", bream_data);
        chart.setData("smelt", smelt_data);
    }
}
