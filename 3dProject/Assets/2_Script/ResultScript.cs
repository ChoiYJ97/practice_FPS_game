using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScript : MonoBehaviour
{
    public Text Scoretxt;
    int Score;
    float start;
    // Start is called before the first frame update
    void Start()
    {
        Score = SceneManagerScript._instance.GetScore();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        start = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (start <= Score)
            start += (Time.deltaTime * Score)/3;
        int txt = (int)start;
        Scoretxt.text = txt.ToString();
    }
}
