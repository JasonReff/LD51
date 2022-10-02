using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrikeManager : SingletonMonobehaviour<LightningStrikeManager>
{
    private Coroutine _lightningCoroutine;
    [SerializeField] private float _lightningDelay, _lightningDuration, _initialDelay;
    public static event Action OnLightningStrikeStart, OnLightningStrikeEnd;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_initialDelay);
        _lightningCoroutine = StartCoroutine(LightningCoroutine());
    }

    private IEnumerator LightningCoroutine()
    {
        OnLightningStrikeStart?.Invoke();
        yield return new WaitForSeconds(_lightningDuration);
        OnLightningStrikeEnd?.Invoke();
        yield return new WaitForSeconds(_lightningDelay);
        _lightningCoroutine = StartCoroutine(LightningCoroutine());
    }
}
