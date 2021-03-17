using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoTutorial : MonoBehaviour
{
    public GameObject pressEkey;
    public AutomaticGunScriptLPFP_Tutorial PlayerAmmo;
    public Transform Ammo;
    public Transform playerTrans;

    float Dis;
    // Start is called before the first frame update
    void Start()
    {
        Dis = Vector3.Distance(playerTrans.position, Ammo.position);
        pressEkey.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        pressEkeyCont();
    }

    void pressEkeyCont()
    {
        Dis = Vector3.Distance(playerTrans.position, Ammo.position);

        if (Dis <= 3.0f)
        {
            pressEkey.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerAmmo.AmmoSupplement();
            }
        }
        else
        {
            pressEkey.SetActive(false);
        }
    }
}
