using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
#pragma warning disable CS0649

public class LobbyCameraMoving : MonoBehaviour
{
    //0->첫 위치, 1->첫 이동 위치, 2-> 모드선택, 3-> 스토리모드, 4-> 데스매치모드
    GameObject[] DestiObj;
    GameObject[] Buttons;

    GameObject go;
    NavMeshAgent nvAgent;
    Transform CameraTrans;
    Transform LookatPosforSelectMode;
    public GameObject HighScoreText;

    float FirstDis, ModeDis, SelectDis, StoryDis, DeathMDis, activeDis,
        TutorialDis;
    location currentLocation;
    bool selected;
    bool SecLocBool;

    public enum location
    {
        TitleL                  =0, //타이틀 화면 위치
        TutorialOrGameModeL,        //튜터리얼과 게임모드 선택하는 위치
        SelectGameModeL,
        StoryModeL,
        DeathMatchModeL,
        TutorialL
    }


    void Awake()
    {
        go = GameObject.Find("CameraMovObject");

        DestiObj = new GameObject[7];
        DestiObj[0] = GameObject.Find("FirstLocation");
        DestiObj[1] = GameObject.Find("Destination_1_Start");
        DestiObj[2] = GameObject.Find("Destination_2_ChooseMode");
        DestiObj[3] = GameObject.Find("Destination_Story");
        DestiObj[4] = GameObject.Find("Destination_DeathMatch");
        DestiObj[5] = GameObject.Find("Destination_Tutorial");
        DestiObj[6] = GameObject.Find("Destination_1_StartLookFor");

        nvAgent = go.GetComponent<NavMeshAgent>();

        Buttons = new GameObject[11];

        CameraTrans = go.GetComponent<Transform>();

        Buttons[0] = GameObject.Find("TitleButton");
        Buttons[1] = GameObject.Find("GoGameMode");
        Buttons[2] = GameObject.Find("GoStoryMode");
        Buttons[3] = GameObject.Find("GoDeathMode");
        Buttons[4] = GameObject.Find("GoBackButton");
        Buttons[5] = GameObject.Find("GoDeathMachModeButton");
        Buttons[6] = GameObject.Find("GoTutorialButton");
        Buttons[7] = GameObject.Find("GoTutorialModeButton");
        Buttons[8] = GameObject.Find("GoBackButton_tu");
        Buttons[9] = GameObject.Find("GoDeathMachModeButtonHard");
        Buttons[10] = GameObject.Find("GoInStoryMode");

        go = GameObject.Find("DetourSign");

        LookatPosforSelectMode = go.GetComponent<Transform>();
    }

    void Start()
    {
        FirstDis = 0;
        ModeDis = 0;
        SelectDis = 0;
        StoryDis = 0;
        DeathMDis = 0;
        currentLocation = 0;
        TutorialDis = 0;
        activeDis = 2.0f;
        selected = false;
        SecLocBool = false;
        nvAgent.destination = DestiObj[0].transform.position;
    }

    void Update()
    {
        TitleButtonActive();
        GoGameModeActive();
        SelectButtonActive();
        currentLocation_mode();
        SelectTutorialButtonActive();
    }

    void LateUpdate()
    {
        if (currentLocation ==location.SelectGameModeL && !selected)
        {
            if(SelectDis <= 30.0f && nvAgent.speed <= 1000)
                nvAgent.speed += 1.5f + Time.deltaTime*2;
            SelectModeLookat();
        }
        if (selected)
        {
            nvAgent.speed += 1.0f + Time.deltaTime;
        }

        if(!SecLocBool && ModeDis <= 8.0f)
        {
            LookatSecLocation();
        }

        if(SelectDis <= 3.0f)
        {
            HighScoreText.SetActive(true);
        }
        else
            HighScoreText.SetActive(false);

        if (currentLocation == location.TutorialL)
            nvAgent.speed = 10;
    }

    public void MoveLocation(int index)
    {
        nvAgent.destination = DestiObj[index].transform.position;
        switch (index)
        {
            case 0:
                {
                    currentLocation = location.TitleL;
                    break;
                }
            case 1:
                {
                    currentLocation = location.TutorialOrGameModeL;
                    break;
                }
            case 2:
                {
                    currentLocation = location.SelectGameModeL;
                    break;
                }
            case 3:
                {
                    currentLocation = location.StoryModeL;
                    break;
                }
            case 4:
                {
                    currentLocation = location.DeathMatchModeL;
                    break;
                }
            case 5:
                {
                    currentLocation = location.TutorialL;
                    break;
                }
        } 
    }

    //첫 위치에서 이동버튼 활성화 함수
    public void TitleButtonActive()
    {
        FirstDis = Vector3.Distance(CameraTrans.position, DestiObj[0].transform.position);
        if (FirstDis <= activeDis)
        {
            Buttons[0].gameObject.SetActive(true);
        }
        else
        {
            Buttons[0].gameObject.SetActive(false);
        }
    }

    //두번째 위치에서 버튼 활성화 - 튜터리얼 버튼과 게임모드선택 위치 버튼 활성화
    public void GoGameModeActive()
    {
        ModeDis = Vector3.Distance(CameraTrans.position, DestiObj[1].transform.position);
        if (ModeDis <= activeDis)
        {
            Buttons[1].gameObject.SetActive(true);
            Buttons[6].gameObject.SetActive(true);
        }
        else
        {
            Buttons[1].gameObject.SetActive(false);
            Buttons[6].gameObject.SetActive(false);
        }
    }

    //게임모드 선택 위치에서 버튼 활성화
    public void SelectButtonActive()
    {
        SelectDis = Vector3.Distance(CameraTrans.position, DestiObj[2].transform.position);
        if (SelectDis <= activeDis)
        {
            Buttons[2].gameObject.SetActive(true);
            Buttons[3].gameObject.SetActive(true);
        }
        else
        {
            Buttons[2].gameObject.SetActive(false);
            Buttons[3].gameObject.SetActive(false);
        }
    }
    //현재 위치가 스토리모드 앞 인지 데스매치모드인지 선택 구간
    public void currentLocation_mode()
    {
        StoryDis = Vector3.Distance(CameraTrans.position, DestiObj[3].transform.position);
        DeathMDis = Vector3.Distance(CameraTrans.position, DestiObj[4].transform.position);
        if (StoryDis <= activeDis)
        {
            Buttons[4].gameObject.SetActive(true);
            Buttons[10].gameObject.SetActive(true);
        }
        else if (DeathMDis <= activeDis)
        {
            Buttons[4].gameObject.SetActive(true);
            Buttons[5].gameObject.SetActive(true);
            Buttons[9].gameObject.SetActive(true);
        }
        else
        {
            Buttons[4].gameObject.SetActive(false);
            Buttons[5].gameObject.SetActive(false);
            Buttons[9].gameObject.SetActive(false);
            Buttons[10].gameObject.SetActive(false);
        }
    }

    //튜터리얼 선택 위치 버튼 활성화
    public void SelectTutorialButtonActive()
    {
        TutorialDis = Vector3.Distance(CameraTrans.position, DestiObj[5].transform.position);
        if (TutorialDis <= activeDis)
        {
            Buttons[8].gameObject.SetActive(true);
            Buttons[7].gameObject.SetActive(true);
        }
        else
        {
            Buttons[8].gameObject.SetActive(false);
            Buttons[7].gameObject.SetActive(false);
        }

    }

    //두번째 위치에서 튜터리얼로 돌아왔을시 볼 위치
    public void LookatSecLocation()
    {
        Transform secLoc = DestiObj[6].GetComponent<Transform>();
        CameraTrans.LookAt(secLoc.position);
    }

   


    public void SelectModeLookat()
    {
        CameraTrans.LookAt(LookatPosforSelectMode.position);
    }

    public void ModeSelected()
    {
        selected = true;
    }
    public void BackButtonSelected()
    {
        selected = false;
    }

    public void Back_tu_Button_notSelected()
    {
        SecLocBool = false;
    }
    public void Back_tu_ButonSelected()
    {
        SecLocBool = true;
    }
}
