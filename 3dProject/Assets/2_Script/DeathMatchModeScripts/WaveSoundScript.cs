using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSoundScript : MonoBehaviour
{
    public AudioClip ZomScream;
    AudioSource _ZomScream;

    void Start()
    {
        _ZomScream = gameObject.GetComponent<AudioSource>();
        _ZomScream.clip = ZomScream;
        SoundsPlay();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    SoundsPlay();
        //}
    }

    public void SoundsPlay()
    {
        _ZomScream.Play();
    }
}
