using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameManager : MonoBehaviour
{
    float timecheck;
    bool Re;

    void Start()
    {
        timecheck = 0;
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

    public void Replay()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1.0f;
    }
}
