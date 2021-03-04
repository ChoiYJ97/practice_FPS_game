﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable CS0649

public class ReSpawnScript : MonoBehaviour
{
    [SerializeField] GameObject Zombie;
    [SerializeField] GameObject SpecialZombie;
    [SerializeField] GameObject[] RespawnLoca;
    [SerializeField] GameObject WaveTitle;
    [SerializeField] Text WaveText;
    [SerializeField] Text WaveTextSmall;
    [SerializeField] GameObject WaveSoundObj;

    GameObject GoZomPrefabs;
    GameObject GoSZomPrefabs;
    Transform[] RespawnPos;
    Vector3[] pos;
    WaveSoundScript ZomScream;

    public float RespawnTime;

    float timecheck, waveTerm;
    int countDown, countDownHard;
    int currentDiff, currentWave;
    int numNormalZ, numSpecialZ;
    int countNSpawned, countSSpawned;
    bool SpeedUp;
    int DoubleSpeed = 1;

    private void Awake()
    {
        RespawnPos = new Transform[5];
        GoZomPrefabs = gameObject.GetComponent<GameObject>();
        GoSZomPrefabs = gameObject.GetComponent<GameObject>();
        for (int i = 0; i < 5; i++)
        {
            RespawnPos[i] = RespawnLoca[i].GetComponent<Transform>();
        }
        pos = new Vector3[5];
        ZomScream = WaveSoundObj.GetComponent<WaveSoundScript>();
    }
    void Start()
    {
        for (int i = 0; i<5; i++)
        {
            pos[i] = RespawnPos[i].position;
        }
        timecheck = 0.0f;
        countDown = 0;
        countNSpawned = 0;
        countSSpawned = 0;
        waveTerm = 0;
        currentWave = IngameManager._instance.CurrentWave();
        Debug.Log("현재 웨이브 " + currentWave);
        Debug.Log(IngameManager._instance.CurrentWave());
        WaveTitle.SetActive(true);
        SpeedUp = false;
    }

    void Update()
    {
        HardOrNot();
        OnOffSpeedUp();

        if (currentWave == IngameManager._instance.CurrentWave())
        {
            NumofZombies();
            StartRespawnFuc();
        }

        if(currentWave == (IngameManager._instance.CurrentWave() - 1))
        {
            waveTerm += Time.deltaTime;
            if(waveTerm >= 5.0f / DoubleSpeed)
            {
                ZomScream.SoundsPlay();
                WaveTitle.SetActive(true);
                WaveText.text = (currentWave+1).ToString();
                WaveTextSmall.text = (currentWave + 1).ToString();
            }
            if (waveTerm >= 8.0f / DoubleSpeed)
            {
                countDown = 0;
                countSSpawned = 0;
                countNSpawned = 0;
                currentWave++;
                waveTerm = 0;
                Debug.Log("현재 웨이브 " + currentWave);
            }
        }
        
        AllSpawned();
    }

    void StartRespawnFuc()
    {
        timecheck += Time.deltaTime;
        if (timecheck >= (RespawnTime/DoubleSpeed))
        {
            WaveTitle.SetActive(false);
            ZombieRespawn();
            timecheck = 0;
            countDown++;
        }

        if (countDown >= countDownHard)
        {
            SpecialZombieRespawn();
            countDown = 0;
        }
    }

    public void ZombieRespawn()
    {
        if (countNSpawned == numNormalZ)
            return;

        for (int i = 0; i < 4; i++)
        {
            countNSpawned++;
            GoZomPrefabs = Instantiate(Zombie);
            Zombie.transform.position = pos[i];
            Debug.Log("노멀좀비 스폰 " + countNSpawned);
        }
    }

    public void SpecialZombieRespawn()
    {
        if (countSSpawned == numSpecialZ)
            return;

        countSSpawned++;
        GoSZomPrefabs = Instantiate(SpecialZombie);
        SpecialZombie.transform.position = pos[4];
        Debug.Log("특수좀비 스폰 " + countSSpawned);
    }

    public void HardOrNot()
    {
        currentDiff = SceneManagerScript._instance.currDifficulty();
        if (currentDiff == 1)
        {
            RespawnTime = 8.0f;
            countDownHard = 5;
        }
        else
        {
            RespawnTime = 10.0f;
            countDownHard = 4;
        }
    }

    public void NumofZombies()
    {
        int curWave = IngameManager._instance.CurrentWave();
        numNormalZ = 20 + (4 * (curWave - 1));
        numSpecialZ = (curWave - 1);
    }

    public void AllSpawned()
    {
        if (countNSpawned == numNormalZ && countSSpawned == numSpecialZ)
        {
            IngameManager._instance.NextWave();
        }
    }

    public void OnOffSpeedUp()
    {
        SpeedUp = IngameManager._instance.OnOffSpeedUp();
        if (SpeedUp)
            DoubleSpeed = 2;
        else
            DoubleSpeed = 1;
    }
}