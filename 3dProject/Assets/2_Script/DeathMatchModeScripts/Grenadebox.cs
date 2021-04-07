using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenadebox : MonoBehaviour
{
    public Transform GrenadeBoxTrans;
    public Transform playerTrans;
    public GameObject pressEkey;
    public AutomaticGunScriptLPFP playerGrenade;

    float dis;

    void Start()
    {
        dis = Vector3.Distance(playerTrans.position, GrenadeBoxTrans.position);
        pressEkey.SetActive(false);
    }

    void Update()
    {
        pressEkeyCtrl();
    }

    void pressEkeyCtrl()
    {
        dis = Vector3.Distance(playerTrans.position, GrenadeBoxTrans.position);

        if (dis <= 3.0f)
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
