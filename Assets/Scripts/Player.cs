using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    public float moveSpeed = 9f;
    public float moveSpeedSprint = 18f;
    public int collectedRobots = 0;
    public Rigidbody2D rigidBody;
    [SerializeField]
    Vector2 movement;
    public static event Action<int> robotsChanged;
    public static event Action<float> staminaChanged;
    public static event Action<bool> exhaustedChanged;
    public Animator animator;
    public int stamina;
    public bool exhausted;
    public float exhaustTime;
    public float timer;
    public float maxStamina;
    public float weightSlow;
    
    
    void Start()
    {
        weightSlow = 1f;
        robotsChanged?.Invoke(0);
        moveSpeed = 4f;
        moveSpeedSprint = 8f;
        speed = moveSpeed;
        stamina = 200;
        exhaustTime = 5f;
        maxStamina = 200f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(MapGenerator.instance.gameIsPaused)
        {
            return;
        }
        //input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.x = movement.x * 1.5f;

        if(Input.GetAxisRaw("Horizontal")!=0 && Input.GetAxisRaw("Vertical") != 0)
        {
            movement.x = movement.x / 1.5f;
            movement.y = movement.y / 1.5f;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(stamina>0)
            {
                speed = moveSpeedSprint;
            } 
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = moveSpeed;
        }
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        
    }
    private void FixedUpdate()
    {
        if (speed == moveSpeedSprint)
        {
            if (exhausted)
            {
                speed = moveSpeed;
            }
            else if (stamina > 0)
            {
                stamina--;
                staminaChanged?.Invoke(stamina / maxStamina);
                if (stamina == 0)
                {
                    exhaustedChanged?.Invoke(true);
                    exhausted = true;
                    timer = 0.0f;
                }
            }
            else
            {
                speed = moveSpeed;
            }
        }
        if (speed == moveSpeed)
        {
            if (stamina < maxStamina)
            {
                stamina++;
                staminaChanged?.Invoke(stamina / maxStamina);
            }
        }
        if (exhausted)
        {
            timer += Time.deltaTime;
            if (timer > exhaustTime)
            {
                exhaustedChanged?.Invoke(false);
                exhausted = false;
            }
        }
        //movement
        rigidBody.MovePosition(rigidBody.position + movement * speed * Time.fixedDeltaTime * weightSlow);
    }
    public void ChangeSpeed(float speed)
    {
        weightSlow -= speed;
    }
    public void ChangeRobots()
    {

        collectedRobots++;
        if(collectedRobots<MapGenerator.instance.numberOfRobots)
        {
            AudioManager.instance.PlaySound("SoundRobot");
        }
        robotsChanged?.Invoke(collectedRobots);
        staminaChanged?.Invoke(stamina / maxStamina);
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
