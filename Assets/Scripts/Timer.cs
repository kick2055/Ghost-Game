using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private static float timer = 0f;
    [SerializeField]
    private TextMeshProUGUI firstMinute;
    [SerializeField]
    private TextMeshProUGUI secondMinute;
    [SerializeField]
    private TextMeshProUGUI colon;
    [SerializeField]
    private TextMeshProUGUI firstSecond;
    [SerializeField]
    private TextMeshProUGUI secondSecond;
    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            timer += Time.deltaTime;
            UpdateTimerDisplay(timer);
        }
    }
    private void ResetTimer()
    {
        timer = 0f;
    }
    private void UpdateTimerDisplay(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string whatTime = string.Format("{00:00}{1:00}", minutes, seconds);

        firstMinute.text = whatTime[0].ToString();
        secondMinute.text = whatTime[1].ToString();
        firstSecond.text = whatTime[2].ToString();
        secondSecond.text = whatTime[3].ToString();
    }
    public float getTimer()
    {
        return timer;
    }
}
