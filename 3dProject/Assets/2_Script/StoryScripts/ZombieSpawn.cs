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
    float dis;
    bool Berserk;
    void Start()
    {
        dis = 0;
        Berserk = false;
    }

    void Update()
    {
        dis = Vector3.Distance(Player.position, electronic_box.position);
        if(dis <= 2.0f && Input.GetKeyDown(KeyCode.E))
        {
            Berserk = true;
        }

        if(Berserk)
        {
            for(int i = 0; i < 5; i++)
            {
                NzombieSpawn();
                StartCoroutine(SpawnDelay());
            }
            SzombieSpawn();
            Berserk = false;
        }
    }

    public void NzombieSpawn()
    {
        for(int i = 0; i < 5; i++)
            GameObject.Instantiate(ZomPrefabs, Spawner_n[i].position, Spawner_n[i].rotation);
    }

    public void SzombieSpawn()
    {
        for (int i = 0; i < 3; i++)
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
