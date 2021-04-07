using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarNavigation : MonoBehaviour
{
    public Transform[] Desti;
    Transform carTrans;
    NavMeshAgent AI;
    float dis1, dis2;
    bool d1, d2;

    void Start()
    {
        carTrans = gameObject.GetComponent<Transform>();
        AI = gameObject.GetComponent<NavMeshAgent>();
        dis1 = Vector3.Distance(carTrans.position, Desti[0].position);
        dis2 = Vector3.Distance(carTrans.position, Desti[1].position);
        d1 = false;
        d2 = false;
    }

    void Update()
    {
        carControl();
    }

    public void carControl()
    {
        dis1 = Vector3.Distance(carTrans.position, Desti[0].position);
        dis2 = Vector3.Distance(carTrans.position, Desti[1].position);
        if (dis1 >= 2.0f && !d1)
        {
            AI.destination = Desti[0].position;
            if(dis1 <= 2.1f)
                d1 = true;
        }

        if (dis2 >= 1.0f && !d2 && d1)
        {
            AI.destination = Desti[1].position;
            if (dis2 <= 1.0f)
                d2 = true;
        }
    }
}
