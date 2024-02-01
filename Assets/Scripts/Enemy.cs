using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector2 position;
    public float moveSpeed = 9f;
    public Rigidbody2D rigidBody;
    public Vector3 direction;
    public int a = 0;
    public List<Vector3> PathToPlayer = new List<Vector3>();
    public List<Vector3> patrolPath = new List<Vector3>();
    public Animator animator;
    public Vector3 nextMove;
    public Point currentChank;
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
        double x_left = middle.x * 2.56f + 1.28f - 0.16f;
        double x_right = middle.x * 2.56f + 1.28f + 0.16f;
        double y_up = middle.y * 2.56f + 1.28f + 0.16f;
        double y_down = middle.y * 2.56f + 1.28f - 0.16f;
        if (transform.position.x <=x_right && transform.position.x >= x_left && transform.position.y >= y_down && transform.position.y <= y_up) 
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
        /*direction.x = 0.005f;
        direction.y = 0.005f;
        direction.z = 0f;*/
        
    }
    void Update()
    {
        currentChank = CheckWhatChank(transform.position.x,transform.position.y);
        if(PathToPlayer.Count>0) { nextMove = PathToPlayer[PathToPlayer.Count-1];  }
        
        if(CheckIfCloseToMiddleOfChank(nextMove))
        {
            if (PathToPlayer.Count>=2)
            {
                PathToPlayer.RemoveAt(PathToPlayer.Count-1);
            }
            
        }
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(nextMove.x*2.56f+1.28f, nextMove.y*2.56f + 1.28f, 0), 2f * Time.deltaTime);
        Debug.Log("aktualna pozycja: " + transform.position.x + " " + transform.position.y);
        Debug.Log("nastepna pozycja: " + nextMove.x + " " + nextMove.y);
        Debug.Log("aktualny chunk: " + currentChank.X + " " + currentChank.Y);
    }  
}
