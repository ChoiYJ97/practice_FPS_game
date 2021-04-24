using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class StoryModeScript : MonoBehaviour
{
    [Header("Chapter Starting Position")]
    public Transform[] StartPos; // 0 - Sewer, 1 - Factory, 2 - Labs

    [Header("StartPointUI")]
    public GameObject Canvas;

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


    private void Awake()
    {
        BlackOutImg = BlackOut.GetComponent<Image>();
    }

    void Start()
    {
        BlackOutImg.color = new Color(0, 0, 0, 1.0f);
        getsetTime = 2.0f;
    }

    void Update()
    {
        BlackOutControl(1);
    }

    public void BlackOutControl(int i) // 0 = 켜짐 , 1 = 꺼짐
    {
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
