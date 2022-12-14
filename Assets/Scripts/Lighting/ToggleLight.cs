using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class ToggleLight : MonoBehaviour, IClassicLight
{
    [SerializeField] private Material _classicMaterial, _litMaterial;
    protected bool _isLightningStriking, _isClassic;
    private List<Collider2D> colliders;
    private SpriteRenderer _sr;
    protected float _fadeDuration = 0.5f;
    protected Tween _fadeTween;
    private (float, float)[] _flickers = new (float, float)[] {(0f, 0.05f), (0.1f, 0.05f), (0.2f, 0.1f), (0.3f, 0f)};

    private void OnEnable()
    {
        LightningStrikeManager.OnLightningStrikeStart += OnLightningStrikeStart;
    }

    private void OnDisable()
    {
        LightningStrikeManager.OnLightningStrikeStart -= OnLightningStrikeStart;
    }

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (LightningStrikeManager.Instance != null)
            ChangeVisibilityTweenless(false);
    }

    public virtual void ChangeVisibility(bool visibility)
    {
        if (visibility && !IsTouchingDarknessSource())
            ChangeAlpha(1f);
        else if ((!_isLightningStriking && !IsTouchingLightSource()) | IsTouchingDarknessSource())
            _fadeTween = GetComponent<SpriteRenderer>().DOFade(0, _fadeDuration);
    }

    public virtual void ChangeVisibilityTweenless(bool visibility)
    {
        if (visibility && !IsTouchingDarknessSource())
            ChangeAlpha(1f);
        else if (IsTouchingDarknessSource() | !IsTouchingLightSource())
            ChangeAlpha(0f);
    }

    protected void ChangeAlpha(float alpha)
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


    protected bool IsTouchingLightSource()
    {
        colliders = new List<Collider2D>();
        GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D().NoFilter(), colliders);
        if (colliders.Count == 0)
            return false;
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent<LightSource>(out LightSource lightSource))
            {
                if (!lightSource.IsSnuffed && lightSource.enabled == true)
                    return true;
            }
        }
        return false;
    }

    protected bool IsTouchingDarknessSource()
    {
        colliders = new List<Collider2D>();
        GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D().NoFilter(), colliders);
        if (colliders.Count == 0)
            return false;
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out DarknessSource darknessSource))
            {
                return true;
            }
        }
        return false;
    }

    public void OnDarknessLeft()
    {
        if (IsTouchingLightSource())
        {
            ChangeVisibilityTweenless(true);
        }
    }

    public void SetClassicMode(bool classic)
    {
        _isClassic = classic;
        if (classic)
        {
            _sr.material = _classicMaterial;
            if (TryGetComponent(out ShadowCaster2D shadow))
            {
                shadow.enabled = false;
            }
        }
        else
        {
            _sr.material = _litMaterial;
            if (TryGetComponent(out ShadowCaster2D shadow))
            {
                shadow.enabled = true;
            }
            enabled = false;
            ChangeAlpha(1f);
        }
    }
}

public interface IClassicLight
{
    public void SetClassicMode(bool classic);
}
