using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighscoreTable : MonoBehaviour
{
    [HideInInspector]
    public static HighscoreTable instance;
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreTransformList;
    public GameObject board;

    private void Awake()
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


        
        entryContainer = transform.Find("HSContainer");
        entryTemplate = entryContainer.Find("HSTemplate");

        entryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);


        if (highscores != null)
        {
            for (int i = 0; i < highscores.highscoreList.Count; i++)
            {
                for (int j = i + 1; j < highscores.highscoreList.Count; j++)
                {
                    if (highscores.highscoreList[j].score < highscores.highscoreList[i].score)
                    {

                        Highscore tmp = highscores.highscoreList[i];
                        highscores.highscoreList[i] = highscores.highscoreList[j];
                        highscores.highscoreList[j] = tmp;
                    }
                }
            }

            highscoreTransformList = new List<Transform>();
            int counter = 0;
            foreach (Highscore highscore in highscores.highscoreList)
            {
                if(counter<10)
                {
                    CreateHighscoreTransform(highscore, entryContainer, highscoreTransformList);
                }
                counter++;
            }
        }
        board.SetActive(false);
    }

    private void CreateHighscoreTransform(Highscore highscore, Transform container, List<Transform> transformList)
    {
        float templateHeight = 31f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("Position").GetComponent<TMP_Text>().text = rankString;

        int score = highscore.score;

        entryTransform.Find("Score").GetComponent<TMP_Text>().text = score.ToString();

        string name = highscore.name;
        entryTransform.Find("Nick").GetComponent<TMP_Text>().text = name;



        if (rank == 1)
        {
            entryTransform.Find("Position").GetComponent<TMP_Text>().color = Color.green;
            entryTransform.Find("Score").GetComponent<TMP_Text>().color = Color.green;
            entryTransform.Find("Nick").GetComponent<TMP_Text>().color = Color.green;
        }

        

        transformList.Add(entryTransform);
    }

    public void AddHighscore(int score, string name)
    {

        Highscore highscore = new Highscore { score = score, name = name };


        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {

            highscores = new Highscores()
            {
                highscoreList = new List<Highscore>()
            };
        }


        highscores.highscoreList.Add(highscore);


        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private class Highscores
    {
        public List<Highscore> highscoreList;
    }


    [System.Serializable]
    private class Highscore
    {
        public int score;
        public string name;
    }

    public void Reset()
    {
        if(highscoreTransformList != null)
        {
            foreach (Transform highscore in highscoreTransformList)
            {
                highscore.gameObject.SetActive(false);
            }
        }
        

        AudioManager.instance.PlaySound("SoundClick");
        PlayerPrefs.DeleteAll();
    }

}
