using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox_Story : MonoBehaviour
{
    public Transform playerTrans;
    public GameObject PressEkey;
    public AutomaticGunScriptLPFP_Story Player;
    public GameObject Icons;

    Animator AniofBox;
    Transform transAmmoBox;

    float distance;
    int used;

    void Start()
    {
        transAmmoBox = gameObject.transform;
        distance = Vector3.Distance(transAmmoBox.position, playerTrans.position);
        AniofBox = gameObject.GetComponent<Animator>();
        used = 0;
    }

    void Update()
    {
        if (used == 1)
        {
            Icons.SetActive(false);
            PressEkey.SetActive(false);
            return;
        }

        distance = Vector3.Distance(transAmmoBox.position, playerTrans.position);
        if (distance <= 2.0f)
        {
            PressEkey.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player.AmmoSupplement();
                AniofBox.SetBool("Acted", true);
                used = 1;
            }
        }
        else
        {
            PressEkey.SetActive(false);
            AniofBox.SetBool("Acted", false);
        }
    }
}
