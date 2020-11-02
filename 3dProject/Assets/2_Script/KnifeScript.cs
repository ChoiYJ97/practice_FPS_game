using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeScript : MonoBehaviour
{
    MeshCollider coll;

    // Start is called before the first frame update
    void Start()
    {
        coll = gameObject.GetComponent<MeshCollider>();
        coll.transform.rotation = new Quaternion(0, 90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
