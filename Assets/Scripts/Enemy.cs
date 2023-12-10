using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector2 position;
    public float moveSpeed = 9f;
    //public Rigidbody2D rigidBody;
    //[SerializeField]
    //public Animator animator;
    public Rigidbody2D rigidBody;
    public Vector3 vec3;
    public int a = 0;
    
    void Start()
    {
        vec3.x = 0.005f;
        vec3.y = 0.005f;
        vec3.z = 0f;
    }

    void Update()
    {
        
        transform.position = transform.position + vec3;
        a++;
        if(a>=1000)
        {
            a = 0;
            vec3 *= -1;
            //vec3.x = vec3.x * -1;
            //vec3.y = vec3.y * -1;
        }
       
        
    }
}
