using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPowerBox : MonoBehaviour
{
    private bool collected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player;
        player = collision.gameObject.GetComponent<Player>();
        if (player != null && !collected)
        {
            AudioManager.instance.PlaySound("SoundRobot");
            collected = true;
            player.GetPower();
            Destroy(gameObject);
        }
    }
}
