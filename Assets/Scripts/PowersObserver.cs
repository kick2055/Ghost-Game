using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowersObserver : MonoBehaviour
{
    public Image hologramOne;
    public Image camuOne;
    public Image freezeOne;
    public Image direOne;
    public Image bulletOne;
    public Image hologramTwo;
    public Image camuTwo;
    public Image freezeTwo;
    public Image direTwo;
    public Image bulletTwo;

    private void OnEnable()
    {
        Player.powerChanged += UpdatePowers;
        SuperPowerManager.powerUsed += UsePower;
    }
    private void OnDisable()
    {
        Player.powerChanged -= UpdatePowers;
        SuperPowerManager.powerUsed -= UsePower;
    }
    public void UpdatePowers(int[] informations)
    {
        if(informations[0]==0)
        {
            switch (informations[1])
            {
                case 1:
                    camuOne.gameObject.SetActive(true);
                    break;
                case 2:
                    bulletOne.gameObject.SetActive(true);
                    break;
                case 3:
                    hologramOne.gameObject.SetActive(true);
                    break;
                case 4:
                    freezeOne.gameObject.SetActive(true);
                    break;
                case 5:
                    direOne.gameObject.SetActive(true);
                    break;
            }
        }
        else
        {
            switch (informations[1])
            {
                case 1:
                    camuTwo.gameObject.SetActive(true);
                    break;
                case 2:
                    bulletTwo.gameObject.SetActive(true);
                    break;
                case 3:
                    hologramTwo.gameObject.SetActive(true);
                    break;
                case 4:
                    freezeTwo.gameObject.SetActive(true);
                    break;
                case 5:
                    direTwo.gameObject.SetActive(true);
                    break;
            }
        }
    }
    public void UsePower(bool first)
    {
        if(first)
        {
            camuOne.gameObject.SetActive(false);
            bulletOne.gameObject.SetActive(false);
            hologramOne.gameObject.SetActive(false);
            freezeOne.gameObject.SetActive(false);
            direOne.gameObject.SetActive(false);
}
        else
        {
            camuTwo.gameObject.SetActive(false);
            bulletTwo.gameObject.SetActive(false);
            hologramTwo.gameObject.SetActive(false);
            freezeTwo.gameObject.SetActive(false);
            direTwo.gameObject.SetActive(false);
        }
    }
}
