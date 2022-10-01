using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrikeManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private Coroutine _lightningCoroutine;
    [SerializeField] private float _lightningDelay, _lightningDuration;

    private void Start()
    {
        _lightningCoroutine = StartCoroutine(LightningCoroutine());
    }

    private IEnumerator LightningCoroutine()
    {
        yield return new WaitForSeconds(_lightningDelay);
        _camera.enabled = true;
        yield return new WaitForSeconds(_lightningDuration);
        _camera.enabled = false;
        _lightningCoroutine = StartCoroutine(LightningCoroutine());
    }
}
