using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    private Vector2 position;
    private bool collected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Player player;
        player = collision.gameObject.GetComponent<Player>();
        if (player != null && !collected)
        {
            
            collected = true;
            player.ChangeSpeed(0.05f);
            player.ChangeRobots();
            Destroy(gameObject);
        }
    }
}
