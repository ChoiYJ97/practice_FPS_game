using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyHard : MonoBehaviour
{
    void Start()
    {
    }
    public void Off()
    {
        gameObject.GetComponent<DifficultyHard>().enabled = false;
    }

    public void On()
    {
        gameObject.GetComponent<DifficultyHard>().enabled = true;
    }
}
