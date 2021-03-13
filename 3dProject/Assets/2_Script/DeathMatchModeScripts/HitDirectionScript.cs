using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitDirectionScript : MonoBehaviour
{
    public Transform playerTrans;
    public Image HitDirection;
    float red, green, blue, alpha, decre;

    void Start()
    {
        red = HitDirection.color.r;
        green = HitDirection.color.g;
        blue = HitDirection.color.b;
        alpha = HitDirection.color.a;
        decre = alpha;
    }

    void Update()
    {
        gameObject.transform.position = playerTrans.position;
        gameObject.transform.rotation = playerTrans.rotation;
        if (HitDirection.color.a >= 0.0f)
        {
            decre -= 0.5f * Time.deltaTime;
            HitDirection.color = new Color(red, green, blue, decre);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "ZombieArm")
        {
            decre = alpha;
            HitDirection.color = new Color(red, green, blue, alpha);
        }

        if (other.transform.tag == "SZombieArm")
        {
            decre = alpha;
            HitDirection.color = new Color(red, green, blue, alpha);
        }
    }
}
