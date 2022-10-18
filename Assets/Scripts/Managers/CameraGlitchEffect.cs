using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGlitchEffect : MonoBehaviour
{
    [SerializeField] private float _glitchDelay, _glitchDuration, _minimumDelay, _maximumDelay;
    [SerializeField] private ShaderEffect_CRT _crtEffect;
    [SerializeField] private ShaderEffect_Unsync _unsyncEffect;

    [SerializeField] private bool _unsync;
    private Coroutine _glitchCoroutine;
    private IEnumerator GlitchCoroutine()
    {
        while (true)
        {
            SetGlitchDelay();
            yield return new WaitForSeconds(_glitchDelay);
            _crtEffect.enabled = true;
            if (_unsync)
                _unsyncEffect.enabled = true;
            yield return new WaitForSeconds(_glitchDuration);
            _crtEffect.enabled = false;
            if (_unsync)
                _unsyncEffect.enabled = false;
        }
    }

    private void OnDisable()
    {
        StopCoroutine(_glitchCoroutine);
    }

    private void OnEnable()
    {
        _glitchCoroutine = StartCoroutine(GlitchCoroutine());
    }

    private void SetGlitchDelay()
    {
        _glitchDelay = UnityEngine.Random.Range(_minimumDelay, _maximumDelay);
    }
}
