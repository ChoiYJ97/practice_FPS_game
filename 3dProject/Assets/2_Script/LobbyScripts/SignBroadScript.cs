using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignBroadScript : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject GMGo;
    public GameObject StartText;
    public GameObject SignBroadGo;
    public GameObject RightSignBroadGo;

    [Header("Transforms")]
    public Transform gamemodeText;
    public Transform cameraTrans;
    public Transform BroadTrans;
    public Transform StartTextTrans;

    [Header("Materials")]
    public Material WrongWay;
    public Material EmptyBroad;

    [Header("MeshRenderer")]
    public MeshRenderer BroadMeshRenderer;

    Material[] materials;

    //float GMTextYposmax = 3.76f;
    float dis, timecheck;
    bool active, Selected;
    void Start()
    {
        materials = BroadMeshRenderer.materials;
        active = true;
        timecheck = 0;
        Selected = false;
        dis = 0;

        SignBroadGo.SetActive(false);
        RightSignBroadGo.SetActive(false);
    }
    void Update()
    {
        dis = Vector3.Distance(cameraTrans.position, BroadTrans.position);
        if (active)
        {
            GMTextControl();
            startTextControl();
        }
        BroadMaterialControl();
    }

    void GMTextControl()
    {
        if(dis <= 4.0f)
        {
            timecheck += Time.deltaTime;
            gamemodeText.position = new Vector3(gamemodeText.position.x, gamemodeText.position.y+0.003f, gamemodeText.position.z);
        }

        if (timecheck >= 1.3f)
            active = false;
    }

    void BroadMaterialControl()
    {
        if (dis <= 4.0f && !Selected)
        {
            materials[0] = EmptyBroad;
            BroadMeshRenderer.materials = materials;
            Selected = true;
        }
    }

    void startTextControl()
    {
        if (dis <= 4.0f)
            StartTextTrans.position = new Vector3(StartTextTrans.position.x, StartTextTrans.position.y, StartTextTrans.position.z - 0.001f);
    }

    public void ActiveFalse()
    {
        GMGo.SetActive(false);
        StartText.SetActive(false);
        materials[0] = WrongWay;
        BroadMeshRenderer.materials = materials;
    }

    public void ActiveTrue_Broad()
    {
        SignBroadGo.SetActive(true);
        RightSignBroadGo.SetActive(true);
    }
}
