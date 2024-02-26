using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWin : MonoBehaviour
{
    public TMP_Text score;
    public TMP_InputField input;
    private void Start()
    {
        AudioManager.instance.PlaySound("SoundWin");
        float time = GlobalData.time;
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string whatTime = string.Format("{00:00}:{1:00}", minutes, seconds);
        score.text = "Your time: " + whatTime ;
    }
    
    public void AcceptName()
    {
        AudioManager.instance.PlaySound("SoundClick");
        HighscoreTable.instance.AddHighscore((int)GlobalData.time, input.text);
        SceneManager.LoadScene(0);
    }
}
