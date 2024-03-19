using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    private bool collected = false;
    public Arrow arrow;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Player player;
        player = collision.gameObject.GetComponent<Player>();
        if (player != null && !collected)
        {
            if(MapGenerator.instance.detectionActivated)
            {
                collected = true;
                player.ChangeSpeed(0.05f);
                player.ChangeRobots();
                SuperPowerManager.instance.resetArrows();
                MapGenerator.instance.listOfRobots.Remove(this);
                Destroy(gameObject);
                SuperPowerManager.instance.UseDetect('a');
            }
            else
            {
                collected = true;
                player.ChangeSpeed(0.05f);
                player.ChangeRobots();
                MapGenerator.instance.listOfRobots.Remove(this);
                Destroy(gameObject);
            }
        }
    }
}
