using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.PlaySound("SoundMenu");
    }
    public void play_game()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void quit_game()
    {
        Application.Quit();
    }
    public void Leaderboard()
    {
        AudioManager.instance.PlaySound("SoundClick");
    }
}
