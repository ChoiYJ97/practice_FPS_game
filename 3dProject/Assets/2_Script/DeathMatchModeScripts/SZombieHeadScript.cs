using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SZombieHeadScript : MonoBehaviour
{
    public SpecialZombie_First Zom;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            Debug.Log("hit");

            Zom.HpChange(10);
        }
    }
}
