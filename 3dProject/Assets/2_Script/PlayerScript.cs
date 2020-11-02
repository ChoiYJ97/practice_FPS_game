using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable CS0649

public class PlayerScript : MonoBehaviour
{
    [SerializeField] Text HpTxt;
    int hp = 100;
    float colorNum;
    bool hitted;

    void Start()
    {
        colorNum = 0;
        hitted = false;
        HpTxt.text = hp.ToString();
    }

    void Update()
    {
        if (hitted)
        {
            HpTxt.color = Color.red;
            hitted = false;
            colorNum = 0;
        }

        if (colorNum <= 255.0f)
        {
            colorNum += Time.deltaTime;
            HpTxt.color = new Color(255.0f, colorNum, colorNum);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "ZombieArm")
        {
            hp--;
            HpTxt.text = hp.ToString();
            hitted = true;
        }
    }
}
