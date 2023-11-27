using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    
    [SerializeField]
    private Vector2[] possibleRobots;
    private List<Robot> listOfRobots = new List<Robot>();
    public GameObject robotPrefab, chestPrefab;

    void Start()
    {
        int i = Random.Range(0, 3);
        Robot robot = Instantiate(robotPrefab, new Vector3(possibleRobots[i].x, possibleRobots[i].y, 0), Quaternion.identity).GetComponent<Robot>();
        listOfRobots.Add(robot);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
