using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private Vector2[] possibleRobots;
    [SerializeField]
    private Vector2[] possibleEnemies;
    private List<Robot> listOfRobots = new List<Robot>();
    private List<Enemy> listOfEnemies = new List<Enemy>();
    public GameObject robotPrefab, chestPrefab, enemyPrefab;
    public Player activePlayer;
    public Tilemap wallCollider1;
    public Tilemap wallCollider2;
    public List<List<int>> Map = new List<List<int>>();
    public bool playerFound = false;
    public List<Point> neighboursten = new List<Point>();
    public List<Point> neighboursfourteen = new List<Point>();
    public float nextActionTime = 0.0f;
    public float period = 4.0f;

    public void CreateMap(int right, int up,int left, int down)
    {
        for (int i = 0; i < (right-left); i++)
        {
            Map.Add(new List<int>());
            for (int j = up-down-1; j >= -1; j--)
            {
                if (wallCollider1.HasTile(new Vector3Int(i+left, j+down, 0)) || wallCollider2.HasTile(new Vector3Int(i+left, j+down, 0)))
                {
                    Map[i].Add(0);
                }
                else
                {
                    Map[i].Add(1);
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
    public Point CheckWhatChank(float x, float y) 
    {
        float a = x/2.56f, b = y/2.56f;
        if( x<0 && y < 0)
        {
            a = (x / 2.56f) - 1;
            b = (y / 2.56f) - 1;
        }
        else if (x<0 && y >= 0)
        {
            a = (x / 2.56f) - 1;
        }
        else if(x>=0 && y<0)
        {
            b = (y / 2.56f) - 1;
        }
        return new Point((int)a,(int)b);
    }
    public class Point
    {
        public int X;
        public int Y;
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

    }
    public int CalculateDistance(int StartX, int StartY, int EndX, int EndY)
    {
        double exit = Mathf.Sqrt(Mathf.Pow((10 *StartX)- (10 * EndX),2)+Mathf.Pow((10 *StartY)-(10 * EndY),2));
        return (int)exit;
    }
    public int ConvertXFromMapToWorld(int x)
    {
        return x;
    }
    public int ConvertYFromMapToWorld(int y)
    {
        return y;
    }
    public int ConvertXFromWorldTomap(int x)
    {
        return x;
    }
    public int ConvertYFromWorldTomap(int y)
    {
        return y;
    }
    public class AStarPoint
    {
        public int X;
        public int Y;
        public int G;
        public int H;
        public int F => G + H;
        public AStarPoint Parent;
        public AStarPoint(int x, int y)
        {
            X = x;
            Y = y;
            G = 0;
            H = 0;
            Parent = null;
        }
    }
    public AStarPoint AStarAlgoritm(Point Start, Point End)
    {

        AStarPoint currentPoint = null;
        List<AStarPoint> toBeEvaluated = new List<AStarPoint>();
        List<AStarPoint> Evaluated = new List<AStarPoint>();
        toBeEvaluated.Add(new AStarPoint(Start.X,Start.Y));
        while(true)//currentPoint.X != End.X || currentPoint.Y != End.Y
        {
            currentPoint = toBeEvaluated.OrderBy(p=>p.F).FirstOrDefault();
            toBeEvaluated.Remove(currentPoint);
            Evaluated.Add(currentPoint);
            if(currentPoint.X == End.X && currentPoint.Y == End.Y)
            {
                return currentPoint;
            }
            foreach(Point neigh in neighboursfourteen)
            {
                if (Map[currentPoint.X + 27 - neigh.X][-currentPoint.Y + 31 + neigh.Y] == 0 || Evaluated.Exists(p => (p.X == currentPoint.X - neigh.X && p.Y == currentPoint.Y - neigh.Y)))
                {
                    //Debug.Log(Map[currentPoint.X + 27 - neigh.X][-currentPoint.Y + 31 - neigh.Y] + "SCIANA\n");
                }

                else if (!(toBeEvaluated.Exists(p => (p.X == currentPoint.X - neigh.X && p.Y == currentPoint.Y - neigh.Y))) || (toBeEvaluated.Find(p => (p.X == currentPoint.X - neigh.X && p.Y == currentPoint.Y - neigh.Y)).G > currentPoint.G + 14))
                {
                    AStarPoint newPoint = new AStarPoint(currentPoint.X - neigh.X, currentPoint.Y - neigh.Y);
                    newPoint.G = currentPoint.G + 14;
                    newPoint.H = CalculateDistance(newPoint.X, newPoint.Y, End.X, End.Y);
                    newPoint.Parent = currentPoint;
                    if (!(toBeEvaluated.Exists(p => (p.X == currentPoint.X - neigh.X && p.Y == currentPoint.Y - neigh.Y))))
                    {
                        toBeEvaluated.Add(newPoint);
                    }
                }
            }
            foreach (Point neigh in neighboursten)
            {
                if (Map[currentPoint.X + 27 - neigh.X][-currentPoint.Y + 31 + neigh.Y] == 0 || Evaluated.Exists(p => (p.X == currentPoint.X - neigh.X && p.Y == currentPoint.Y - neigh.Y)))
                {
                    if(Map[currentPoint.X + 27 - neigh.X][-currentPoint.Y + 31 - neigh.Y] ==0)
                    {
                        //Debug.Log(" jeden: "+currentPoint.X + " " + (-currentPoint.Y));
                        //Debug.Log("dwa: "+(currentPoint.X + 27 - neigh.X) + " " + (-currentPoint.Y + 31 - neigh.Y));
                    }
                }

                else if (!(toBeEvaluated.Exists(p => (p.X == currentPoint.X - neigh.X && p.Y == currentPoint.Y - neigh.Y))) || (toBeEvaluated.Find(p => (p.X == currentPoint.X - neigh.X && p.Y == currentPoint.Y - neigh.Y)).G > currentPoint.G + 10))
                {
                    AStarPoint newPoint = new AStarPoint(currentPoint.X - neigh.X, currentPoint.Y - neigh.Y);
                    newPoint.G = currentPoint.G + 10;
                    newPoint.H = CalculateDistance(newPoint.X, newPoint.Y, End.X, End.Y);
                    newPoint.Parent = currentPoint;
                    if (!(toBeEvaluated.Exists(p => (p.X == currentPoint.X - neigh.X && p.Y == currentPoint.Y - neigh.Y))))
                    {
                        toBeEvaluated.Add(newPoint);
                    }
                }
            }
        }
    }
    void Start()
    {
        playerFound = false;
        neighboursfourteen.Add(new Point(1,1));
        neighboursfourteen.Add(new Point(1,-1));
        neighboursfourteen.Add(new Point(-1,1));
        neighboursfourteen.Add(new Point(-1,-1));
        neighboursten.Add(new Point(1,0));
        neighboursten.Add(new Point(-1,0));
        neighboursten.Add(new Point(0,1));
        neighboursten.Add(new Point(0,-1));
        string a = "";
        List<int> positionsOfRobots = RandomIds(23,9);
        List<int> positionsOfEnemies = RandomIds(5, 5);
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
        CreateMap(21, 32, -27, -3);
        for(int i=0;i< Map[0].Count - 1; i++)
        {
            for(int j=0;j<Map.Count;j++)
            {
                if(Map[j][i]==1)
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
        

        Point abc = new Point(0, 0);
        Point bcd = new Point(-26, 30);
        AStarPoint asp = AStarAlgoritm(abc, bcd);
        int x = 0, y = 0, z = 1;
        string message = "";
        List<Vector3> trasa = new List<Vector3>();

        /*while(asp.Parent != null)
        {
            message = "";

            if (asp.X > x) { message += "lewo "; }
            else if(asp.X < x) { message += "prawo "; }
            if(asp.Y > y) { message += "gora "; }
            else if(asp.Y < y) { message += "dol "; }
            x = asp.X;
            y = asp.Y;
            Debug.Log(z + ". " + message + " "+  asp.X + " " +  asp.Y + "\n");
            trasa.Add(new Vector3(asp.X, asp.Y, 0));
            asp = asp.Parent;
            z++;
        }
        listOfEnemies[0].PathToPlayer = trasa;
        foreach(Vector3 v in listOfEnemies[0].PathToPlayer)
        {
            Debug.Log(v.x + " " + v.y);
        }*/
        
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextActionTime)
        {
            nextActionTime += period;
            foreach (Enemy enemy in listOfEnemies)
            {
                Point start = CheckWhatChank(enemy.transform.position.x, enemy.transform.position.y);
                Point end = CheckWhatChank(activePlayer.transform.position.x, activePlayer.transform.position.y);
                AStarPoint road = AStarAlgoritm(start, end);
                List<Vector3> path = new List<Vector3>();
                while (road.Parent != null)
                {
                    path.Add(new Vector3(road.X, road.Y, 0));
                    Debug.Log(road.X + " " + road.Y);
                    road = road.Parent;
                }
                enemy.PathToPlayer = path;
            }
        }
    }
}