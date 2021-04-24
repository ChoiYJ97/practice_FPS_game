using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarNavigation : MonoBehaviour
{
    public Transform Desti;
    Transform carTrans;
    NavMeshAgent AI;
    float dis;

    void Start()
    {
        carTrans = gameObject.GetComponent<Transform>();
        AI = gameObject.GetComponent<NavMeshAgent>();
        dis = Vector3.Distance(carTrans.position, Desti.position);
    }

    void Update()
    {
        carControl();
    }

    public void carControl()
    {
        dis = Vector3.Distance(carTrans.position, Desti.position);
        if (dis >= 2.0f)
        {
            AI.destination = Desti.position;
        }
    }
}
