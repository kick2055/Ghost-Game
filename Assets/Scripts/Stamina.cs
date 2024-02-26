using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public Transform staminaBar;
    public Image image;
    private void OnEnable()
    {
        Player.staminaChanged += UpdateStaminaBar;
        Player.exhaustedChanged += UpdateColorBar;
    }
    private void OnDisable()
    {
        Player.staminaChanged -= UpdateStaminaBar;
        Player.exhaustedChanged -= UpdateColorBar;
    }
    public void UpdateStaminaBar(float stamina)
    {
        Vector3 bar = new Vector3(stamina, 1, 1);
        staminaBar.localScale = bar;
    }
    public void UpdateColorBar(bool exhausted)
    {
        if(exhausted)
        {
            image.color = Color.red;
        }
        else
        {
            image.color = Color.blue;
        }
    }
}
