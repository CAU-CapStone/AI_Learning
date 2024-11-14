using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindNearestHouse : MonoBehaviour
{
    public List<GameObject> houseList = new List<GameObject>();
    private List<GameObject> nearestHouses;
    private Dictionary<Color, int> colorCount;
    public GameObject referenceHouse;
    private int nearestK = 3;

    public GameObject redHouses;
    public GameObject greenHouses;
    public GameObject blueHouses;

    public Transform TopLeft;
    public Transform BottomRight;

    public Quiz4UIHandler quiz4UIHandler;

    void Start()
    {
        initNearHouse();
        initNearestHouse();
    }

    void Update()
    {
        
    }

    public void initNearestHouse()
    {
        referenceHouse.GetComponent<Renderer>().material.color = Color.white;
        placeReferenceHouse();
        setClosestHouses();
    }

    private void initNearHouse()
    {
        foreach(Transform child in redHouses.transform)
        {
            houseList.Add(child.gameObject);
        }
        foreach (Transform child in greenHouses.transform)
        {
            houseList.Add(child.gameObject);
        }
        foreach (Transform child in blueHouses.transform)
        {
            houseList.Add(child.gameObject);
        }
    }

    private void placeReferenceHouse()
    {
        float randomX = Random.Range(TopLeft.position.x, BottomRight.position.x);
        float y = TopLeft.position.y;
        float randomZ = Random.Range(BottomRight.position.z, TopLeft.position.z);
        referenceHouse.transform.position = new Vector3(randomX, y, randomZ);
    }

    private void setClosestHouses()
    {
        nearestHouses = houseList.OrderBy(
            house => Vector3.Distance(referenceHouse.transform.position, house.transform.position))
            .Take(nearestK).ToList();

        colorCount = new Dictionary<Color, int>();
        foreach(GameObject house in nearestHouses)
        {
            Color color = house.GetComponent<Renderer>().material.color;
            if (colorCount.ContainsKey(color))
            {
                colorCount[color]++;
            }
            else
            {
                colorCount[color] = 1;
            }
        }


    }

    public void checkAnswerColor(Color answerColor)
    {
        //Error Prone. 
        Color tmpColor = answerColor;
        tmpColor.a = 0f;

        int maxCount = 0;
        foreach(int count in colorCount.Values)
        {
            if(count>maxCount)
            {
                maxCount = count;
            }
        }
        if(colorCount.ContainsKey(tmpColor))
        {
            if(colorCount[tmpColor]==maxCount)
            {
                quiz4UIHandler.showResultUI(true);
            }
        }
        else
        {
            quiz4UIHandler.showResultUI(false);
        }
    }
}
