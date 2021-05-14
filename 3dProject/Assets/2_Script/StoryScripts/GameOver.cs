using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject OverWindow;

    void Start()
    {
        OverWindow.SetActive(false);
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(GameoverDelay());
        }
    }

    IEnumerator GameoverDelay()
    {
        yield return new WaitForSeconds(1.0f);
        OverWindow.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
