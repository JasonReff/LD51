using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeEnvironment : MonoBehaviour
{
    [SerializeField]
    GameObject openAxe;

    bool axeOpen = false;
    bool reset = true;

    public void EnterTrigger()
    {
        if(axeOpen)
        {
            CloseAxe();
            reset = false;
        }
        print("enter trigger, reset: " + reset);
    }

    public void ExitTrigger()
    {
        if (axeOpen)
            CloseAxe();
        else if(reset)
            OpenAxe();

        reset = true;

        print("exit trigger, reset: " + reset);
    }

    void OpenAxe()
    {
        print("open axe");
        openAxe.SetActive(true);
        NavMeshManager.Instance.RebakeHumanoidMesh();
        axeOpen = true;
    }
    void CloseAxe()
    {
        print("close axe");
        openAxe.SetActive(false);
        NavMeshManager.Instance.RebakeHumanoidMesh();
        axeOpen = false;
    }
}
