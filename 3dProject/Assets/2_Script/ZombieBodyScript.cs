using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBodyScript : MonoBehaviour
{
    BoxCollider colPos;
    SkinnedMeshRenderer Center;
    Vector3 vecPos, vecSize;

    void Start()
    {
        Center = gameObject.GetComponent<SkinnedMeshRenderer>();
        colPos = gameObject.GetComponent<BoxCollider>();
        vecPos = new Vector3(0, 0, 0); 
        vecSize = new Vector3(0, 0, 0);

    }

    void Update()
    {
        vecPos.x = Center.bounds.center.x;
        vecPos.y = Center.bounds.center.y;
        vecPos.z = Center.bounds.center.z;

        vecSize.x = Center.bounds.extents.x;
        vecSize.y = Center.bounds.extents.y;
        vecSize.z = Center.bounds.extents.z;

        colPos.center = vecPos;
        colPos.size = vecSize;
    }
}
