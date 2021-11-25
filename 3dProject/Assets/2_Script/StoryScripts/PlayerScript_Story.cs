using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable CS0649

public class PlayerScript_Story : MonoBehaviour
{
    public AutomaticGunScriptLPFP_Story AmmoAndGrenade;
    public Slider HpSlider;
    public Image HittedImpact;
    public Image HpFillImg;
    public Transform[] revivePos;
    public GameObject[] kitIcons;
    public int hp = 100;
    float hittedNum, GoalHitAlpha, decre;
    bool hitted;
    bool isDead;
    bool canRevive;
    int Damage;
    int kitcount;

    public Transform[] MinimapObj;

    void Start()
    {
        hittedNum = 0.4901961f;
        GoalHitAlpha = 0.0f;
        decre = 0.0f;
        Damage = 2;
        hitted = false;
        isDead = false;
        canRevive = false;
        HittedImpact.color = new Color(HittedImpact.color.r,
                HittedImpact.color.g, HittedImpact.color.b, GoalHitAlpha);
        kitIcons[0].SetActive(false);
        kitIcons[1].SetActive(false);
        kitIcons[2].SetActive(false);
    }

    void Update()
    {
        MinimapObj[0].position = new Vector3(gameObject.transform.position.x, -149, gameObject.transform.position.z);
        MinimapObj[1].position = new Vector3(gameObject.transform.position.x, -137, gameObject.transform.position.z);
        if (canRevive && Input.anyKeyDown)
        {
            hp = 100;
            gameObject.transform.position = revivePos[0].position;
            AmmoAndGrenade.GrenadeSupplement();
            canRevive = false;
            isDead = false;
        }

        if (hp <= 0)
        {
            HpSlider.value = 0;
            StoryModeScript._instance.BlackOutControl(0);
            Isdead();
            StartCoroutine(Revive());
            return;
        }

        if (!isDead)
        {
            if (hitted)
            {
                hitted = false;
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

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            StartCoroutine(HealDelay());
            UseKit();
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
    }

    public void DamagedFromOthers(int damage)
    {
        hp -= damage;
        if (hp <= 0)
            hp = 0;
    }

    IEnumerator Revive()
    {
        StoryModeScript._instance.BlackOutControl(0);
        yield return new WaitForSeconds(1.0f);
        canRevive = true;
        StoryModeScript._instance.BlackOutControl(1);
    }
    IEnumerator HealDelay()
    {
        yield return new WaitForSeconds(1.0f);
    }

    public void getKit()
    {
        if (kitcount >= 3)
            return;
        kitcount++;
        kitIcons[kitcount - 1].SetActive(true);
    }

    public void UseKit()
    {
        if (kitcount <= 0)
            return;
        kitcount--;
        kitIcons[kitcount].SetActive(false);
        hp = 100;
    }

    public int currentKitcount()
    {
        return kitcount;
    }
}
