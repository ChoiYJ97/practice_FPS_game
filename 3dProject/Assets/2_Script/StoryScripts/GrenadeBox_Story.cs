using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBox_Story : MonoBehaviour
{
    public Transform GrenadeBoxTrans;
    public Transform playerTrans;
    public GameObject pressEkey;
    public AutomaticGunScriptLPFP_Story playerGrenade;
    public GameObject Icons;

    float dis;
    int used;

    void Start()
    {
        dis = Vector3.Distance(playerTrans.position, GrenadeBoxTrans.position);
        pressEkey.SetActive(false);
        used = 0;
    }

    void Update()
    {
        pressEkeyCtrl();
    }

    void pressEkeyCtrl()
    {
        if (used == 1)
        {
            pressEkey.SetActive(false);
            Icons.SetActive(false);
            return;
        }

        dis = Vector3.Distance(playerTrans.position, GrenadeBoxTrans.position);

        if (dis <= 2.0f)
        {
            pressEkey.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                playerGrenade.GrenadeSupplement();
                used = 1;
            }
        }
        else
        {
            pressEkey.SetActive(false);
        }
    }

}
