using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class YouLose : MonoBehaviour
{
    public TMP_Text score;
    public TMP_Text robots;
    void Start()
    {
        AudioManager.instance.PlaySound("SoundLose");
        float time = GlobalData.time;
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string whatTime = string.Format("{00:00}:{1:00}", minutes, seconds);
        score.text = "Your time: " + whatTime;

        robots.text = ("Robots collected: " + GlobalData.numberOfRobots);
    }

    public void Exit()
    {
        AudioManager.instance.PlaySound("SoundClick");
        SceneManager.LoadScene(0);
    }

}
