using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private Vector2 position;
    public float moveSpeed = 0.000001f;
    public Rigidbody2D rigidBody;
    public Vector3 direction;
    public int a = 0;
    public List<Vector3> PathToPlayer = new List<Vector3>();
    public List<Vector3> patrolPath = new List<Vector3>();
    public List<Vector3> patrolPathReturn = new List<Vector3>();
    public Animator animator;
    public Vector3 nextMove;
    public Vector3 nextMovePatrol;
    public Point currentChank;
    public float startTime;
    public float journeyLength;
    public float speed;
    private bool patrolForward = true;
    public int patrolCounter = -1;
    public int layerMask;
    public RaycastHit2D hit;
    public bool canSeePlayer = false;
    public bool returningToPatrol = false;

    public Point CheckWhatChank(float x, float y)
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
        return new Point((int)a, (int)b);
    }
    public bool CheckIfCloseToMiddleOfChank(Vector3 middle)
    {
        double errorMargin = 0.16f;
        double xLeft = middle.x * 2.56f + 1.28f - errorMargin;
        double xRight = middle.x * 2.56f + 1.28f + errorMargin;
        double yUp = middle.y * 2.56f + 1.28f + errorMargin;
        double yDown = middle.y * 2.56f + 1.28f - errorMargin;
        if (transform.position.x <xRight && transform.position.x > xLeft && transform.position.y > yDown && transform.position.y < yUp) 

        { return true; }
        else { return false; }
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

    public double CalculateDistance(float StartX, float StartY, float EndX, float EndY)
    {
        double exit = Mathf.Sqrt(Mathf.Pow((10 * StartX) - (10 * EndX), 2) + Mathf.Pow((10 * StartY) - (10 * EndY), 2));
        return exit;
    }
    void Start()
    {
        startTime = Time.time;
        journeyLength = 5000f;
        /*direction.x = 0.005f;
        direction.y = 0.005f;
        direction.z = 0f;*/
        layerMask = 72;

    }
    void Update()
    {
        if(CalculateDistance(transform.position.x,transform.position.y, MapGenerator.instance.activePlayer.transform.position.x, MapGenerator.instance.activePlayer.transform.position.y) <= 16)
        {
            SceneManager.LoadScene(2);
        }
        Vector2 rayDirection = MapGenerator.instance.activePlayer.transform.position - transform.position;
        rayDirection.y -= 0.8f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, 9999, layerMask);
        Vector2 raycastDirection = hit.point - new Vector2(transform.position.x,transform.position.y);
        Debug.DrawRay(transform.position, raycastDirection, Color.red);
        if (hit.collider != null)
        {
            if(hit.collider.gameObject.CompareTag("Player"))
            {
                canSeePlayer = true;
            }
            else
            {
                canSeePlayer = false;
            }
        }
        if (MapGenerator.instance.playerFound == false)
        {
            if(returningToPatrol == false)
            {
                if (patrolForward == true)
                {
                    if (patrolCounter + 1 < patrolPath.Count)
                    {
                        nextMovePatrol = patrolPath[patrolCounter + 1];
                    }
                    else
                    {
                        patrolForward = !patrolForward;
                        nextMovePatrol = patrolPath[patrolCounter - 1];
                    }
                }
                else
                {
                    if (patrolCounter - 1 > 0)
                    {
                        nextMovePatrol = patrolPath[patrolCounter - 1];
                    }
                    else
                    {
                        patrolForward = !patrolForward;
                        nextMovePatrol = patrolPath[patrolCounter + 1];
                    }
                }
                if (CheckIfCloseToMiddleOfChank(nextMovePatrol))
                {
                    if (patrolForward)
                    {
                        patrolCounter++;
                    }
                    else
                    {
                        patrolCounter--;
                    }
                }
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(nextMovePatrol.x * 2.56f + 1.28f, nextMovePatrol.y * 2.56f + 1.28f, 0), moveSpeed * Time.deltaTime * 2);
            }
            else
            {
                if (patrolPathReturn.Count > 0) { nextMove = patrolPathReturn[patrolPathReturn.Count - 1]; }

                if (CheckIfCloseToMiddleOfChank(nextMove))
                {
                    if (patrolPathReturn.Count >= 2)
                    {
                        patrolPathReturn.RemoveAt(patrolPathReturn.Count - 1);
                    }
                }
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(nextMove.x * 2.56f + 1.28f, nextMove.y * 2.56f + 1.28f, 0), moveSpeed * Time.deltaTime * 2);
                if (CheckIfCloseToMiddleOfChank(nextMovePatrol))
                {
                    returningToPatrol = false;
                }
            }
            
        }
        else
        {
            if(hit.collider.gameObject.CompareTag("Player") && CalculateDistance(transform.position.x, transform.position.y, MapGenerator.instance.activePlayer.transform.position.x, MapGenerator.instance.activePlayer.transform.position.y) <= 64)
            {
                transform.position = Vector3.MoveTowards(transform.position, MapGenerator.instance.activePlayer.transform.position, moveSpeed * Time.deltaTime * 2);
            }
            else
            {
                if (PathToPlayer.Count > 0) { nextMove = PathToPlayer[PathToPlayer.Count - 1]; }

                if (CheckIfCloseToMiddleOfChank(nextMove))
                {
                    if (PathToPlayer.Count >= 2)
                    {
                        PathToPlayer.RemoveAt(PathToPlayer.Count - 1);
                    }
                }
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(nextMove.x * 2.56f + 1.28f, nextMove.y * 2.56f + 1.28f, 0), moveSpeed * Time.deltaTime * 2);
            }
            

            
            //transform.position = Vector3.Lerp(transform.position, new Vector3(nextMove.x * 2.56f + 1.28f, nextMove.y * 2.56f + 1.28f, 0), fractionOfJourney);
            //transform.Translate(Time.deltaTime * moveSpeed, Time.deltaTime * moveSpeed, 0);
        }
    }  
}
