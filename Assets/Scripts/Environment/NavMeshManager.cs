using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshManager : MonoBehaviour
{
    [SerializeField] private List<NavMeshSurface2d> AllMeshes;
    [SerializeField]
    NavMeshSurface2d humanoidMesh;

    [SerializeField]
    NavMeshSurface2d ghostMesh;

    [SerializeField]
    NavMeshSurface2d knightMesh;

    public static NavMeshManager Instance { get; private set; }

    private void OnEnable()
    {
        DestroyWithKey.OnWallDestroyed += OnWallsChanged;
    }

    private void OnDisable()
    {
        DestroyWithKey.OnWallDestroyed -= OnWallsChanged;
    }

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

    private void OnWallsChanged()
    {
        humanoidMesh.BuildNavMesh();
        knightMesh.BuildNavMesh();
    }

    public void RebakeAllMeshes()
    {
        foreach (var mesh in AllMeshes)
            mesh.BuildNavMesh();
    }
}
