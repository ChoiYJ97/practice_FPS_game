using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBox_Tutorial : MonoBehaviour
{
    public Transform GrenadeBoxTrans;
    public Transform playerTrans;
    public GameObject pressEkey;
    public AutomaticGunScriptLPFP_Tutorial playerGrenade;

    float dis;

    void Start()
    {
        dis = Vector3.Distance(playerTrans.position, GrenadeBoxTrans.position);
        pressEkey.SetActive(false);
    }

    void Update()
    {
        pressEkeyCtrl_tut();
    }

    void pressEkeyCtrl_tut()
    {
        dis = Vector3.Distance(playerTrans.position, GrenadeBoxTrans.position);

        if(dis <= 3.0f)
        {
            pressEkey.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                playerGrenade.GrenadeSupplement();
            }
        }
        else
        {
            pressEkey.SetActive(false);
        }
    }
}
