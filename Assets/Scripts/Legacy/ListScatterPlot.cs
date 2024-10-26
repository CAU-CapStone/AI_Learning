using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XCharts;
using XCharts.Runtime;

public class ListScatterPlot : MonoBehaviour
{
    private ScatterChart scatterChart;
    private const int symbolSize = 5;

    void Awake()
    {
        scatterChart = GetComponent<ScatterChart>();
        scatterChart.RemoveAllSerie();
        Legend legend = scatterChart.EnsureChartComponent<Legend>();

        legend.show = true;
        legend.orient = Orient.Horizonal;
        legend.location.align = Location.Align.BottomCenter;
        legend.location.bottom = -0.15f;


    }

    public void setTitle(string newtitle)
    {

        Title title = scatterChart.EnsureChartComponent<Title>();
        title.text = newtitle;
    }

    public void setAxisName(string XAxisName, string YAxisName)
    {
        XAxis xAxis = scatterChart.EnsureChartComponent<XAxis>();
        YAxis yAxis = scatterChart.EnsureChartComponent<YAxis>();

        xAxis.axisName.name = XAxisName;
        yAxis.axisName.name = YAxisName;
        xAxis.axisName.show = true;
        yAxis.axisName.show = true;
    }

    public void setData(string dataName, List<(double,double)> data)
    {
        Serie newSerie = scatterChart.AddSerie<Scatter>(dataName);
        
        foreach (var item in data)
        {
            newSerie.AddXYData(item.Item1,item.Item2);
            newSerie.symbol.size = symbolSize;
        }
    }

    public void resetData()
    {
        scatterChart.RemoveAllSerie();
    }
}
