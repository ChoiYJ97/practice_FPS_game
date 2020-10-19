using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable CS0649

public class ReSpawnScript : MonoBehaviour
{
    [SerializeField] GameObject Zombie;
    [SerializeField] GameObject[] RespawnLoca;

    GameObject GoZomPrefabs;
    Transform[] RespawnPos;
    Vector3[] pos;

    float timecheck;

    private void Awake()
    {
        RespawnPos = new Transform[4];
        GoZomPrefabs = gameObject.GetComponent<GameObject>();
        for (int i = 0; i < 4; i++)
        {
            RespawnPos[i] = RespawnLoca[i].GetComponent<Transform>();
        }
        pos = new Vector3[4];
    }
    void Start()
    {
        for(int i = 0; i<4; i++)
        {
            pos[i] = RespawnPos[i].position;
        }
        timecheck = 10.0f;
    }

    void Update()
    {
        timecheck += Time.deltaTime;
        if(timecheck >= 10.0f)
        {
            ZombieRespawn();
            timecheck = 0;
        }
    }

    public void ZombieRespawn()
    {
        for (int i = 0; i < 4; i++)
        {
            GoZomPrefabs = Instantiate(Zombie);
            Zombie.transform.position = pos[i];
        }
    }   
}
