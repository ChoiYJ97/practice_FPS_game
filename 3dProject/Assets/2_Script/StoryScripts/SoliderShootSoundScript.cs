using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoliderShootSoundScript : MonoBehaviour
{
    public AudioClip clip;
    AudioSource Audio;
    AudioClip none;

    public float playtime;
    bool PlayStart;
    float timecheck = 0;
    int stop;
    void Start()
    {
        Audio = gameObject.GetComponent<AudioSource>();
        PlayStart = false;
        stop = 0;
        Audio.clip = clip;
    }

    void Update()
    {
        if (stop >= 3)
        {
            Audio.clip = none;
            ResetSound();
            return;
        }

        timecheck += Time.deltaTime;
        if (timecheck > 10.0f+playtime && !PlayStart)
        {
            PlayStart = true;
            Audio.Play();
            timecheck = 0;
        }

        if (playtime <= timecheck && PlayStart)
        {
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
