using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTextScript : MonoBehaviour
{
    public Transform CameraTrans;
    public Transform _trans;

    bool act;
    float Zpos;
    float max = 5.618f, min = 5.576f;

    void Start()
    {
        Zpos = max;
        act = false;        
    }

    void Update()
    {
        if (Vector3.Distance(CameraTrans.position, _trans.position) <= 4.0f && !act)
        {
            Zpos -= 0.0002f;
            if (Zpos < min)
            { act = true; }
            _trans.position = new Vector3(_trans.position.x, _trans.position.y, Zpos);            
        }
        
    }
}
