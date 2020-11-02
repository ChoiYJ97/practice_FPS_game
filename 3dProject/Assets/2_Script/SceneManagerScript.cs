using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{

    static SceneManagerScript _uniqueinstance;
    public static SceneManagerScript _instance
    {
        get { return _uniqueinstance; }
    }

    private void Awake()
    {
        _uniqueinstance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit_Game();
        }
    }

    public void Exit_Game()
    {
#if UNITY_EDITOR
        //에디터에 play버튼을 끄는 역활
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //빌드에서는 가능하지만 에디터에서는 안된다.
        Application.Quit();
#endif

    }
}
