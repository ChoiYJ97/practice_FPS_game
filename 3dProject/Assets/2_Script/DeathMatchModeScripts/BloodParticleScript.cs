using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticleScript : MonoBehaviour
{
    float timecheck;
    void Start()
    {
        timecheck = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timecheck += Time.deltaTime;
        if (timecheck >= 2.0f)
            Destroy(gameObject);
    }
}
