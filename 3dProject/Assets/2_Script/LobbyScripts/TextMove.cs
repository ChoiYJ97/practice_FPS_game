using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMove : MonoBehaviour
{
    Transform Trans;
    float timecheck;
    
    void Start()
    {
        Trans = gameObject.GetComponent<Transform>();
        timecheck = 0;
    }

    void Update()
    {
        
    }

    public void Moving()
    {
        timecheck += Time.deltaTime;
        if(timecheck < 3.0f)
        {
            Trans.Translate(Vector3.down * Time.deltaTime * 1.0f);
        }
    }
}
