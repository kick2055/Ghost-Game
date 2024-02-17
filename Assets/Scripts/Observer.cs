using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Observer : MonoBehaviour
{
    public TMP_Text robotsText;
    public TMP_Text dangedText;
    public TMP_Text heartText;

    private void OnEnable()
    {
        Player.soulsChanged += UpdateRobots;
    }
    private void OnDisable()
    {
        Player.soulsChanged -= UpdateRobots;
    }
    private void UpdateRobots(int ammount)
    {
        robotsText.text = ammount + "/9 robots";
        if (ammount == MapGenerator.instance.numberOfRobots)
        {
            SceneManager.LoadScene(3);
        }
    }
    
}
