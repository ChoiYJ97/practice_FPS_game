using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TDTextScore : MonoBehaviour
{
    int Score;
    public TextMesh Tm;

    void Start()
    {
        Score = 0;
        Tm.text = Score.ToString();
    }

    void Update()
    {
        if (SceneManagerScript._instance != null)
        {
            Score = SceneManagerScript._instance.GetHighScore();
            Tm.text = Score.ToString();
        }
    }
}
