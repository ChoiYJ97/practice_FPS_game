using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable CS0649

public class TitleScript : MonoBehaviour
{
    [SerializeField] Material[] mats;

    MeshRenderer MeshRend;
    GameObject go;
    GameObject BlackoutObj;
    Transform CameraTrans;
    Transform goTrans;
    Image Blackout;

    bool Fadeout;
    float timeCheck;
    int materialNum;

    void Awake()
    {
        go = GameObject.Find("CameraMovObject");
        CameraTrans = go.GetComponent<Transform>();
        go = GameObject.Find("TitleMain");
        MeshRend = go.GetComponent<MeshRenderer>();
        go = GameObject.Find("Title");
        goTrans = go.GetComponent<Transform>();
        BlackoutObj = GameObject.Find("Blackout");
        Blackout = BlackoutObj.GetComponent<Image>();

    }
    void Start()
    {
        Fadeout = false;
        timeCheck = 0;
        materialNum = 13;
        FadeoutTitle(materialNum);
    }
    void Update()
    {
        Blackout.CrossFadeAlpha(0.0f, 1.2f, false);

        timeCheck += Time.deltaTime;
        if (Vector3.Distance(goTrans.position, CameraTrans.position) <= 7.0f && !Fadeout)
        {
            BlackoutObj.SetActive(false);
            //나타나는 조건문
            if (timeCheck >= 0.05f && materialNum >= 0)
            {
                timeCheck = 0;
                FadeoutTitle(materialNum);
                materialNum--;
            }
        }

        //사라지는 조건문
        if (timeCheck >= 0.05f && Fadeout && materialNum < 14)
        {
            if (materialNum == -1)
                materialNum++;
            timeCheck = 0;
            FadeoutTitle(materialNum);
            materialNum++;
        }
    }

    public void FadeoutTitle(int index)
    {
        MeshRend.material = mats[index];
    }

    public void TitlebuttonClicked()
    {
        Fadeout = true;
    }
}
