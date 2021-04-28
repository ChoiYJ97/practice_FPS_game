using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarNavigation : MonoBehaviour
{
    public Transform Desti;
    public GameObject carCam;
    public GameObject Player;
    Transform carTrans;
    NavMeshAgent AI;
    float dis;
    float timecheck;
    bool arrive;

    void Start()
    {
        Player.SetActive(false);
        carTrans = gameObject.GetComponent<Transform>();
        AI = gameObject.GetComponent<NavMeshAgent>();
        dis = Vector3.Distance(carTrans.position, Desti.position);
        arrive = false;
    }

    void Update()
    {
        carControl();
    }

    public void carControl()
    {
        dis = Vector3.Distance(carTrans.position, Desti.position);
        AI.destination = Desti.position;
        if (dis <= 2.0f && !arrive)
        {
            timecheck += Time.deltaTime;
            StoryModeScript._instance.BlackOutControl(0);
            if (timecheck >= 2.0f)
            {
                arrive = true;
                timecheck = 0;
            }
        }

        if(arrive)
        {
            timecheck += Time.deltaTime;
            if(timecheck >= 2.0f)
            {
                StoryModeScript._instance.BlackOutControl(1);
                carCam.SetActive(false);
                Player.SetActive(true);
            }
        }
    }
}
