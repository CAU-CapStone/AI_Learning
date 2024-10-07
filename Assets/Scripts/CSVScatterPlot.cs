using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XCharts;
using XCharts.Runtime;

public class CSVScatterPlot : MonoBehaviour
{
    public TextAsset csvFile;
    public Font font;
    private ScatterChart scatterChart;
    private const int symbolSize = 5;
    void Start()
    {
        scatterChart= GetComponent<ScatterChart>();

        string[] rows = csvFile.text.Split('\n');
        Dictionary<string,Serie> fishSeries = new Dictionary<string,Serie>();
        XAxis xAxis = scatterChart.EnsureChartComponent<XAxis>();
        YAxis yAxis = scatterChart.EnsureChartComponent<YAxis>();
        Title title = scatterChart.EnsureChartComponent<Title>();
        Legend legend = scatterChart.EnsureChartComponent<Legend>();

        title.text = "Fish Market";
        xAxis.axisName.name = "Length";
        yAxis.axisName.name = "Weight";
        xAxis.axisName.show = true; 
        yAxis.axisName.show = true;

        title.labelStyle.textStyle.font= font;
        title.labelStyle.textStyle.fontStyle = FontStyle.Bold;
        xAxis.axisLabel.textStyle.font = font;
        yAxis.axisLabel.textStyle.font = font;
        
        legend.show = true;
        legend.orient = Orient.Horizonal;
        legend.location.align = Location.Align.BottomCenter;
        legend.location.bottom = -0.15f;

        for(int i=1;i<rows.Length;i++)
        {
            string[] values = rows[i].Split(',');
            if(values.Length >= 3 )
            {
                string fishName = values[0];
                float weight = float.Parse(values[1]);
                float length = float.Parse(values[2]);

                if(!fishSeries.ContainsKey(fishName))
                {
                    Serie newSerie = scatterChart.AddSerie<Scatter>(fishName);       
                    fishSeries[fishName] = newSerie;
                    newSerie.symbol.size = symbolSize;
                }

                fishSeries[fishName].AddXYData(length, weight);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
