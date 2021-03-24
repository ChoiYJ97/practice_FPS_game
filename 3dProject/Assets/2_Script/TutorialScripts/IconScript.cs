using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconScript : MonoBehaviour
{
    Transform iconTrans;

    float timecheck;

    void Start()
    {
        iconTrans = gameObject.GetComponent<Transform>();

        timecheck = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timecheck += Time.deltaTime;
        if(timecheck <= 1.0f)
        {
            iconTrans.Translate(Vector3.up * Time.deltaTime*0.3f);
        }
        else if(timecheck > 1.0f)
        {
            iconTrans.Translate(Vector3.down * Time.deltaTime * 0.3f);
            if (timecheck >= 2.0f)
                timecheck = 0;
        }

        iconTrans.Rotate(0, 90.0f * Time.deltaTime, 0.0f);
    }
}
