using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrikeManager : MonoBehaviour
{
    private Coroutine _lightningCoroutine;
    [SerializeField] private float _lightningDelay, _lightningDuration;
    public static event Action OnLightningStrikeStart, OnLightningStrikeEnd;

    private void Start()
    {
        _lightningCoroutine = StartCoroutine(LightningCoroutine());
    }

    private IEnumerator LightningCoroutine()
    {
        yield return new WaitForSeconds(_lightningDelay);
        OnLightningStrikeStart?.Invoke();
        yield return new WaitForSeconds(_lightningDuration);
        OnLightningStrikeEnd?.Invoke();
        _lightningCoroutine = StartCoroutine(LightningCoroutine());
    }
}
