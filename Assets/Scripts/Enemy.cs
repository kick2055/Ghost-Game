using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector2 position;
    public float moveSpeed = 0.000001f;
    public Rigidbody2D rigidBody;
    public Vector3 direction;
    public int a = 0;
    public List<Vector3> PathToPlayer = new List<Vector3>();
    public List<Vector3> patrolPath = new List<Vector3>();
    public Animator animator;
    public Vector3 nextMove;
    public Point currentChank;
    public float startTime;
    public float journeyLength;
    public float speed;
    private bool playerFound = false;
    private bool patrolForward = true;
    public int patrolCounter = -1;

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

    void Start()
    {
        
        startTime = Time.time;
        journeyLength = 5000f;
        /*direction.x = 0.005f;
        direction.y = 0.005f;
        direction.z = 0f;*/

    }
    void Update()
    {
        if(MapGenerator.instance.playerFound == true)
        {
            if(patrolForward == true)
            {
                if(patrolCounter + 1 < patrolPath.Count)
                {
                    nextMove = patrolPath[patrolCounter + 1];
                }
                else
                {
                    patrolForward = !patrolForward;
                    nextMove = patrolPath[patrolCounter - 1];
                }
                
            }
            else
            {
                if (patrolCounter - 1 > 0)
                {
                    nextMove = patrolPath[patrolCounter - 1];
                }
                else
                {
                    patrolForward = !patrolForward;
                    nextMove = patrolPath[patrolCounter + 1];
                }
            }
            if (CheckIfCloseToMiddleOfChank(nextMove))
            {
                if(patrolForward)
                {
                    patrolCounter++;
                }
                else
                {
                    patrolCounter--;
                }
                
            }
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(nextMove.x * 2.56f + 1.28f, nextMove.y * 2.56f + 1.28f, 0), moveSpeed * Time.deltaTime * 2);
        }
        else
        {
            Debug.Log("Z ENEMY: " + (nextMove.x * 2.56f + 1.28f) + "  " + (nextMove.y * 2.56f + 1.28f));
            if (PathToPlayer.Count > 0) { nextMove = PathToPlayer[PathToPlayer.Count - 1]; }

            if (CheckIfCloseToMiddleOfChank(nextMove))
            {
                //startTime = Time.time;
                if (PathToPlayer.Count >= 2)
                {
                    PathToPlayer.RemoveAt(PathToPlayer.Count - 1);
                }
            }
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(nextMove.x * 2.56f + 1.28f, nextMove.y * 2.56f + 1.28f, 0), moveSpeed * Time.deltaTime * 2);
            //transform.position = Vector3.Lerp(transform.position, new Vector3(nextMove.x * 2.56f + 1.28f, nextMove.y * 2.56f + 1.28f, 0), fractionOfJourney);
            //transform.Translate(Time.deltaTime * moveSpeed, Time.deltaTime * moveSpeed, 0);
        }
    }  
}
