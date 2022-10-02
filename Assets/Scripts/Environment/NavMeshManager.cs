using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshManager : MonoBehaviour
{
    [SerializeField]
    NavMeshSurface2d humanoidMesh;

    [SerializeField]
    NavMeshSurface2d ghostMesh;

    [SerializeField]
    NavMeshSurface2d knightMesh;

    public static NavMeshManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance)
            Destroy(this);
        else
            Instance = this;
    }

    public void RebakeHumanoidMesh()
    {
        humanoidMesh.BuildNavMesh();
    }
}
