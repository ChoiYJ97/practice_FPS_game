using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#pragma warning disable CS0649
#pragma warning disable CS0414

public class SceneManagerScript : MonoBehaviour
{
    bool quit, ingame;
    int Score, HighScore;
    static int Hard;

    static SceneManagerScript _uniqueinstance;

    public GameObject Quit;

    [Header("Texts")]
    public GameObject Text_Tut;
    public GameObject Text_Quit;
    public GameObject Text_Mission;

    static int curMode; 
    // 0 = startEmptyScene
    // 1 = Lobby
    // 2 = Tutorial
    // 3 = DeathMatchMode

    public static SceneManagerScript _instance
    {
        get { return _uniqueinstance; }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _uniqueinstance = this;
        Lobby_Game();
        Quit.SetActive(false);
        quit = false;
        ingame = false;
    }

    void Start()
    {     
        Score = 0;
        Hard = 0;
        curMode = 1;
        Screen.SetResolution(1024, 768, true);
    }

    void Update()
    {
        if(curMode == 1)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        if (!ingame)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !quit)
            {
                timescaleControl();
                Quit.SetActive(true);
                QuitWindTextControl();
                quit = true;
            }

            else if (Input.GetKeyDown(KeyCode.Escape) && quit)
            {
                timescaleControl();
                Quit.SetActive(false);
                QuitWindTextControl();
                quit = false;
            }

            else if (Input.GetKeyDown(KeyCode.Return) && quit)
            {
                QuitWindTextControl();
                Quit.SetActive(false);
                if (curMode == 2 || curMode == 3)
                    Lobby_Game();
                else if (curMode == 1)
                    Exit_Game(); 
            }
        }
    }
    public void Lobby_Game()
    {
        ingame = false;
        curMode = 1;
        SceneManager.LoadScene("LobbyScene");
    }
    public void Story_mode()
    {
        SceneManager.LoadScene("StoryMode");
    }
    public void DeathMatch_mode_Normal()
    {
        ingame = true;
        Hard = 0;
        Score = 0;
        Debug.Log("난이도 " + Hard);
        curMode = 3;
        SceneManager.LoadScene("DeathMatchMode");
    }
    public void DeathMatch_mode_Hard()
    {
        ingame = true;
        Hard = 1;
        Score = 0;
        Debug.Log("난이도 " + Hard);
        curMode = 3;
        SceneManager.LoadScene("DeathMatchMode");
    }
    public void RestartDeathMatchMode()
    {
        SceneManager.LoadScene("DeathMatchMode");
    }
    public void Tutorial_mode()
    {
        curMode = 2;
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
        if (HighScore < score)
            Score = score;
        else
            return;
    }
    public int GetScore()
    {
        HighScore = Score;
        return HighScore;
    }

    public int currDifficulty()
    {
         return Hard;
    }
    public void timescaleControl()
    {
        if (Time.timeScale == 1.0f)
            Time.timeScale = 0.0f;
        else if (Time.timeScale == 0.0f)
            Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
    public void Bool_quit()
    {
        quit = !quit;
    }

    public void QuitWindTextControl()
    {
        Text_Quit.SetActive(false);
        Text_Mission.SetActive(false);
        Text_Tut.SetActive(false);
        switch (curMode)
        {
            case 0:
                break;
            case 1:
                Text_Quit.SetActive(true);
                break;
            case 2:
                Text_Tut.SetActive(true);
                break;
            case 3:
                Text_Mission.SetActive(true);
                break;

        }
    }
}
