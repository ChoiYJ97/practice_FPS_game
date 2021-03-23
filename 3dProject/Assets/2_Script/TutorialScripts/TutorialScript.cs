using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    public AudioSource RadioSound;

    public Text GText;
    public GameObject TextWind;

    [Header("UI Icons")]
    public GameObject interfaceObj;
    public GameObject Bulleticon;
    public GameObject Hpicon;
    public GameObject Healicon;
    public GameObject Minimapicon;
    public GameObject MinimapMarkers;

    [Header("Keyboard Images")]
    public GameObject MoveGuide;
    public Image wKey;
    public Image aKey;
    public Image sKey;
    public Image dKey;

    [Header("Objects transform")]
    public Transform playerTrans;
    public Transform Ammobox;
    public Transform Medickit;
    public Transform Radio;

    [Header("ZombieSpawnPosition")]
    public Transform[] ZombieSpawnPos;

    [Header("ZombiePreFabs")]
    public GameObject Zombie;

    [Header("PlayerScript")]
    public GameObject Player;
    public PlayerScript_Tutorial Player_Tut;
    public AutomaticGunScriptLPFP_Tutorial Player_AutoScript_tut;

    string[] textsofguide = {
                        "훈련장에 온 것을 환영한다. 병사여!    <color=#0000ff>(Enter)</color>",       //0
                        "이제부터 임무에 필요한 기본적인 지식을 알려주겠다.",                         //1
                        "먼저 화면 인터페이스부터 알려 주겠다.",                                      //2
                        "병사가 가진 <color=#0000ff>총알</color>과 <color=#0000ff>수류탄</color>의 갯수를 알려준다.",                               //3
                        "현재 병사의 <color=#00bc00>건강 상태</color>를 알려준다.",                   //4
                        "치료 가능 여부를 알려준다.",                                                 //5
                        "<color=#00bc00>미니맵</color>이다.\n좀비나 물품의 위치가 나타난다.",         //6
                        "<color=#00ff00>초록</color> -> 본인\n<color=#0000ff>파랑</color> -> 물품\n<color=#ff0000>빨강</color> -> 좀비",            //7

                        "이제 움직임에 대해 알려주겠다, 시선은 <color=#0000ff>마우스</color>로 돌린다.",                     //8
                        "감도는 '<color=#0000ff>[</color>' , '<color=#0000ff>]</color>' 키로 조절 가능하다.",                //9
                        "이제 <color=#0000ff>w a s d</color> 로 움직여보자",                                                 //10
                        //wasd누르는 위치

                        "택티컬 나이프는 <color=#0000ff>F</color> 또는 <color=#0000ff>Q</color> 를 눌러서 적을 공격할 수 있다.",                    //11
                        "잘했다.",                                                                    //12
                        "나머지 동작키들에 대해서는 후에 설명하겠다.",                                //13

                        "다음 단계는 <color=#0000ff>총알박스</color>를 찾아가라.",                    //14
                        "가까이 다가가면 <color=#0000ff>E</color> 키를 누르라는 표시가 뜬다.",        //15
                        "누르고 총알을 보급 받도록.",                                                 //16

                        "이제 <color=#0000ff>R</color> 키를 눌러서 <color=#0000ff>장전</color>이 가능해 졌다.",              //17
                        "마우스 좌클릭으로 사격하고 우클릭으로 정조준할 수 있다.",                    //18

                        "총알도 받았으니 라디오를 작동 시키서 좀비들을 유도해라.",                    //19
                        "라디오는 스토리 임무과 섬멸 임무에서 효과가 서로 달라지니 기억해두도록.",    //20
                        "<color=#bcbc00>스토리모드</color> - 좀비 유도 장치\n<color=#ff0000>섬멸모드</color> - 좀비 스폰 속도 2배 빠르게",          //21
                        "수고했다.",                                                                  //22
                        "이제 싸우면서 다친 몸을 치료하도록.",                                        //23
                        "구급상자가 있는 위치로 가서 <color=#0000ff>E</color> 누르면 바로 회복된다.",   //24
                        "마지막으로 수류탄에 대해서 설명하겠다.",                                     //25
                        "<color=#0000ff>수류탄</color>은 <color=#0000ff>3</color> 을 누르면 사용할 수 있다.",                //26
                        "<color=#0000ff>수류탄</color>은 일정 반경 내에 있는 모든 일반 좀비들을 죽일 수 있다.",              //27
                        "하지만 특수 좀비는 한번에 안 죽으니 주의하도록",                             //28
                        "이상 훈련을 마치겠다.",                                                      //29
                        "(Esc키를 누르시면 튜토리얼을 종료할 수 있습니다.)" ,                          //30
                        ""//31
    };

    int count = 0, Killed = 0;

    float timecheck;
    float disP_A, disP_M, disP_R;

    bool keyCheck, keyW, keyA, keyS, keyD, NextKey,qfKeyCheck;
    bool killedAll, AmmoCheck, MedickitCheck, RadioCheck;

    void Start()
    {
        GText.text = textsofguide[count].ToString();
        interfaceObj.SetActive(false);
        MinimapMarkers.SetActive(false);
        MoveGuide.SetActive(false);

        timecheck = 0;

        keyCheck = false;
        keyW = false;
        keyA = false;
        keyS = false;
        keyD = false;
        NextKey = false;
        qfKeyCheck = false;
        killedAll = false;
        AmmoCheck = false;
        MedickitCheck = false;
        RadioCheck = false;

        disP_A = 0;
        disP_M = 0;
        disP_R = 0;
    }


    void Update()
    {
        EachDisOfObj();

        //Start line, 0line~9line + 10line
        if (Input.GetKeyDown(KeyCode.Return) && count < 10)
        {
            changeText(++count);
            if (count == 3)
            {
                interfaceObj.SetActive(true);
                UIIconControl(1);
            }
            if (count == 4) 
                UIIconControl(2);
            if (count == 5)
                UIIconControl(3);
            if (count == 6)
                UIIconControl(4);
            if (count == 7)
                UIIconControl(5);
            if(count == 8)
            {
                UIIconControl(6);
                interfaceObj.SetActive(false);
            }
        }

        //wasd key Guide If문
        if (count == 10)
        {
            if (Input.GetKeyDown(KeyCode.Return) && !keyCheck)
            {
                TextWind.SetActive(false);
                MoveGuide.SetActive(true);
                timecheck = 0;
                keyCheck = true;
            }

            if(keyCheck)
            {
                KeyboardIcons();
                if (Input.GetKeyDown(KeyCode.W))
                    keyW = true;
                if (Input.GetKeyDown(KeyCode.A))
                    keyA = true;
                if (Input.GetKeyDown(KeyCode.S))
                    keyS = true;
                if (Input.GetKeyDown(KeyCode.D))
                    keyD = true;
            }

            if (keyW && keyA && keyS && keyD)
            {
                timecheck += Time.deltaTime;
                if (timecheck >= 3.0f)
                {
                    count = 12;
                    TextWind.SetActive(true);
                    MoveGuide.SetActive(false);
                    keyW = false;
                    keyCheck = false;
                }
            }
        }

        //wasd key end and next
        if (count == 12)
        {
            if (!keyCheck)
            {
                changeText(count);
                keyCheck = true;
                NextKey = true;
            }
            if (Input.GetKeyDown(KeyCode.Return)&& keyCheck && NextKey)
            {
                count = 11;
                changeText(count);
                NextKey = false;
            }
        }

        //q,f key check
        if (count == 11 && !qfKeyCheck)
        {
            if(Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Q))
            {
                changeText(++count);
                qfKeyCheck = true;
            }
        }

        {/*
        if (count == 11)
        {
            timecheck += Time.deltaTime;
            if(Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Q))
            {
                timecheck = 0;
                changeText(12);
            }
            if(timecheck >= 2.0f)
                count = 12;
        }
        */
        }

        //q/f key check after stage 
        //Find Ammo Box 13line ~ 16line
        if (count >= 12 && count <= 15 && qfKeyCheck)
        {
            if (count != 15 && Input.GetKeyDown(KeyCode.Return))
            {
                changeText(++count);
            }

            if (count == 15 && disP_A < 3.0f)
            {
                changeText(++count);//count = 16
                AmmoCheck = true;
            }
        }

        //총알 박스 사용
        if(Input.GetKeyDown(KeyCode.E) && AmmoCheck)
        {
            changeText(++count);//17 R키
            AmmoCheck = false;
        }

        //장전
        if(count == 17 && !AmmoCheck && Input.GetKeyDown(KeyCode.R))
        {
            changeText(++count); //18
        }

        //라디오 설명 조건문
        if(Input.GetKeyDown(KeyCode.Return) && count >= 18 && count <= 20)
            changeText(++count);//19~21

        //좀비 스폰
        if(disP_R <= 3.0f && count == 21 && Input.GetKeyDown(KeyCode.E) && !RadioCheck)
        {
            Instantiate(Zombie, ZombieSpawnPos[0].position, ZombieSpawnPos[0].rotation);
            Instantiate(Zombie, ZombieSpawnPos[1].position, ZombieSpawnPos[1].rotation);
            Instantiate(Zombie, ZombieSpawnPos[2].position, ZombieSpawnPos[2].rotation);
            Instantiate(Zombie, ZombieSpawnPos[3].position, ZombieSpawnPos[3].rotation);
            count++;
            RadioCheck = true;
            TextWind.SetActive(false);
        }

        //좀비 킬수 확인
        if(Killed >= 4)
        {
            TextWind.SetActive(true);
            changeText(count); //22
            killedAll = true;
            Killed = 0;
        }

        //일반 설명
        if (Input.GetKeyDown(KeyCode.Return) && (count == 22||count == 23) && killedAll)
        {
            changeText(++count);//23
            Player_Tut.DamagedFromOthers(20);
        }

        //회복 설명 조건문
        if(disP_M <= 3.0f && count == 24 && Input.GetKeyDown(KeyCode.E))
        {
            changeText(++count);//24
        }

        //수류탄 설명 조건문
        if (Input.GetKeyDown(KeyCode.Return) && count >= 25 && count <=30)
        {
            changeText(++count);//25~30
            if (count == 26)
                Player_AutoScript_tut.GrenadeSupplement();
        }
        
        if(count == 30)
        {
            TextWind.SetActive(false);
        }
    }

    void changeText(int i)
    {
        GText.text = textsofguide[i].ToString();
       if(!RadioSound.isPlaying)
            RadioSound.Play();
    }

    void KeyboardIcons()
    {
        if (Input.GetKey(KeyCode.W))
            wKey.color = new Color(0.6f, 0.6f, 0.6f);
        else
            wKey.color = new Color(1f, 1f, 1f);

        if (Input.GetKey(KeyCode.A))
            aKey.color = new Color(0.6f, 0.6f, 0.6f);
        else
            aKey.color = new Color(1f, 1f, 1f);

        if (Input.GetKey(KeyCode.S))
            sKey.color = new Color(0.6f, 0.6f, 0.6f);
        else
            sKey.color = new Color(1f, 1f, 1f);

        if (Input.GetKey(KeyCode.D))
            dKey.color = new Color(0.6f, 0.6f, 0.6f);
        else
            dKey.color = new Color(1f, 1f, 1f);
    }

    void EachDisOfObj()
    {
        disP_A = Vector3.Distance(playerTrans.position, Ammobox.position);
        disP_M = Vector3.Distance(playerTrans.position, Medickit.position);
        disP_R = Vector3.Distance(playerTrans.position, Radio.position);
    }

    void UIIconControl(int i)
    {
        Bulleticon.SetActive(false);
        Hpicon.SetActive(false);
        Healicon.SetActive(false);
        Minimapicon.SetActive(false);
        MinimapMarkers.SetActive(false);
        switch (i)
        {
            case 1:
                Bulleticon.SetActive(true);
                break;
            case 2:
                Hpicon.SetActive(true);
                break;
            case 3:
                Healicon.SetActive(true);
                break;
            case 4:
                Minimapicon.SetActive(true);
                break;
            case 5:
                MinimapMarkers.SetActive(true);
                break;
            case 6:
                Bulleticon.SetActive(false);
                Hpicon.SetActive(false);
                Healicon.SetActive(false);
                Minimapicon.SetActive(false);
                MinimapMarkers.SetActive(false);
                break;
        }
    }


    public void ZombieKilled_Tut()
    {
        Killed++;
    }

}
