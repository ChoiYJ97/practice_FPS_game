using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable CS0649

public class PlayerScript_Tutorial : MonoBehaviour
{
    public Slider HpSlider;
    public Image HittedImpact;
    public Image HpFillImg;
    public int hp = 100;
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

            if (HittedImpact.color.a <= hittedNum && HittedImpact.color.a > 0)
            {
                decre -= 0.5f * Time.deltaTime;
                HittedImpact.color = new Color(HittedImpact.color.r,
                  HittedImpact.color.g, HittedImpact.color.b, decre);
            }

            HpSlider.value = (float)hp / 100;
            if (hp <= 50)
                HpFillImg.color = new Color(255, 255, 0, HpFillImg.color.a);
            else if (hp <= 25)
                HpFillImg.color = new Color(255, 0, 0, HpFillImg.color.a);
            else
                HpFillImg.color = new Color(255, 255, 255, HpFillImg.color.a);
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
        //IngameManager._instance.CheckLife();
    }

    public void HardorNot()
    {
        //currentDiff = SceneManagerScript._instance.currDifficulty();
        currentDiff = 1;
        if (currentDiff == 1)
            Damage = 5;
        else
            Damage = 1;
    }

    public void DamagedFromOthers(int damage)
    {
        hp -= damage;
        if (hp <= 0)
            hp = 0;
    }
}
