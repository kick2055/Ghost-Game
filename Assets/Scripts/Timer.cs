using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private static float timer = 0f;
    [SerializeField]
    public TextMeshProUGUI timeUI;
    public static Timer instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
            return;
        }
    }
    void Start()
    {
        ResetTimer();
    }

    public float getTimer()
    {
        return timer;
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
    public void UpdateTimerDisplay(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string whatTime = string.Format("{00:00}:{1:00}", minutes, seconds);
        Debug.Log(whatTime);
        timeUI.text = whatTime.ToString();
    }
}
