using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class ToggleLight : MonoBehaviour
{
    private bool _isLightningStriking;
    private List<Collider2D> colliders;
    private SpriteRenderer _sr;
    private float _fadeDuration = 0.5f;
    private Tween _fadeTween;
    private (float, float)[] _flickers = new (float, float)[] {(0f, 0.05f), (0.1f, 0.05f), (0.2f, 0.1f), (0.3f, 0f)};

    private void OnEnable()
    {
        LightningStrikeManager.OnLightningStrikeStart += OnLightningStrikeStart;
    }

    private void OnDisable()
    {
        LightningStrikeManager.OnLightningStrikeStart -= OnLightningStrikeStart;
    }

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        ChangeVisibilityTweenless(false);
    }

    public void ChangeVisibility(bool visibility)
    {
        if (visibility)
            ChangeAlpha(1f);
        else if (!_isLightningStriking && !IsTouchingLightSource())
            _fadeTween = GetComponent<SpriteRenderer>().DOFade(0, _fadeDuration);
    }

    public void ChangeVisibilityTweenless(bool visibility)
    {
        if (visibility)
            ChangeAlpha(1f);
        else if (!IsTouchingLightSource())
            ChangeAlpha(0f);
    }

    private void ChangeAlpha(float alpha)
    {
        if (_fadeTween != null)
            _fadeTween.Kill();
        Color color = new Color(_sr.color.r, _sr.color.g, _sr.color.b, alpha);
        _sr.color = color;
    }

    private void OnLightningStrikeStart()
    {
        StartCoroutine(LightningCoroutine());

        IEnumerator LightningCoroutine()
        {
            _isLightningStriking = true;
            foreach (var flicker in _flickers)
            {
                yield return new WaitForSeconds(flicker.Item1);
                ChangeVisibilityTweenless(false);
                yield return new WaitForSeconds(flicker.Item2);
                ChangeVisibility(true);
            }
            _isLightningStriking = false;
            ChangeVisibility(false);
        }
    }


    private bool IsTouchingLightSource()
    {
        colliders = new List<Collider2D>();
        GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D().NoFilter(), colliders);
        if (colliders.Count == 0)
            return false;
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent<LightSource>(out LightSource lightSource))
            {
                return true;
            }
        }
        return false;
    }
}