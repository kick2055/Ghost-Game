using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;
    public int collectedRobots = 0;
    public Rigidbody2D rigidBody;
    Vector2 movement;
    public static event Action<int> soulsChanged;
    
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if(Input.GetKey(KeyCode.Escape)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }

    private void FixedUpdate()
    {
        //movement
        rigidBody.MovePosition(rigidBody.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    public void ChangeSpeed(float speed)
    {
        moveSpeed += speed;
    }
    public void ChangeSouls()
    {
        collectedRobots++;
        soulsChanged?.Invoke(collectedRobots);
    }
}
