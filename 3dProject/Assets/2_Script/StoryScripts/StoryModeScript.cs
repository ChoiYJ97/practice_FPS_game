using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class StoryModeScript : MonoBehaviour
{
    static StoryModeScript _uniqueinstance;
    public static StoryModeScript _instance
    {
        get { return _uniqueinstance; }
    }

    [Header("Chapter Starting Position")]
    public Transform StartPos; // 스타트 맵 하수구 뚜껑 위치
    public Transform SewerPos; // 하수구 안 시작 포지션

    [Header("StartPointUI")]
    public GameObject StartCanvas;

    [Header("Black Out")]
    public GameObject BlackOut;
    Image BlackOutImg;
    float lengthOfTime;
    float getsetTime
    {
        set
        {
            lengthOfTime = value;
        }
        get
        {
            return lengthOfTime;
        }
    }

    [Header("Revive Place")]
    public Transform[] RevivePos;

    [Header("Player Transform")]
    public Transform playerTrans;

    [Header("Quest")]
    public Text Start_QuestTxt;
    public Transform[] NPCs;
    public GameObject[] Icons;
    public GameObject StartMapEkey;
    public GameObject SewerMapEkey;
    string[] questtxt = 
        {
                        "어서오게, 중사여!\n이번 임무에 자원해줘서 고맙다.                        <Enter>",
                        "오면서 보고를 받았겠지만 다시 한번 더 브리핑하지.",
                        "지금 중사가 있는 이 공장에서 <color=red>생화학 무기 테러</color>가 발생했다.",
                        "이 무기는 생물을 감염시켜 생물병기로 만든다.",
                        "또한 매우 강력한 전염성도 있어서 매우 위험하다.",
                        "현재 공장 전체를 폐쇄조치를 취해 놓은 상태다.",
                        "하지만 생물병기들이 내부로부터 계속 나오고 있는 상황이다.",
                        "사령부는 공장의 내부 깊숙한 곳에 <color=red>발생원</color>이 있다고 본다.",
                        "그래서 중사가 이번에 맡은 임무는 이 <color=red>발생원</color>을 파괴하는 것이다.",
                        "준비되었으면 건물 뒤쪽에 <color=yellow>하수구</color>를 통해서 침투 하도록.",
                        "그럼 중사의 건투를 빌겠네."
    };



    bool start, inSewer;
    float distance, SewerEntDis;
    int startQuestInteger;

    private void Awake()
    {
        _uniqueinstance = this;
        BlackOutImg = BlackOut.GetComponent<Image>();
    }

    void Start()
    {
        BlackOutImg.color = new Color(0, 0, 0, 1.0f);
        getsetTime = 2.0f;
        distance = 0;
        SewerEntDis = 0;
        startQuestInteger = 0;
        start = false;
        inSewer = false;
        StartCanvas.SetActive(false);
        StartMapEkey.SetActive(false);
        SewerMapEkey.SetActive(false);
    }

    void Update()
    {
        if(!start)
            BlackOutControl(1);

        if(!inSewer)
            UpdateStartQuest();

        EnterSewer();
    }

    public void UpdateStartQuest()
    {
        distance = Vector3.Distance(playerTrans.position, NPCs[0].position);
        if (distance <= 2.0f && startQuestInteger != 11)
        {
            StartCanvas.SetActive(true);
            StartQuestTextControl(startQuestInteger);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (startQuestInteger <= 10)
                    startQuestInteger++;
            }
        }
        else
        {
            StartCanvas.SetActive(false);
        }
    }
    public void StartQuestTextControl(int i)
    {
        if (i == 10)
            Start_QuestTxt.alignment = TextAnchor.MiddleCenter;
        Start_QuestTxt.text = questtxt[i];
    }

    public void EnterSewer()
    {
        SewerEntDis = Vector3.Distance(playerTrans.position, StartPos.position);
        if (SewerEntDis <= 3.0f)
        {
            StartMapEkey.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                inSewer = true;
                StartCoroutine(TranslateDelay());
            }
        }
    }

    private IEnumerator TranslateDelay()
    {
        yield return new WaitForSeconds(2.0f);
        playerTrans.position = new Vector3(SewerPos.position.x, SewerPos.position.y, SewerPos.position.z);
        AllLightControls._instance.StartMapLightsControl(0);
    }


    public void BlackOutControl(int i) // 0 = 켜짐 , 1 = 꺼짐
    {
        start = true;
        if(i == 0) //검은 화면 켜짐
        {
            BlackOut.SetActive(true);
            BlackOutImg.CrossFadeAlpha(1.0f, lengthOfTime, false);
        }

        if(i == 1)//검은 화면 꺼짐
        {
            BlackOutImg.CrossFadeAlpha(0.0f, lengthOfTime, false);
            if(BlackOutImg.color.a <= 0.1f)
                BlackOut.SetActive(false);
        }
    }
}
