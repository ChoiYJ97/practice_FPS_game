using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHeadScript_Tutorial : MonoBehaviour
{
    public ZombieScript_Tutorial Zom;

    void Start()
    {
        
    }
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Bullet")
        {
            Debug.Log("hit");

            Zom.HpChange(5);
        }
    }
}
