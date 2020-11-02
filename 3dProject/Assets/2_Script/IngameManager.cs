using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IngameManager : MonoBehaviour
{
    Text ScoreText;

    int Score;
    float timecheck;
    bool Re;

    static IngameManager _uniqueinstance;

    public static IngameManager _instance
    {
        get { return _uniqueinstance; }
    }

    void Awake()
    {
        _uniqueinstance = this;
    }

    void Start()
    {
        GameObject go = GameObject.Find("ScoreText");
        ScoreText = go.GetComponent<Text>();

        Score = 0;
        timecheck = 0;
        ScoreText.text = Score.ToString();
        Re = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            Re = true;
        }

        if (Re)
        {           
            timecheck += Time.deltaTime;
            if(timecheck >= 2.0f)
            {
                Replay();
            }
        }
    }
    
    public void addScore()
    {
        Score++;
        ScoreText.text = Score.ToString();
    }

    public void Replay()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1.0f;
    }
}
