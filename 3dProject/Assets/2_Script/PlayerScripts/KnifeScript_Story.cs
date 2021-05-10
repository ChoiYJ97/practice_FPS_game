﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeScript_Story : MonoBehaviour
{
    public Transform[] bloodPar;
    Transform _trans;

    float timecheck;
    bool interval;

    void Start()
    {
        _trans = gameObject.GetComponent<Transform>();
        interval = false;
        timecheck = 0;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Q))
        {
            interval = true;
        }
        if (interval)
        {
            timecheck += Time.deltaTime;
            if (timecheck >= 0.6f)
            {
                interval = false;
                timecheck = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Zombie" && interval)
        {
            Instantiate(bloodPar[Random.Range(0, bloodPar.Length)], _trans);
            other.gameObject.GetComponent<ZombieScript_Story>().HpChange(5);            
        }

        if (other.transform.tag == "SpecialZombie" && interval)
        {
            Instantiate(bloodPar[Random.Range(0, bloodPar.Length)], _trans);
            other.gameObject.GetComponent<SZombieScript_Story>().HpChange(5);
        }
    }
}
