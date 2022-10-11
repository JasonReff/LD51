using UnityEngine;
using DG.Tweening;

public class InverseToggleLight : ToggleLight
{
    public override void ChangeVisibility(bool visibility)
    {
        if (visibility && !IsTouchingDarknessSource())
            _fadeTween = GetComponent<SpriteRenderer>().DOFade(0, _fadeDuration);
        else if ((!_isLightningStriking && !IsTouchingLightSource()) | IsTouchingDarknessSource())
            ChangeAlpha(1f);
    }

    public override void ChangeVisibilityTweenless(bool visibility)
    {
        if (visibility && IsTouchingDarknessSource())
            ChangeAlpha(0f);
        else if (IsTouchingDarknessSource() | !IsTouchingLightSource())
            ChangeAlpha(1f);
    }
}