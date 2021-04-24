using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPointZombieSound : MonoBehaviour
{
    public AudioClip Clip;
    AudioClip none;
    AudioSource Audio;
    bool PlayStart;
    float timecheck = 0;
    int stop;
    void Start()
    {
        Audio = gameObject.GetComponent<AudioSource>();
        PlayStart = false;
    }

    void Update()
    {
        if (stop >= 3)
        {
            Audio.clip = none;
            return;
        }

        timecheck += Time.deltaTime;
        if (timecheck > 10.0f && !PlayStart)
        {
            PlayStart = true;
            timecheck = 0;
        }

        if (PlayStart)
        {
            Audio.clip = Clip;
            if(!Audio.isPlaying)
                Audio.Play();
            timecheck = 0;
            stop++;
        }
    }

    public void ResetSound()
    {
        PlayStart = false;
        Audio.Pause();
    }
}
