using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioTutorial : MonoBehaviour
{
    public GameObject pressEkey;
    //public GameObject SpeedUPIcon;

    public Transform Radio;
    public Transform playerTrans;

    bool acted;
    float Dis;
    // Start is called before the first frame update
    void Start()
    {
        Dis = Vector3.Distance(playerTrans.position, Radio.position);
        pressEkey.SetActive(false);
        //SpeedUPIcon.SetActive(false);
        acted = false;

    }

    // Update is called once per frame
    void Update()
    {
        pressEkeyCont();
    }

    void pressEkeyCont()
    {
        Dis = Vector3.Distance(playerTrans.position, Radio.position);

        if (Dis <= 3.0f)
        {
            pressEkey.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!acted)
                {
                    //SpeedUPIcon.SetActive(true);
                    acted = true;
                }
                else if (acted)
                {
                    //SpeedUPIcon.SetActive(false);
                    acted = false;
                }
            }
        }
        else
        {
            pressEkey.SetActive(false);
        }
    }
}
