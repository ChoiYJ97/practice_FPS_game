using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSoundScripts : MonoBehaviour
{
    public Transform playerTrans;
    public AudioClip[] Scream;
    AudioSource ZomScream;

    void Start()
    {
        ZomScream = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        gameObject.transform.position = playerTrans.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Zombie" && !ZomScream.isPlaying)
        {
            int i = Random.Range(0, 1);
            ZomScream.clip = Scream[i];
            ZomScream.Play();
            
        }
        if (Vector3.Distance(other.transform.position, playerTrans.position) > 5.0f)
                ZomScream.Pause();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Zombie" && ZomScream.isPlaying)
        {
            ZomScream.clip = Scream[2];
            ZomScream.Pause();
        }
    }

    public void ZombieDied()
    {
        ZomScream.Pause();
    }

}
