using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Data;
using System.Linq;

public class Knn<T> where T : ITuple
{
    private List<T> features;
    private List<int> targets;

    public int Neighbors { get; set; } = 5;
    
    public void Fit(List<T> features, List<int> targets)
    {
        if (features == null)
        {
            throw new NoNullAllowedException("features cannot be null");
        }
        
        if (targets == null)
        {
            throw new NoNullAllowedException("targets cannot be null");
        }
        
        if (features.Count != targets.Count)
        {
            throw new Exception("feature and targets do not match");
        }
        
        this.features = new(features);
        this.targets = new(targets);
    }

    public (int target, List<T> feature) Predict(T feature)
    {
        List<(double norm, int target, T feature)> neighbors = new();

        for (int i = 0; i < this.features.Count; i++)
        {
            double norm = Norm(feature, this.features[i]);

            neighbors.Add((norm, this.targets[i], this.features[i]));
            neighbors.Sort();
            
            if (neighbors.Count > Neighbors)
            {
                neighbors.RemoveAt(Neighbors);
            }
        }
        
        int predict = neighbors
            .Select(x => x.target)
            .GroupBy(label => label)
            .OrderByDescending(group => group.Count())
            .First()
            .Key;
        
        List<T> points = neighbors
            .Where(x => x.target == predict)
            .Select(x => x.feature)
            .ToList();
        
        return (predict, points);
    }

    private double Norm(T feature1, T feature2)
    {
        double result = 0;

        for (int i = 0; i < feature1.Length; i++)
        {
            double temp = (double)feature1[i] - (double)feature2[i];
            result += temp * temp;
        }

        result = Math.Sqrt(result);
        return result;
    }
}
