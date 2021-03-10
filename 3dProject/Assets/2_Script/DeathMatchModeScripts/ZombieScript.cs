using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
#pragma warning disable CS0649

public class ZombieScript : MonoBehaviour
{
    Animator aniZombie;
    Transform _trans;
    Transform playerTransform;
    Transform FirstDesti;
    GameObject go;
    NavMeshAgent nvAgent;
    BoxCollider Box;
    SceneManagerScript diffculty;

    [SerializeField] int Hp;
    [SerializeField] CapsuleCollider ArmL;
    [SerializeField] CapsuleCollider ArmR;

    public AudioClip[] Scream;
    AudioSource ZomScream;

    public enum aniState
    {
        IDLE    =0,
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
    bool interval;
    bool isdead;
    bool start;
    int currentDiff, score;

    void Awake()
    {
        _trans = gameObject.GetComponent<Transform>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        go = GameObject.Find("FirstDestination");
        FirstDesti = go.GetComponent<Transform>();
        nvAgent = gameObject.GetComponent<NavMeshAgent>();
        aniZombie = gameObject.GetComponent<Animator>();
        Box = gameObject.GetComponent<BoxCollider>();
        diffculty = GameObject.Find("Scene_Manager").GetComponent<SceneManagerScript>();
        ZomScream = gameObject.GetComponent<AudioSource>();
    }
    void Start()
    {
        ArmL.isTrigger = false;
        ArmR.isTrigger = false;
        distance = Vector3.Distance(playerTransform.position, _trans.position);
        start = false;
        find = false;
        hitted = false;
        interval = true;
        isdead = false;
        currentDiff = SceneManagerScript._instance.currDifficulty();
        ZomScream.volume = 0.5f;
    }

    void Update()
    {
        currentDiff = SceneManagerScript._instance.currDifficulty();

        if (!start)
        {
            if (currentDiff == 1)
                HardMode();
            else
                NormalMode();
            start = true;
        }

        if (Hp <= 0)
        {
            if (!isdead)
            {
                IngameManager._instance.countKilledNZ();
                IngameManager._instance.addScore(score);
                isdead = true;
            }
            Box.size = new Vector3(0, 0, 0);
            nvAgent.height = 0.01f;
            timecheck += Time.deltaTime;
            Dead();
            if (timecheck >= 2.5f)
            {
                Destroy(gameObject);
                timecheck = 0;
            }
            return;
        }

        if (!interval)
        {
            distance = Vector3.Distance(playerTransform.position, _trans.position);

            if (distance <= 30.0f)
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
            else if (distance > 30.0f && !hitted)
            {
                hitted = false;
                find = false;
                Idle();
            }
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
        else
        {
            Walking(false);
            nvAgent.destination = FirstDesti.position;
            timecheck += Time.deltaTime;
            if(timecheck >= 6.0f)
            {
                timecheck = 0;
                interval = false;
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

        if (other.transform.tag == "Bullet" && !interval)
        {
            IngameManager._instance.addScore(1);
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
    public  bool isinterval()
    {
        return interval;
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

    void HardMode()
    {
        Hp = 20;
        WalkSpeed = 2.0f;
        RunSpeed = 4.0f;
        score = 40;
    }

    void NormalMode()
    {
        Hp = 10;
        WalkSpeed = 1.5f;
        RunSpeed = 3.5f;
        score = 20;
    }
}
