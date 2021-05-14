using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medickit_Story : MonoBehaviour
{
    public Transform transPlayer;
    public GameObject PressEkey;
    public PlayerScript_Story Player;
    public GameObject Icons;
    Transform transMeditKit;

    float distance;
    int used;

    void Start()
    {
        transMeditKit = gameObject.transform;
        distance = Vector3.Distance(transMeditKit.position, transPlayer.position);
        used = 0;
    }

    void Update()
    {
        if (used == 1)
        {
            PressEkey.SetActive(false);
            Icons.SetActive(false);
            return;
        }
        distance = Vector3.Distance(transMeditKit.position, transPlayer.position);
        if (distance <= 2.5f)
        {
            PressEkey.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player.getKit();
                used = 1;
            }
        }
        else
            PressEkey.SetActive(false);
    }
}
