## knn.cs

사용 예시
```cs
//객체 생성; Neighbors 프로퍼티로 확인할 이웃 수 결정(기본값 5)
//제네릭 타입은 Tuple 가능
//여기선 T = Tuple<double, double>
Knn<(double, double)> knn = new()
{
    Neighbors = 5
}

//Fit
//features는 List<T>(튜플들의 리스트), targets는 List<int>
knn.Fit(features, targets);

//Predict
//input의 myFeature는 T
//output은 Tuple<int, List<T>> 형태; int는 예측값, List<T>는 이웃들
(int pred, List<(double, double)> nei) = knn.Predict(myFeature);
```

---

## 사용 예제(SampleScene)

plane의 Data.cs
데이터들을 시각화

bream은 붉은색 원, smelt는 푸른색 원
예측한 물고기는 각 종류에 해당하는 색깔의 사각형
이웃들은 초록색 원

![예시 이미지 1](https://github.com/CAU-CapStone/AI_Learning/blob/ml/Captures/red_pred.png)
![예시 이미지 2](https://github.com/CAU-CapStone/AI_Learning/blob/ml/Captures/blue_pred.png)
