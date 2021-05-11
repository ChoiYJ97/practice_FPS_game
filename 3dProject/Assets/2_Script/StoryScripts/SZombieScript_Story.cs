using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SZombieScript_Story : MonoBehaviour
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
    NavMeshAgent nvAgent;
    BoxCollider Box;

    [SerializeField] int Hp;
    [SerializeField] CapsuleCollider ArmR;

    float distance;
    float timecheck = 0;
    bool start;
    bool isdead;
    float WalkSpeed;
    int MaxHp;

    void Awake()
    {
        _trans = gameObject.GetComponent<Transform>();
        playerTransform = GameObject.Find("Player_Story").GetComponent<Transform>();
        nvAgent = gameObject.GetComponent<NavMeshAgent>();
        aniZombie = gameObject.GetComponent<Animator>();
        Box = gameObject.GetComponent<BoxCollider>();
    }
    void Start()
    {
        ArmR.isTrigger = false;
        start = false;
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

        if (true)
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
    }

    private void OnCollisionEnter(Collision other)
    {
        if (Hp <= 0)
            return;

        if (other.transform.tag == "Bullet")
        {
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
        MaxHp = 100;
        WalkSpeed = 2.5f;
    }
}
