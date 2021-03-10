using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HittedUIScript : MonoBehaviour
{
    Image AimHitted;

    bool Hit;

    void Start()
    {
        AimHitted = this.gameObject.GetComponent<Image>();
    }

    void Update()
    {
        if (Hit)
        {
            AimHitted.CrossFadeAlpha(0, 1.0f, true);           
        }
    }

    public void HittedZombie()
    {
        Hit = true;
        AimHitted.color = new Color(0,0,0, 0.5019608f);
    }
}
