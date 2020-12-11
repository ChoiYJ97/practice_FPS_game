using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#pragma warning disable CS0649
#pragma warning disable CS0414

public class SceneManagerScript : MonoBehaviour
{
    int Score;
    static int Hard;
    static SceneManagerScript _uniqueinstance;
    public static SceneManagerScript _instance
    {
        get { return _uniqueinstance; }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _uniqueinstance = this;
        Lobby_Game();
    }

    void Start()
    {
        Score = 0;
        Hard = 0;
        Screen.SetResolution(1024, 768, true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit_Game();
        }
    }
    public void Lobby_Game()
    {
        SceneManager.LoadScene("LobbyScene");
    }
    public void Story_mode()
    {
        SceneManager.LoadScene("StoryMode");
    }
    public void DeathMatch_mode_Normal()
    {
        Hard = 0;
        Debug.Log("난이도 " + Hard);
        SceneManager.LoadScene("DeathMatchMode");
    }
    public void DeathMatch_mode_Hard()
    {
        Hard = 1;
        Debug.Log("난이도 " + Hard);
        SceneManager.LoadScene("DeathMatchMode");
    }
    public void RestartDeathMatchMode()
    {
        SceneManager.LoadScene("DeathMatchMode");
    }
    public void Tutorial_mode()
    {
        SceneManager.LoadScene("TutorialScene");
    }
    public void Result_Scene()
    {
        SceneManager.LoadScene("ResultScene");
    }
    public void Exit_Game()
    {
#if UNITY_EDITOR
        //에디터에 play버튼을 끄는 역활
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //빌드에서는 가능하지만 에디터에서는 안된다.
        Application.Quit();
#endif

    }

    public void Scoresave(int score)
    {
        if (Score < score)
            Score = score;
        else
            return;
    }
    public int GetScore()
    {
        return Score;
    }

    public int currDifficulty()
    {
         return Hard;
    }
}
