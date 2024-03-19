using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTimer : MonoBehaviour
{
    public FreezeTimer()
    {

    }
    private void Start()
    {
        MapGenerator.instance.freezeCounter = 0f;
    }
    void Update()
    {
        if (MapGenerator.instance.freezeCounter < MapGenerator.instance.freezeTime)
        {
            MapGenerator.instance.freezeCounter += Time.deltaTime;
            if(MapGenerator.instance.freezeCounter >= MapGenerator.instance.freezeTime)
            {
                Destroy(gameObject);
            }
        }
    }
}
