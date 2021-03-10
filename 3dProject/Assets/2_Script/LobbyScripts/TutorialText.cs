using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0649

public class TutorialText : MonoBehaviour
{
    [SerializeField] GameObject tutoraltext;
    bool active;
    bool StopAll;

    void Start()
    {
        active = true;
        OnOfftheTutorial();
    }

    void Update()
    {
        if (Input.GetButtonDown("F1"))
        {
            OnOfftheTutorial();
        }

        if (Input.GetKeyDown(KeyCode.F10) && !StopAll)
        {
            StopAll = !StopAll;
            Time.timeScale = 0.0f;
        }
        else if (Input.GetKeyDown(KeyCode.F10) && StopAll)
        {
            StopAll = !StopAll;
            Time.timeScale = 1.0f;

        }
    }

    void OnOfftheTutorial()
    {
        if (active)
        {
            active = false;
            tutoraltext.SetActive(false);
        }
        else if (!active)
        {
            active = true;
            tutoraltext.SetActive(true);
        }
    }
}
