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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Zombie" && ZomScream.isPlaying)
        {
            ZomScream.clip = Scream[2];
            ZomScream.Pause();
        }
    }

}
