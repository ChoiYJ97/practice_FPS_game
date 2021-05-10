using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieTrap : MonoBehaviour
{
    public GameObject ZomPrefabs;
    public Transform Spawner;
    //public GameObject Self;
    bool connect;
    bool Spawned;
    float timecheck;

    void Start()
    {
        timecheck = 0;
        connect = false;
        Spawned = false;
    }

    void Update()
    {
        if(connect)
        {
            timecheck += Time.deltaTime;
            if(timecheck >= 3.0f && !Spawned)
            {
                StartCoroutine(SpawnAndDestory());
                Spawned = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            connect = true;
    }
    public void zombieSpawn()
    {
        GameObject.Instantiate(ZomPrefabs, Spawner.position, Spawner.rotation);
    }

    public void ObjectDestory()
    {
        Destroy(this.gameObject);
    }

    IEnumerator SpawnAndDestory()
    {
        zombieSpawn();
        yield return new WaitForSeconds(0.1f);
        ObjectDestory();
    }
}
