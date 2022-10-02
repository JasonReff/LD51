using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInAndOut : MonoBehaviour
{
    [SerializeField] private float _minAlpha, _maxAlpha, _fadeRate;

    private IEnumerator Start()
    {
        var rawImage = GetComponent<RawImage>();
        while (true)
        {
            rawImage.DOFade(_minAlpha, _fadeRate);
            yield return new WaitForSeconds(_fadeRate);
            rawImage.DOFade(_maxAlpha, _fadeRate);
            yield return new WaitForSeconds(_fadeRate);
        }
    }
}