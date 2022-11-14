using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class LightningStrikeManager : SingletonMonobehaviour<LightningStrikeManager>
{
    private Coroutine _lightningCoroutine;
    [SerializeField] private float _lightningDelay, _lightningDuration, _initialDelay;
    public static event Action OnLightningStrikeStart, OnLightningStrikeEnd;
    [SerializeField] private Light2D _light;
    private (float, float)[] _flickers = new (float, float)[] { (0f, 0.05f), (0.1f, 0.05f), (0.2f, 0.1f), (0.3f, 0f) };
    private float _fadeDuration = 0.5f, _maxIntensity;

    private IEnumerator Start()
    {
        _maxIntensity = _light.intensity;
        _light.enabled = false;
        yield return new WaitForSeconds(_initialDelay);
        _lightningCoroutine = StartCoroutine(LightningCoroutine());
    }

    private IEnumerator LightningCoroutine()
    {
        StartCoroutine(FlickerCoroutine());
        yield return new WaitForSeconds(_lightningDuration);
        yield return new WaitForSeconds(_lightningDelay);
        _lightningCoroutine = StartCoroutine(LightningCoroutine());
    }

    IEnumerator FlickerCoroutine()
    {
        _light.intensity = _maxIntensity;
        foreach (var flicker in _flickers)
        {
            yield return new WaitForSeconds(flicker.Item1);
            _light.enabled = false;
            yield return new WaitForSeconds(flicker.Item2);
            _light.enabled = true;
        }
        StartCoroutine(FadeLight(_fadeDuration));
    }

    private IEnumerator FadeLight(float fadeDuration)
    {
        var intensity = _light.intensity;
        var time = fadeDuration;
        float value;
        while (time > 0f)
        {
            time -= Time.deltaTime;
            value = Mathf.Lerp(intensity, 0f, 1f - time / fadeDuration);
            _light.intensity = value;
            yield return null;
        }
    }
}
