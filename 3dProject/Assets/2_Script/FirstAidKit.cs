using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstAidKit : MonoBehaviour
{
    public Transform transPlayer;
    public GameObject PressEkey;
    public Slider MeditCoolTime;
    public PlayerScript Player;
    Transform transMeditKit;

    float distance, timecheck;

    void Start()
    {
        transMeditKit = gameObject.transform;
        distance = Vector3.Distance(transMeditKit.position, transPlayer.position);
        MeditCoolTime.value = 60;
        timecheck = 60;
    }

    void Update()
    {
        timecheck += Time.deltaTime;//시간 딜레이
        distance = Vector3.Distance(transMeditKit.position, transPlayer.position);
        if (distance <= 3.0f)
        {
            PressEkey.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E) && timecheck >= 120.0f)
            {
                timecheck = 0;
                MeditCoolTime.value = 120;
                Player.hp = 100;
            }
        }
        else
            PressEkey.SetActive(false);

        MeditCoolTime.value -= Time.deltaTime;
    }
}
