using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private List<CinemachineVirtualCamera> _cameras;
    [SerializeField] private CinemachineBrain _brain;

    private void Start()
    {
        if (_brain == null)
            _brain = FindObjectOfType<CinemachineBrain>();
        if (_cameras[1].Follow == null)
            _cameras[1].Follow = PlayerManager.Instance.transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CycleCamera();
        }
    }

    private void CycleCamera()
    {
        var camera = _brain.ActiveVirtualCamera as CinemachineVirtualCamera;
        var nextCamera = _cameras.GetNext(camera);
        nextCamera.Priority = 10;
        foreach (var c in _cameras)
            if (c != nextCamera)
                c.Priority = 0;
    }
}
