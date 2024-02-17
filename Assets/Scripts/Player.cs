using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed = 9f;
    public int collectedRobots = 0;
    public Rigidbody2D rigidBody;
    [SerializeField]
    Vector2 movement;
    public static event Action<int> soulsChanged;
    public Animator animator;
    
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.x = movement.x * 1.5f;

        if(Input.GetAxisRaw("Horizontal")!=0 && Input.GetAxisRaw("Vertical") != 0)
        {
            movement.x = movement.x / 1.5f;
            movement.y = movement.y / 1.5f;
        }
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);



        if(Input.GetKey(KeyCode.Escape)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        //Debug.Log(CheckWhatChank(transform.position.x, transform.position.y).x + " " + CheckWhatChank(transform.position.x, transform.position.y).y);
    }

    private void FixedUpdate()
    {
        //movement
        rigidBody.MovePosition(rigidBody.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    public void ChangeSpeed(float speed)
    {
        moveSpeed -= speed;
    }
    public void ChangeSouls()
    {

        collectedRobots++;
        soulsChanged?.Invoke(collectedRobots);
    }
    public Vector3 CheckWhatChank(float x, float y)
    {
        float a = 0, b = 0;
        if (x < 0 && y < 0)
        {
            a = (x / 2.56f) - 1;
            b = (y / 2.56f) - 1;
        }
        else if (x < 0 && y >= 0)
        {
            a = (x / 2.56f) - 1;
            b = y / 2.56f;
        }
        else if (x >= 0 && y < 0)
        {
            a = x / 2.56f;
            b = (y / 2.56f) - 1;
        }
        else
        {
            a = x / 2.56f;
            b = y / 2.56f;
        }
        return new Vector3(a - (a % 1), b - (b % 1), 0);
    }
}
