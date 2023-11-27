using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    private Vector2 position;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player;
        player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.ChangeSpeed(1);
            player.ChangeSouls();
            Destroy(gameObject);
        }
    }
}
