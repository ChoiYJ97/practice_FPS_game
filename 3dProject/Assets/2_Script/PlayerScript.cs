using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable CS0649

public class PlayerScript : MonoBehaviour
{
    public Slider HpSlider;
    public Image HittedImpact;
    int hp = 100;
    float colorNum, hittedNum, GoalHitAlpha, decre;
    bool hitted;
    bool isDead;
    int currentDiff;
    int Damage;

    void Start()
    {
        colorNum = 0;
        hittedNum = 0.4901961f;
        GoalHitAlpha = 0.0f;
        decre = 0.0f;
        hitted = false;
        isDead = false;
        HittedImpact.color = new Color(HittedImpact.color.r,
                HittedImpact.color.g, HittedImpact.color.b, GoalHitAlpha);
    }

    void Update()
    {
        HardorNot();

        if (hp <= 0)
        {
            Isdead();
            return;
        }

        if (!isDead)
        {
            if (hitted)
            {
                hitted = false;
                colorNum = 0;
            }

            if (colorNum <= 255.0f)
            {
                colorNum += Time.deltaTime;
                if (hp >= 70)
                {
                    //HpTxt.color = new Color(0.0f, colorNum, 0, 0.5901961f);
                }
                else if (hp < 70 && hp >= 40)
                {
                    //HpTxt.color = new Color(255.0f, colorNum, 0, 0.5901961f);
                }
                else
                {
                    //HpTxt.color = new Color(255.0f, colorNum / 2, 0, 0.5901961f);
                }
            }
                    
            if (HittedImpact.color.a <= hittedNum && HittedImpact.color.a > 0)
            {
                decre -= 0.5f*Time.deltaTime;
                HittedImpact.color = new Color(HittedImpact.color.r,
                  HittedImpact.color.g, HittedImpact.color.b, decre);
            }

            HpSlider.value = (float)hp / 100;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "ZombieArm")
        {
            HittedImpactFuc();
            hp -= Damage;
            hitted = true;
        }

        if (other.transform.tag == "SZombieArm")
        {
            HittedImpactFuc();
            hp -= (10 * Damage);
            hitted = true;
        }
    }

    void HittedImpactFuc()
    {
        if (isDead)
            return;
        decre = hittedNum;
        HittedImpact.color = new Color(HittedImpact.color.r,
            HittedImpact.color.g, HittedImpact.color.b, hittedNum);
    }

    public void Isdead()
    {
        isDead = true;
        IngameManager._instance.CheckLife();
    }

    public void HardorNot()
    {
        currentDiff = SceneManagerScript._instance.currDifficulty();
        if (currentDiff == 1)
            Damage = 5;
        else
            Damage = 1;
    }
}
