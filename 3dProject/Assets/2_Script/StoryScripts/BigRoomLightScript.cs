﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRoomLightScript : MonoBehaviour
{
    public GameObject[] Lights;
    public Light ElectronicBox;
    public Transform Player;
    Transform GTrans;
    float dis;


    void Start()
    {
        dis = 0;
        GTrans = this.gameObject.GetComponent<Transform>();
        for (int i = 0; i < Lights.Length; i++)
            Lights[i].SetActive(false);
    }

    void Update()
    {
        dis = Vector3.Distance(Player.position, GTrans.position);
        if(dis <= 1.5f)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ElectronicBox.color = Color.green;
                foreach(GameObject gm in Lights)
                {
                    gm.SetActive(true);
                }
            }
        }
    }
}
