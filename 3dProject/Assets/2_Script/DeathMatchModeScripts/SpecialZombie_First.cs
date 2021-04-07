using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
#pragma warning disable CS0649


public class SpecialZombie_First : MonoBehaviour
{
    public enum aniState
    {
        IDLE = 0,
        WALK,
        ATTACK,
        DEAD,

        TOTAL
    }

    Animator aniZombie;

    Transform _trans;
    Transform playerTransform;
    Transform FirstDesti;
    GameObject go;
    NavMeshAgent nvAgent;
    BoxCollider Box;

    [SerializeField] int Hp;
    [SerializeField] CapsuleCollider ArmR;

    float distance;
    float timecheck = 0;
    bool interval, start;
    bool isdead;
    int currentDiff;
    float WalkSpeed;
    int score;
    int MaxHp;

    void Awake()
    {
        _trans = gameObject.GetComponent<Transform>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        go = GameObject.Find("FirstDestination");
        FirstDesti = go.GetComponent<Transform>();
        nvAgent = gameObject.GetComponent<NavMeshAgent>();
        aniZombie = gameObject.GetComponent<Animator>();
        Box = gameObject.GetComponent<BoxCollider>();
    }
    void Start()
    {
        ArmR.isTrigger = false;
        start = false;
        interval = true;
        isdead = false;
    }

    void Update()
    {
        HardOrNot();

        if (!start)
        {
            start = true;
            Hp = MaxHp;
        }

        if (Hp <= 0)
        {
            if (!isdead)
            {
                IngameManager._instance.countKilledSZ();
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

            nvAgent.destination = playerTransform.position;
            if (distance <= 2.5f)
            {
                Attack();
            }
            else
                Walking();
        }
        else
        {
            Walking();
            nvAgent.destination = FirstDesti.position;
            timecheck += Time.deltaTime;
            if (timecheck >= 3.0f)
            {
                timecheck = 0;
                interval = false;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (Hp <= 0)
            return;

        if (other.transform.tag == "Bullet" && !interval)
        {
            IngameManager._instance.addScore(1);
            HpChange(1);
        }
    }

    public void Dead()
    {
        ZomSpeed(0);
        aniZombie.SetInteger("Anistate", (int)aniState.DEAD);
        ArmR.radius = 0;
        ArmR.height = 0;
    }


    public void Walking()
    {
        ZomSpeed(WalkSpeed);
        aniZombie.SetInteger("Anistate", (int)aniState.WALK);
        ArmColliderIsNotTriggr();
    }

    public void Attack()
    {
        ZomSpeed(0);
        aniZombie.SetInteger("Anistate", (int)aniState.ATTACK);
        ArmColliderIsTrigger();
    }

    public void Idle()
    {
        ZomSpeed(0);
        aniZombie.SetInteger("Anistate", (int)aniState.IDLE);
        ArmColliderIsNotTriggr();
    }

    public void ZomSpeed(float speed)
    {
        nvAgent.speed = speed;
    }

    public int Currenthp()
    {
        return Hp;
    }
    public void HpChange(int hp)
    {
        Hp -= hp;
    }
    public bool isinterval()
    {
        return interval;
    }

    void ArmColliderIsTrigger()
    {
        ArmR.isTrigger = true;
    }
    void ArmColliderIsNotTriggr()
    {
        ArmR.isTrigger = false;
    }

    public void HardOrNot()
    {
        currentDiff = SceneManagerScript._instance.currDifficulty();
        if (currentDiff == 1)
        { MaxHp = 150; WalkSpeed = 2.5f; score = 200; }
        else
        { MaxHp = 70; WalkSpeed = 2.0f; score = 500; }
    }
}
