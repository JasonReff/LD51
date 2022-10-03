using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeEnvironment : MonoBehaviour
{
    [SerializeField]
    private GameObject[] openAxes;
    

    bool axeOpen = false;
    bool reset = true;

    public void EnterTrigger()
    {
        if(axeOpen)
        {
            CloseAxe();
            reset = false;
        }
    }

    public void ExitTrigger()
    {
        if (axeOpen)
            CloseAxe();
        else if(reset)
            OpenAxe();

        reset = true;

    }

    void OpenAxe()
    {
        foreach(var openAxe in openAxes)
        {
            openAxe.SetActive(true);
        }
        NavMeshManager.Instance.RebakeHumanoidMesh();
        axeOpen = true;
    }
    void CloseAxe()
    {
        foreach (var openAxe in openAxes)
        {
            openAxe.SetActive(false);
        }
        NavMeshManager.Instance.RebakeHumanoidMesh();
        axeOpen = false;
    }
}
