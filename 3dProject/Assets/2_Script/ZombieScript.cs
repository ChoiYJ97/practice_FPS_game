using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
#pragma warning disable CS0649

public class ZombieScript : MonoBehaviour
{
    Animator aniZombie;
    Transform _trans;
    Transform playerTransform;
    Transform FirstDesti;
    GameObject go;
    NavMeshAgent nvAgent;
    BoxCollider ZombieCollider;   

    [SerializeField] int Hp;
    [SerializeField] CapsuleCollider ArmL;
    [SerializeField] CapsuleCollider ArmR;

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
    bool find;
    bool hitted;
    bool interval;
    bool isdead;

    void Awake()
    {
        _trans = gameObject.GetComponent<Transform>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        go = GameObject.Find("FirstDestination");
        FirstDesti = go.GetComponent<Transform>();
        nvAgent = gameObject.GetComponent<NavMeshAgent>();
        aniZombie = gameObject.GetComponent<Animator>();
        ZombieCollider = gameObject.GetComponent<BoxCollider>();
    }
    void Start()
    {
        ArmL.isTrigger = false;
        ArmR.isTrigger = false;
        distance = Vector3.Distance(playerTransform.position, _trans.position);
        Hp = 10;
        find = false;
        hitted = false;
        interval = true;
        isdead = false;
    }

    void Update()
    {
        if (Hp <= 0)
        {
            if (!isdead)
            {
                IngameManager._instance.addScore();
                isdead = true;
            }
            nvAgent.height = 0.01f;
            ZombieCollider.size = new Vector3(0, 0, 0);
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

            if (distance <= 20.0f)
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
            else if (distance > 20.0f && !hitted)
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
            if(timecheck >= 8.0f)
            {
                timecheck = 0;
                interval = false;
            }
        }
    }

    public void Dead()
    {
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
            ZomSpeed(1.5f);
            aniZombie.SetInteger("Anistate", (int)aniState.WALK_HANDUP);
        }
        else
        {
            ZomSpeed(1.5f);
            aniZombie.SetInteger("Anistate", (int)aniState.WALK_HANDDOWN);
        }
        ArmColliderIsNotTriggr();
    }

    public void Running()
    {
        ZomSpeed(3.5f);
        aniZombie.SetInteger("Anistate", (int)aniState.RUN);
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

    private void OnCollisionEnter(Collision other)
    {
        if (Hp <= 0)
            return;

        if (other.transform.tag == "Bullet" && !interval)
        {
            hitted = true;
            Hp--;
            if (!find)
                find = true;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Hp <= 0)
            return;

        if (other.transform.tag == "Knife" && !interval)
        {
            hitted = true;
            Hp--;
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
        Hp = hp;
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
}
