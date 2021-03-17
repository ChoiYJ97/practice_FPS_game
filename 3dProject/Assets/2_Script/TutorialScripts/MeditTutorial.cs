using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeditTutorial : MonoBehaviour
{
    public GameObject pressEkey;
    public PlayerScript_Tutorial player;
    public Transform Meditkit;
    public Transform playerTrans;

    float Dis;
    // Start is called before the first frame update
    void Start()
    {
        Dis = Vector3.Distance(playerTrans.position, Meditkit.position);
        pressEkey.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        pressEkeyCont();
    }

    void pressEkeyCont()
    {
        Dis = Vector3.Distance(playerTrans.position, Meditkit.position);

        if (Dis <= 3.0f)
        {
            pressEkey.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.hp = 100;
            }
        }
        else
        {
            pressEkey.SetActive(false);
        }
    }
}
