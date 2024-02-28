using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using UnityEngine.SceneManagement;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private Vector2[] possibleRobots;
    [SerializeField]
    private Vector2[] possibleEnemies;
    public List<Robot> listOfRobots = new List<Robot>();
    public List<Enemy> listOfEnemies = new List<Enemy>();
    public GameObject robotPrefab, chestPrefab, enemyPrefab;
    public Player activePlayer;
    public Tilemap wallCollider1;
    public Tilemap wallCollider2;
    public Tilemap waterCollider;
    public List<List<int>> Map = new List<List<int>>();
    public bool playerFound = false;
    public List<Point> neighboursten = new List<Point>();
    public List<Point> neighboursfourteen = new List<Point>();
    public float nextActionTime = 0.0f;
    public float period = 10f;
    public bool EnemySeesPlayer;
    public int numberOfRobots = 9;
    public int numberOfEnemy = 5;
    private float timerPlayerLost = 0.0f;
    private float timerPathToPlayer = 10.0f;
    private float waitTimePlayerLost = 4.0f;
    private float waitTimeToPlayer = 1f;
    public int[,] mapOfTakenPoints;
    [HideInInspector]
    public static MapGenerator instance;
    public bool gameIsPaused = false;
    public static event System.Action<string> playerVisibilityChanged;
    public GameObject parentOfPause;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
            return;
        }
        numberOfRobots = 6;
    }
    public void CreateMap(int right, int up,int left, int down)
    {
        for (int i = 0; i < (right-left); i++)
        {
            Map.Add(new List<int>());
            for (int j = up-down-1; j >= -1; j--)
            {
                if (wallCollider1.HasTile(new Vector3Int(i+left, j+down, 0)) || wallCollider2.HasTile(new Vector3Int(i+left, j+down, 0)) || waterCollider.HasTile(new Vector3Int(i+left,j+down,0)))
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
    public void ResetMapOfTakenPoints()
    {
        mapOfTakenPoints = new int[Map.Count,Map[0].Count];
    }
    public List<int> RandomIds(int max, int ammount)
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
    public Point CheckWhatChankWithWallCorrection(float x, float y)
    {
        float a = x / 2.56f, b = y / 2.56f;
        if (x < 0 && y < 0)
        {
            a = (x / 2.56f) - 1;
            b = (y / 2.56f) - 1;
        }
        else if (x < 0 && y >= 0)
        {
            a = (x / 2.56f) - 1;
        }
        else if (x >= 0 && y < 0)
        {
            b = (y / 2.56f) - 1;
        }
        Point point = new Point((int)a, (int)b);

        if (Map[point.X + 27][-point.Y + 31] == 1)
        {
            return point;
        }
        else
        {
            float xPlane = x % 2.56f;
            float yPlane = y % 2.56f;
            if(xPlane >= 1.28f)
            {
                if(point.X +27+1 < Map.Count)
                {
                    if (Map[point.X + 27 + 1][-point.Y + 31] == 1)
                    {
                        point.X++;
                        return point;
                    }
                }
                
            }
            else if (xPlane <= 1.28f)
            {
                if (Map[point.X + 27 - 1][-point.Y + 31] == 1)
                {
                    point.X--;
                    return point;
                }
            }
            if (yPlane >= 1.28f)
            {
                if (Map[point.X + 27][-point.Y + 31 + 1] == 1)
                {
                    point.Y++;
                    return point;
                }
            }
            else if (yPlane <= 1.28f)
            {
                if (Map[point.X + 27][-point.Y + 31 - 1] == 1)
                {
                    point.Y--;
                    return point;
                }
            }
            if(xPlane >= 1.28f && yPlane >= 1.28f)
            {
                point.Y++;
                point.X++;
                return point;
            }
            else if (xPlane >= 1.28f && yPlane <= 1.28f)
            {
                point.Y--;
                point.X++;
                return point;
            }
            else if (xPlane <= 1.28f && yPlane <= 1.28f)
            {
                point.Y--;
                point.X--;
                return point;
            }
            else if (xPlane <= 1.28f && yPlane >= 1.28f)
            {
                point.Y++;
                point.X--;
                return point;
            }
            return point;
        }
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
    public class AStarPoint
    {
        public int X;
        public int Y;
        public int G;
        public int H;
        public int J;
        public int F => G + H + J;
        public AStarPoint Parent;
        public AStarPoint(int x, int y)
        {
            X = x;
            Y = y;
            G = 0;
            H = 0;
            J = 0;
            Parent = null;
        }
    }
    public AStarPoint AStarAlgoritm(Point Start, Point End)
    {

        AStarPoint currentPoint = null;
        List<AStarPoint> toBeEvaluated = new List<AStarPoint>();
        List<AStarPoint> Evaluated = new List<AStarPoint>();
        toBeEvaluated.Add(new AStarPoint(Start.X,Start.Y));
        while(true)
        {
            if(toBeEvaluated.Count == 0)
            {
                return currentPoint;
            }
            currentPoint = toBeEvaluated.OrderBy(p=>p.F).FirstOrDefault();
            toBeEvaluated.Remove(currentPoint);
            Evaluated.Add(currentPoint);

            if (currentPoint.X == End.X && currentPoint.Y == End.Y)
            {
                return currentPoint;
            }

            foreach (Point neigh in neighboursfourteen)
            {
                if (Map[currentPoint.X + 27 - neigh.X][-currentPoint.Y + 31 + neigh.Y] == 0 || Evaluated.Exists(p => (p.X == currentPoint.X - neigh.X && p.Y == currentPoint.Y - neigh.Y)))
                {

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
    public AStarPoint AStarAlgoritmUpgraded(Point Start, Point End)
    {

        AStarPoint currentPoint = null;
        List<AStarPoint> toBeEvaluated = new List<AStarPoint>();
        List<AStarPoint> Evaluated = new List<AStarPoint>();
        toBeEvaluated.Add(new AStarPoint(Start.X, Start.Y));
        while (true)
        {
            if (toBeEvaluated.Count == 0)
            {
                AStarPoint fastPoint = currentPoint;
                while(fastPoint.Parent != null)
                {
                    mapOfTakenPoints[fastPoint.X + 27, -fastPoint.Y + 31] += 600;
                    fastPoint = fastPoint.Parent;
                }
                return currentPoint;
            }
            currentPoint = toBeEvaluated.OrderBy(p => p.F).FirstOrDefault();
            toBeEvaluated.Remove(currentPoint);
            Evaluated.Add(currentPoint);

            if (currentPoint.X == End.X && currentPoint.Y == End.Y)
            {
                AStarPoint fastPoint = currentPoint;
                while (fastPoint.Parent != null)
                {
                    mapOfTakenPoints[fastPoint.X + 27, -fastPoint.Y + 31] += 600;
                    fastPoint = fastPoint.Parent;
                }
                return currentPoint;
            }

            foreach (Point neigh in neighboursfourteen)
            {
                if (Map[currentPoint.X + 27 - neigh.X][-currentPoint.Y + 31 + neigh.Y] == 0 || Evaluated.Exists(p => (p.X == currentPoint.X - neigh.X && p.Y == currentPoint.Y - neigh.Y)))
                {

                }

                else if (!(toBeEvaluated.Exists(p => (p.X == currentPoint.X - neigh.X && p.Y == currentPoint.Y - neigh.Y))) || (toBeEvaluated.Find(p => (p.X == currentPoint.X - neigh.X && p.Y == currentPoint.Y - neigh.Y)).G > currentPoint.G + 14))
                {
                    AStarPoint newPoint = new AStarPoint(currentPoint.X - neigh.X, currentPoint.Y - neigh.Y);
                    newPoint.G = currentPoint.G + 14;
                    newPoint.H = CalculateDistance(newPoint.X, newPoint.Y, End.X, End.Y);
                    newPoint.J = mapOfTakenPoints[newPoint.X + 27, - newPoint.Y + 31];
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
    public Point CreatePatrol(float x, float y)
    {
        Point start = CheckWhatChank(x, y);
        Point end = new Point(0,0);
        bool correctEnd = false;
        while(correctEnd == false)
        {
            int randomX = Random.Range(5, 20);
            int randomY = Random.Range(5, 20);
            int xSign = Random.Range(0, 2);
            int ySign = Random.Range(0, 2);
            switch(xSign)
            {
                case 0:
                    if(ySign==0)
                    {
                        end.X = start.X - randomX;
                        end.Y = start.Y - randomY;
                    }
                    else
                    {
                        end.X = start.X - randomX;
                        end.Y = start.Y + randomY;
                    }
                    break;
                case 1:
                    if (ySign == 0)
                    {
                        end.X = start.X + randomX;
                        end.Y = start.Y - randomY;
                    }
                    else
                    {
                        end.X = start.X + randomX;
                        end.Y = start.Y + randomY;
                    }
                    break;
            }
            if(end.X + 27 >= 0 && end.X + 27 <= 47 && -end.Y + 31 >= 0 && -end.Y + 31 <= 26)
            {
                if (Map[end.X + 27][-end.Y + 31] == 1)
                {
                    correctEnd = true;
                }
            }
            
        }
        return end;
    }

    public void Resume()
    {
        parentOfPause.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        AudioManager.instance.PlaySound("SoundClick");
    }

    public void Pause()
    {
        parentOfPause.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    
    public void LoseGame()
    {
        Time.timeScale = 1f;
        GlobalData.numberOfRobots = activePlayer.collectedRobots;
        GlobalData.time = Timer.instance.getTimer();
        SceneManager.LoadScene(2);
    }
    void Start()
    {
        AudioManager.instance.PlaySound("SoundGame");
        playerVisibilityChanged?.Invoke("green");
        playerFound = false;
        EnemySeesPlayer = false;
        neighboursfourteen.Add(new Point(1, 1));
        neighboursfourteen.Add(new Point(1, -1));
        neighboursfourteen.Add(new Point(-1, 1));
        neighboursfourteen.Add(new Point(-1, -1));
        neighboursten.Add(new Point(1, 0));
        neighboursten.Add(new Point(-1, 0));
        neighboursten.Add(new Point(0, 1));
        neighboursten.Add(new Point(0, -1));
        string a = "";
        List<int> positionsOfRobots = RandomIds(23, numberOfRobots);
        List<int> positionsOfEnemies = RandomIds(9, numberOfEnemy);
        for (int i = 0; i < positionsOfRobots.Count; i++)
        {
            Robot robot = Instantiate(robotPrefab, new Vector3(possibleRobots[positionsOfRobots[i]].x, possibleRobots[positionsOfRobots[i]].y, 0), Quaternion.identity).GetComponent<Robot>();
            listOfRobots.Add(robot);
        }
        for (int i = 0; i < positionsOfEnemies.Count; i++)
        {
            Enemy enemy = Instantiate(enemyPrefab, new Vector3(possibleEnemies[positionsOfEnemies[i]].x, possibleEnemies[positionsOfEnemies[i]].y, 0), Quaternion.identity).GetComponent<Enemy>();
            listOfEnemies.Add(enemy);
        }
        CreateMap(21, 32, -27, -3);
        ResetMapOfTakenPoints();
        for (int i = 0; i < Map[0].Count - 1; i++)
        {
            for (int j = 0; j < Map.Count; j++)
            {
                if (Map[j][i] == 1)
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

        foreach (Enemy enemy in listOfEnemies)
        {
            Point end = CreatePatrol(enemy.transform.position.x,enemy.transform.position.y);
            Point start = CheckWhatChank(enemy.transform.position.x, enemy.transform.position.y);
            AStarPoint road = AStarAlgoritm(start, end);
            List<Vector3> path = new List<Vector3>();
            while (road.Parent != null)
            {

                path.Add(new Vector3(road.X, road.Y, 0));

                road = road.Parent;
            }
            path.Reverse();
            enemy.patrolPath = path;
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused)
            {
                Resume();
            }
            else
            {           
                Pause();
            }
        }

        foreach(Enemy enemy in listOfEnemies)
        {
            if(enemy.canSeePlayer == true)
            {
                EnemySeesPlayer = true;
            }
        }
        if(EnemySeesPlayer==true)
        {
            timerPathToPlayer += Time.deltaTime;
            if(timerPathToPlayer>waitTimeToPlayer)
            {
                playerVisibilityChanged?.Invoke("red");
                playerFound = true;
                ResetMapOfTakenPoints();

                foreach (Enemy enemy in listOfEnemies)
                {
                    Point start = CheckWhatChankWithWallCorrection(enemy.transform.position.x, enemy.transform.position.y);
                    Point end = CheckWhatChankWithWallCorrection(activePlayer.transform.position.x, activePlayer.transform.position.y);
                    AStarPoint road = AStarAlgoritmUpgraded(start, end);
                    List<Vector3> path = new List<Vector3>();
                    while (road.Parent != null)
                    {
                        path.Add(new Vector3(road.X, road.Y, 0));
                        road = road.Parent;
                    }
                    enemy.PathToPlayer = path;
                }
                EnemySeesPlayer = false;
                timerPlayerLost = 0.0f;
                timerPathToPlayer = 0.0f;
            }
        }
        else
        {
            if(playerFound == true)
            {
                playerVisibilityChanged?.Invoke("yellow");
                timerPlayerLost += Time.deltaTime;
                if(timerPlayerLost > waitTimePlayerLost)
                {
                    playerFound = false;
                    foreach(Enemy enemy in listOfEnemies)
                    {
                        enemy.returningToPatrol = true;
                        Point start = CheckWhatChankWithWallCorrection(enemy.transform.position.x, enemy.transform.position.y);
                        Point end = new Point((int)enemy.nextMovePatrol.x, (int)enemy.nextMovePatrol.y);
                        AStarPoint road = AStarAlgoritm(start, end);
                        List<Vector3> path = new List<Vector3>();
                        while (road.Parent != null)
                        {
                            path.Add(new Vector3(road.X, road.Y, 0));
                            road = road.Parent;
                        }
                        enemy.patrolPathReturn = path;
                    }
                    timerPlayerLost = 0.0f;
                    playerVisibilityChanged?.Invoke("green");
                }
            }
        }
    }
}