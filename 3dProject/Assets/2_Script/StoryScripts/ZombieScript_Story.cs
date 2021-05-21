using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript_Story : MonoBehaviour
{
    Transform playerTransform;
    Animator aniZombie;
    Transform _trans;
    NavMeshAgent nvAgent;
    BoxCollider Box;
    SceneManagerScript diffculty;

    [SerializeField] int Hp;
    [SerializeField] CapsuleCollider ArmL;
    [SerializeField] CapsuleCollider ArmR;

    public AudioClip[] Scream;
    AudioSource ZomScream;
    ZombieSoundScripts Zs;

    public enum aniState
    {
        IDLE = 0,
        WALK_HANDDOWN,
        WALK_HANDUP,
        RUN,
        ATTACK,
        DEAD,

        TOTAL
    }

    float distance;
    float timecheck = 0;
    float WalkSpeed, RunSpeed;
    bool find;
    bool hitted;
    bool isdead;
    bool start;

    void Awake()
    {
        _trans = gameObject.GetComponent<Transform>();
        playerTransform = GameObject.Find("Player_Story").GetComponent<Transform>();
        nvAgent = gameObject.GetComponent<NavMeshAgent>();
        aniZombie = gameObject.GetComponent<Animator>();
        Box = gameObject.GetComponent<BoxCollider>();
        ZomScream = gameObject.GetComponent<AudioSource>();
        Zs = GameObject.Find("SoundColliderGO").GetComponent<ZombieSoundScripts>();
    }
    void Start()
    {
        ArmL.isTrigger = false;
        ArmR.isTrigger = false;
        distance = Vector3.Distance(playerTransform.position, _trans.position);
        start = false;
        find = false;
        hitted = true;
        //hitted = false;
        isdead = false;
        ZomScream.volume = 0.5f;
    }

    void Update()
    {
        if (!start)
        {
            NormalMode();
            start = true;
        }

        if (Hp <= 0)
        {
            if (!isdead)
            {
                isdead = true;
            }
            Box.size = new Vector3(0, 0, 0);
            nvAgent.height = 0.01f;
            timecheck += Time.deltaTime;
            Zs.ZombieDied();
            Dead();
            if (timecheck >= 2.5f)
            {
                Destroy(gameObject);
                timecheck = 0;
            }
            return;
        }

        if (true)
        {
            distance = Vector3.Distance(playerTransform.position, _trans.position);

            if (distance <= 15.0f)
            {
                find = true;
                nvAgent.destination = playerTransform.position;
                if (distance <= 2.0f)
                    Attack();
                else if (!hitted)
                    Walking(true);
                else if (hitted)
                    Running();
            }
            //else if (distance > 30.0f && !hitted)
            //{
            //    hitted = false;
            //    find = false;
            //    Idle();
            //}
            else if (hitted)
            {
                find = true;
                nvAgent.destination = playerTransform.position;
                if (distance <= 2.0f)
                {
                    Attack();
                }
                else
                {
                    Running();
                }
            }
        }
    }

    public void Dead()
    {
        soundControl(5);
        ZomSpeed(0);
        aniZombie.SetInteger("Anistate", (int)aniState.DEAD);
        ArmL.radius = 0;
        ArmR.radius = 0;
        ArmL.height = 0;
        ArmR.height = 0;
    }

    public void Walking(bool found)
    {
        if (found)
        {
            ZomSpeed(WalkSpeed);
            aniZombie.SetInteger("Anistate", (int)aniState.WALK_HANDUP);
        }
        else
        {
            ZomSpeed(WalkSpeed);
            aniZombie.SetInteger("Anistate", (int)aniState.WALK_HANDDOWN);
        }
        ArmColliderIsNotTriggr();
        soundControl(0);
    }

    public void Running()
    {
        soundControl(0);
        ZomSpeed(RunSpeed);
        aniZombie.SetInteger("Anistate", (int)aniState.RUN);
        ArmColliderIsNotTriggr();
    }

    public void Attack()
    {
        soundControl(4);
        ZomSpeed(0.001f);
        aniZombie.SetInteger("Anistate", (int)aniState.ATTACK);
        ArmColliderIsTrigger();
    }

    public void Idle()
    {
        soundControl(0);
        ZomSpeed(0);
        aniZombie.SetInteger("Anistate", (int)aniState.IDLE);
        ArmColliderIsNotTriggr();
    }
    public void ZomSpeed(float speed)
    {
        nvAgent.speed = speed;
        nvAgent.destination = playerTransform.position;
    }

    int i = 0;
    void soundControl(int curAni)
    {
        if (i != curAni && !ZomScream.isPlaying)
        {
            ZomScream.clip = Scream[2];
            ZomScream.Pause();
            i = curAni;
        }
        else
            return;

        if (!ZomScream.isPlaying && curAni == 4)
        {
            ZomScream.clip = Scream[0];
            ZomScream.Play();
        }

        else if (curAni == 5)
        {
            ZomScream.clip = Scream[1];
            ZomScream.Play();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (Hp <= 0)
            return;

        if (other.transform.tag == "Bullet")
        {
            hitted = true;
            HpChange(1);
            if (!find)
                find = true;
        }
    }

    public int Currenthp()
    {
        return Hp;
    }
    public void HpChange(int hp)
    {
        Hp -= hp;
    }

    void ArmColliderIsTrigger()
    {
        ArmR.isTrigger = true;
        ArmL.isTrigger = true;
    }
    void ArmColliderIsNotTriggr()
    {
        ArmR.isTrigger = false;
        ArmL.isTrigger = false;
    }

    void NormalMode()
    {
        Hp = 5;
        WalkSpeed = 2.0f;
        RunSpeed = 3.5f;
    }

    public void Berserk()
    {
        hitted = true;
    }
}
