using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxScript : MonoBehaviour
{
    public Transform playerTrans;
    public GameObject PressEkey;
    public AutomaticGunScriptLPFP Player;
    Animator AniofBox;
    Transform transAmmoBox;

    float distance; //timecheck;

    void Start()
    {
        transAmmoBox = gameObject.transform;
        distance = Vector3.Distance(transAmmoBox.position, playerTrans.position);
        //timecheck = 30.0f;
        AniofBox = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        //timecheck += Time.deltaTime;
        distance = Vector3.Distance(transAmmoBox.position, playerTrans.position);
        if (distance <= 3.0f)
        {
            PressEkey.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))// && timecheck >= 30)
            {
                //timecheck = 0;
                Player.AmmoSupplement();
                AniofBox.SetBool("Acted", true);
            }
        }
        else
        {
            PressEkey.SetActive(false);
            AniofBox.SetBool("Acted", false);
        }

        
    }
}
