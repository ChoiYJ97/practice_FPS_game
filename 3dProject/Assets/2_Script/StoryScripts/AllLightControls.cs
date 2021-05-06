using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllLightControls : MonoBehaviour
{
    [Header("Story_StartMap")]
    public GameObject[] S_Light;

    [Header("Story_StartMap")]
    public GameObject[] F_Light;

    static AllLightControls Uniqueinstance;
    public static AllLightControls _instance
    {
        get { return Uniqueinstance; }
    }
    void Awake()
    {
        Uniqueinstance = this;
    }
    
    void Start()
    {
        StartMapLightsControl(1);
        FirstMapLightsControl(0);
    }

    void Update()
    {
        
    }

    public void StartMapLightsControl(int onoff)
    { // 0 -> 끄기, 1 -> 켜기
        if(onoff == 0)
        {
            for (int i = 0; i < S_Light.Length; i++)
            {
                S_Light[i].SetActive(false);
            }
        }

        else if (onoff == 1)
        {
            for (int i = 0; i < S_Light.Length; i++)
            {
                S_Light[i].SetActive(true);
            }
        }
        
    }

    public void FirstMapLightsControl(int onoff)
    {// 0 -> 끄기, 1 -> 켜기
        if (onoff == 0)
        {
            for (int i = 0; i < F_Light.Length; i++)
            {
                F_Light[i].SetActive(false);
            }
        }

        else if (onoff == 1)
        {
            for (int i = 0; i < F_Light.Length; i++)
            {
                F_Light[i].SetActive(true);
            }
        }
    }
}
