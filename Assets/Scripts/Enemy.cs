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

    void Start()
    {
        /*direction.x = 0.005f;
        direction.y = 0.005f;
        direction.z = 0f;*/
    }
    void Update()
    {  
        /*transform.position = transform.position + direction;
        a++;
        if(a>=1000)
        {
            a = 0;
            direction *= -1;
            //vec3.x = vec3.x * -1;
            //vec3.y = vec3.y * -1;
        }*/

    }
    
}
