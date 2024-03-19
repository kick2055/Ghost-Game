using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPowerManager : MonoBehaviour
{
    public Player activePlayer;
    public GameObject bulletPrefab;
    public Vector3 directionOfBullet;
    public GameObject arrowPrefab, hologramPrefab;
    public List<Arrow> listOfArrows = new List<Arrow>();
    public static SuperPowerManager instance;
    public static event Action<bool> powerUsed;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
            return;
        }
    }

    public void resetArrows()
    {
        foreach(Arrow arrow in listOfArrows)
        {
            Destroy(arrow.gameObject);
        }
        listOfArrows.Clear();
    }
    public void SendSignal(char c)
    {
        if (c == 'z')
        {
            powerUsed?.Invoke(true);
        }
        else
        {
            powerUsed?.Invoke(false);
        }
    }

    public double CalculateDistance(float StartX, float StartY, float EndX, float EndY)
    {
        double exit = Mathf.Sqrt(Mathf.Pow((StartX) - (EndX), 2) + Mathf.Pow((StartY) - (EndY), 2));
        return exit;
    }
    public void UsePower(int i, char c)
    {
        switch (i)
        {
            case 1:
                UseCamouflage(c);
                SendSignal(c);      
                break;
            case 2:
                UseBullet(c);
                SendSignal(c);
                break;
            case 3:
                UseHologram(c);
                SendSignal(c);
                break;
            case 4:
                UseFreeze(c);
                SendSignal(c);
                break;
            case 5:
                UseDetect(c);
                SendSignal(c);
                break;
        }
    }
    public void UseCamouflage(char c)
    {
        MapGenerator.instance.activePlayer.GetComponent<Renderer>().material.color = new Color(0, 51, 0, 80);
        if (c == 'z')
        {
            activePlayer.superPowers[0] = 0;
        }
        else
        {
            activePlayer.superPowers[1] = 0;
        }
        activePlayer.camouflage = true;
    }
    public void UseBullet(char c)
    {
        foreach (Enemy enemy in MapGenerator.instance.listOfEnemies)
        {
            if (CalculateDistance(enemy.transform.position.x, enemy.transform.position.y, activePlayer.transform.position.x, activePlayer.transform.position.y) < 1)
            {
                break;
            }
        }
        Vector3 mousePosition = Input.mousePosition;
        mousePosition += Camera.main.transform.forward * 10f;
        Vector3 aim = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 direction = aim - activePlayer.transform.position;
        direction.Normalize();
        Vector3 bulletSpawn = activePlayer.transform.position + direction * 1.5f;
        directionOfBullet = direction;
        Bullet bullet = Instantiate(bulletPrefab, bulletSpawn, Quaternion.identity).GetComponent<Bullet>();
        if(c == 'z')
        {
            activePlayer.superPowers[0] = 0;
        }
        else
        {
            activePlayer.superPowers[1] = 0;
        }
    }
    public void UseHologram(char c)
    {
        Vector3 chank = activePlayer.CheckWhatChank(activePlayer.transform.position.x, activePlayer.transform.position.y);
        Vector3 hologramPosition;
        hologramPosition.x = chank.x * 2.56f + 1.28f;
        hologramPosition.y = chank.y * 2.56f + 1.28f;
        hologramPosition.z = 0;
        Hologram hologram = Instantiate(hologramPrefab, hologramPosition, Quaternion.identity).GetComponent<Hologram>();
        MapGenerator.instance.CreateHologram(chank);
        if (c == 'z')
        {
            activePlayer.superPowers[0] = 0;
        }
        else
        {
            activePlayer.superPowers[1] = 0;
        }
    }
    public void UseFreeze(char c)
    {
        foreach(Enemy enemy in MapGenerator.instance.listOfEnemies)
        {
            enemy.GetComponent<Renderer>().material.color = new Color(51, 255, 255, 80);
            enemy.GetComponent<Animator>().enabled = false;
        }
        MapGenerator.instance.freezeCounter = 0f;
        if (c == 'z')
        {
            activePlayer.superPowers[0] = 0;
        }
        else
        {
            activePlayer.superPowers[1] = 0;
        }
    }
    public void UseDetect(char c)
    {
        MapGenerator.instance.detectionActivated = true;
        foreach (Robot robot in MapGenerator.instance.listOfRobots)
        {
            Arrow arrow = Instantiate(arrowPrefab, activePlayer.transform.position, Quaternion.identity).GetComponent<Arrow>();
            arrow.assingedRobot = robot;
            listOfArrows.Add(arrow);
        }
        if (c == 'z')
        {
            activePlayer.superPowers[0] = 0;
        }
        else if (c == 'x')
        {
            activePlayer.superPowers[1] = 0;
        }
    }
}
