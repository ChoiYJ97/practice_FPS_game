using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
    public Transform Player;
    public Transform electronic_box;
    public GameObject ZomPrefabs;
    public GameObject SZomPrefabs;
    public Transform[] Spawner_n; // 5군데 스폰
    public Transform[] Spawner_s; // 3군데 스폰

    [Header("Zombie Spawn Count")]
    public int nzSpawnNum;
    public int szSpawnNum;
    float dis;
    bool Berserk;
    bool Once;
    void Start()
    {
        dis = 0;
        Berserk = false;
        Once = false;
    }

    void Update()
    {
        if (Once)
            return;
        dis = Vector3.Distance(Player.position, electronic_box.position);
        if(dis <= 2.0f && Input.GetKeyDown(KeyCode.E))
        {
            Berserk = true;
        }

        if(Berserk)
        {
            for(int i = 0; i < nzSpawnNum; i++)
            {
                NzombieSpawn();
                StartCoroutine(SpawnDelay());
            }
            SzombieSpawn();
            Berserk = false;
            Once = true;
        }
    }

    public void NzombieSpawn()
    {
        for(int i = 0; i < Spawner_n.Length; i++)
            GameObject.Instantiate(ZomPrefabs, Spawner_n[i].position, Spawner_n[i].rotation);
    }

    public void SzombieSpawn()
    {
        for (int i = 0; i < Spawner_s.Length; i++)
            GameObject.Instantiate(SZomPrefabs, Spawner_s[i].position, Spawner_s[i].rotation);
    }

    public void ObjectDestory()
    {
        Destroy(this.gameObject);
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(0.2f);
    }
}
