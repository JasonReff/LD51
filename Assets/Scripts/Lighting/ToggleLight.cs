using System.Collections.Generic;
using UnityEngine;

public class ToggleLight : MonoBehaviour
{
    private bool _isLightningStriking;
    private List<Collider2D> colliders;

    private void OnEnable()
    {
        LightningStrikeManager.OnLightningStrikeStart += OnLightningStrikeStart;
        LightningStrikeManager.OnLightningStrikeEnd += OnLightningStrikeEnd;
    }

    private void OnDisable()
    {
        LightningStrikeManager.OnLightningStrikeStart -= OnLightningStrikeStart;
        LightningStrikeManager.OnLightningStrikeEnd -= OnLightningStrikeEnd;
    }

    public void ChangeVisibility(bool visibility)
    {
        if (visibility)
            gameObject.layer = 6;
        else if (!_isLightningStriking && !IsTouchingLightSource())
            gameObject.layer = 7;
    }

    private void OnLightningStrikeStart()
    {
        ChangeVisibility(true);
        _isLightningStriking = true;
    }

    private void OnLightningStrikeEnd()
    {
        _isLightningStriking = false;;
        ChangeVisibility(IsTouchingLightSource());
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
