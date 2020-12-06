using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameManager : MonoBehaviour
{
    //Text ScoreText;
    public GameObject DiedBG;
    public GameObject YouDiedTxt;
    Image DBGimg;
    Image YDTxt;

    int Score;
    int killedNZ, killedSZ;
    float timecheck;
    bool Re;
    bool isdead;
    int currentWave;

    static IngameManager _uniqueinstance;

    public static IngameManager _instance
    {
        get { return _uniqueinstance; }
    }

    void Awake()
    {
        _uniqueinstance = this;
        isdead = false;
        DBGimg = DiedBG.GetComponent<Image>();
        YDTxt = YouDiedTxt.GetComponent<Image>();
    }

    void Start()
    {
        GameObject go = GameObject.Find("ScoreText");
        //ScoreText = go.GetComponent<Text>();
        currentWave = 1;
        Score = 0;
        timecheck = 0;
        //ScoreText.text = Score.ToString();
        Re = false;
        DiedBG.SetActive(false);
        YouDiedTxt.SetActive(false);
        killedNZ = 0;
        killedSZ = 0;
    }

    void Update()
    {
        if (Re)
        {           
            timecheck += Time.deltaTime;
            if(timecheck >= 2.0f && Input.anyKeyDown)
            {
                Result();
            }
        }

        if (isdead)
        {
            DBGimg.color = new Color(0, 0, 0, timecheck);
            YDTxt.color = new Color(255, 255, 255, timecheck);
            timecheck += Time.deltaTime;
            Re = true;
        }
    }
    
    public void addScore(int addscore)
    {
        Score += addscore;
        //ScoreText.text = Score.ToString();
        SceneManagerScript._instance.Scoresave(Score);
    }

    public void Result()
    {
        SceneManagerScript._instance.Result_Scene();
    }

    public void CheckLife()
    {
        isdead = true;
        DiedBG.SetActive(true);
        YouDiedTxt.SetActive(true);
    }

    public bool IsDead()
    {
        return isdead;
    }
    public void NextWave()
    {
        int numNormalZ = 20 + (4 * (currentWave - 1));
        int numSpecialZ = (currentWave - 1);

        if (killedNZ == numNormalZ && killedSZ == numSpecialZ)
        {
            currentWave++;
            killedNZ = 0;
            killedSZ = 0;
        }
    }
    public int CurrentWave()
    {
        return currentWave;
    }

    public void countKilledNZ()
    {
        killedNZ++;
        Debug.Log("노멀좀비 킬 " + killedNZ);
    }
    public void countKilledSZ()
    {
        killedSZ++;
        Debug.Log("특수좀비 킬 " + killedSZ);
    }
}
