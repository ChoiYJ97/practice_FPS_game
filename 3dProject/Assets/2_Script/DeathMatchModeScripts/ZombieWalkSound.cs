using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieWalkSound : MonoBehaviour
{
    public AudioClip[] WalkSound;
    AudioSource AS_ZomWalk;
    Rigidbody vel;

    void Start()
    {
        vel = gameObject.GetComponent<Rigidbody>();
        AS_ZomWalk = gameObject.GetComponent<AudioSource>();
        AS_ZomWalk.volume = 0.5f;
    }

    void Update()
    {
        SoundControl();
    }

    void SoundControl()
    {
        if(vel.velocity.sqrMagnitude > 0.1f)
        {
            AS_ZomWalk.volume = 0.5f;
            AS_ZomWalk.clip = WalkSound[0];
        }

        else
        {
            AS_ZomWalk.volume = 0.0f;
        }
    }
}
