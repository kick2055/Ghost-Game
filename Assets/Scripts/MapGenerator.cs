using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    
    [SerializeField]
    private Vector2[] possibleRobots;
    [SerializeField]
    private Vector2[] possibleEnemies;
    private List<Robot> listOfRobots = new List<Robot>();
    private List<Enemy> listOfEnemies = new List<Enemy>();
    public GameObject robotPrefab, chestPrefab, enemyPrefab;

    public Tilemap wallCollider1;
    public Tilemap wallCollider2;
    public List<List<int>> lista = new List<List<int>>();
    public void calc(int a, int b,int c, int d)
    {
        for (int i = 0; i < (a-c); i++)
        {
            lista.Add(new List<int>());
            for (int j = 0; j < (b-d); j++)
            {
                if (wallCollider1.HasTile(new Vector3Int(i+c, j+d, 0)) || wallCollider2.HasTile(new Vector3Int(i+c, j+d, 0)))
                {
                    lista[i].Add(0);
                }
                else
                {
                    lista[i].Add(1);
                }
            }
        } 
    }


    List<int> RandomIds(int max, int ammount)
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
        string a = "";
        List<int> positionsOfRobots = RandomIds(23,9);
        List<int> positionsOfEnemies = RandomIds(5, 2);
        for (int i=0;i<positionsOfRobots.Count;i++)
        {
            Robot robot = Instantiate(robotPrefab, new Vector3(possibleRobots[positionsOfRobots[i]].x, possibleRobots[positionsOfRobots[i]].y, 0), Quaternion.identity).GetComponent<Robot>();
            listOfRobots.Add(robot);
        }
        for(int i=0;i<positionsOfEnemies.Count;i++)
        {
            Enemy enemy = Instantiate(enemyPrefab, new Vector3(possibleEnemies[positionsOfEnemies[i]].x, possibleEnemies[positionsOfEnemies[i]].y,0), Quaternion.identity).GetComponent<Enemy>();
            
            listOfEnemies.Add(enemy);

        }
        calc(56, 84, -72, -10);
        for(int i=0;i<lista.Count;i++)
        {
            for(int j=0;j<lista[i].Count;j++)
            {
                if(lista[i][j]==1)
                {
                    a += "+";
                }
                else
                {
                    a += "@";
                }
            }
            a += "\n";
        }
        Debug.Log(a);



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
