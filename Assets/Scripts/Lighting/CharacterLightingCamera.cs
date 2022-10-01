using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CharacterLightingCamera : MonoBehaviour
{
    [SerializeField] GameObject _character;
    private Camera _camera;
    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        transform.position = new Vector3(_character.transform.position.x, _character.transform.position.y, -10);
    }
}
