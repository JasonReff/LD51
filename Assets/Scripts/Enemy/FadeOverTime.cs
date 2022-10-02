using DG.Tweening;
using System.Collections;
using UnityEngine;

public class FadeOverTime : MonoBehaviour
{
    [SerializeField] private float _fadeDuration, _destructionDelay;

    private void Awake()
    {
        StartCoroutine(FadeCoroutine());
    }

    private IEnumerator FadeCoroutine()
    {
        transform.DOScale(0f, _fadeDuration);
        yield return new WaitForSeconds(_fadeDuration + _destructionDelay);
        Destroy(gameObject);
    }
}