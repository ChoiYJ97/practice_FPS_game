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

        if(playtime <= timecheck && PlayStart)
        {
            Audio.clip = clip;
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
