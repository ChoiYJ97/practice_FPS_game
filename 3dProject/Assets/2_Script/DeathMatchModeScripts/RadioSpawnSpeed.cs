using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioSpawnSpeed : MonoBehaviour
{
    public GameObject IconSpeedUp;
    public Transform transPlayer;
    public GameObject PressEkey;
    Transform transRadio;

    float distance, timecheck;

    void Start()
    {
        transRadio = gameObject.transform;
        distance = Vector3.Distance(transRadio.position, transPlayer.position);
    }

    void Update()
    {
        timecheck += Time.deltaTime;//시간 딜레이
        distance = Vector3.Distance(transRadio.position, transPlayer.position);
        if (distance <= 3.0f)
        {
            PressEkey.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E) && timecheck >= 3.0f)
            {
                timecheck = 0;
                OnOffSpeedRadio();
            }
        }
        else
            PressEkey.SetActive(false);

        if (IngameManager._instance.OnOffSpeedUp())
            IconSpeedUp.SetActive(true);
        else
            IconSpeedUp.SetActive(false);
    }

    public void OnOffSpeedRadio()
    {
        IngameManager._instance.OnOffDoubleSpeed();
    }
}
