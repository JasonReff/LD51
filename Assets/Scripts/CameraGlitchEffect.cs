using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGlitchEffect : MonoBehaviour
{
    [SerializeField] private float _glitchDelay, _glitchDuration, _minimumDelay, _maximumDelay;
    [SerializeField] private ShaderEffect_CRT _crtEffect;

    private IEnumerator Start()
    {
        while (true)
        {
            SetGlitchDelay();
            yield return new WaitForSeconds(_glitchDelay);
            _crtEffect.enabled = true;
            yield return new WaitForSeconds(_glitchDuration);
            _crtEffect.enabled = false;

        }
    }

    private void SetGlitchDelay()
    {
        _glitchDelay = UnityEngine.Random.Range(_minimumDelay, _maximumDelay);
    }
}
