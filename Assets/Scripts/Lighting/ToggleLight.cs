using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ToggleLight : MonoBehaviour
{

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
        else
            gameObject.layer = 7;
    }

    private void OnLightningStrikeStart()
    {
        ChangeVisibility(true);
    }

    private void OnLightningStrikeEnd()
    {
        ChangeVisibility(false);
        var colliders = new List<Collider2D>();
        GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D().NoFilter(), colliders);
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent<LightSource>(out LightSource lightSource))
            {
                ChangeVisibility(true);
                return;
            }
        }
    }
}
