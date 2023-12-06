using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    
    [SerializeField]
    private Vector2[] possibleRobots;
    private List<Robot> listOfRobots = new List<Robot>();
    public GameObject robotPrefab, chestPrefab;

    List<int> RandomIdRobots(int max, int ammount)
    {
        List<int> list = new List<int>();
        int newId;
        while(list.Count < ammount)
        {
            newId = Random.Range(0, max);
            if (!list.Contains(newId)) list.Add(newId);
        }
        return list;
    }

    void Start()
    {
        List<int> positionsOfRobots = RandomIdRobots(23,9);
        for(int i=0;i<positionsOfRobots.Count;i++)
        {
            Robot robot = Instantiate(robotPrefab, new Vector3(possibleRobots[positionsOfRobots[i]].x, possibleRobots[positionsOfRobots[i]].y, 0), Quaternion.identity).GetComponent<Robot>();
            listOfRobots.Add(robot);
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
