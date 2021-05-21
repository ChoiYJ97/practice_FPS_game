using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyZombie : MonoBehaviour
{
    Animator ani;
    public AudioClip Clip;
    AudioSource AS;

    void Start()
    {
        AS = gameObject.GetComponent<AudioSource>();
        ani = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
    }

    public void attackaniplay()
    {
        AS.clip = Clip;
        AS.Play();
        ani.SetBool("clicked", true);
        
    }
}
