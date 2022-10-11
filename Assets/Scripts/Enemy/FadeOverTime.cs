using DG.Tweening;
using System.Collections;
using UnityEngine;

public class FadeOverTime : MonoBehaviour
{
    [SerializeField] private float _fadeDuration, _destructionDelay;
    [SerializeField] private bool _doScale;

    private void Awake()
    {
        StartCoroutine(FadeCoroutine());
    }

    private IEnumerator FadeCoroutine()
    {
        if (_doScale)
            transform.DOScale(0f, _fadeDuration);
        else if (TryGetComponent(out SpriteRenderer renderer))
            renderer.DOFade(0f, _fadeDuration);
        yield return new WaitForSeconds(_fadeDuration + _destructionDelay);
        Destroy(gameObject);
    }
}
