using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Observer : MonoBehaviour
{
    public TMP_Text robotsText;

    private void OnEnable()
    {
        Player.robotsChanged += UpdateRobots;
    }
    private void OnDisable()
    {
        Player.robotsChanged -= UpdateRobots;
    }
    public void UpdateRobots(int ammount)
    {
        robotsText.text = ammount + "/"+ MapGenerator.instance.numberOfRobots;
        if (ammount == MapGenerator.instance.numberOfRobots)
        {
            GlobalData.time = Timer.instance.getTimer();
            SceneManager.LoadScene(3);
        }
    }
    
}
