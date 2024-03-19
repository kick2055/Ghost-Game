using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hologram : MonoBehaviour
{
    public float hologramTime;
    public float hologramCounter;
    private void Start()
    {
        hologramTime = 10f;
        hologramCounter = 0f;
    }
    private void Update()
    {
        hologramCounter += Time.deltaTime;
        if(hologramCounter > hologramTime)
        {
            MapGenerator.instance.DestroyHologram();
            Destroy(gameObject);
        }
    }
}
