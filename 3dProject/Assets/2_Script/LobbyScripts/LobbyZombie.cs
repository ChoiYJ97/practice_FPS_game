using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyZombie : MonoBehaviour
{
    Animator ani;

    void Start()
    {
        ani = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
    }

    public void attackaniplay()
    {
        ani.SetBool("clicked", true);
    }
}
