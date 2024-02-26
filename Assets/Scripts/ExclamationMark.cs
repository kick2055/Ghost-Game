using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExclamationMark : MonoBehaviour
{
    public Image image;
    private void OnEnable()
    {
        MapGenerator.playerVisibilityChanged += UpdateColorExclamation;
    }
    private void OnDisable()
    {
        MapGenerator.playerVisibilityChanged -= UpdateColorExclamation;
    }
    public void UpdateColorExclamation(string color)
    {
        if (color == "red")
        {
            image.color = Color.red;
        }
        else if(color == "yellow")
        {
            image.color = Color.yellow;
        }
        else
        {
            image.color = Color.green;
        }
    }
}
