using System.Collections;
using UnityEngine;

public class FlashAbility : PlayerAbility
{
    private LightSource _lightSource;
    [SerializeField] private float _flashDuration, _flashRadius;

    public override void Initialize(PlayerAbilityController controller)
    {
        base.Initialize(controller);
        _lightSource = _controller.GetComponentInChildren<LightSource>();
    }

    public override void UseAbility()
    {
        base.UseAbility();
        _controller.StartCoroutine(AbilityCoroutine());
    }

    private IEnumerator AbilityCoroutine()
    {
        var defaultRadius = _lightSource.GetComponent<CircleCollider2D>().radius;
        _lightSource.GetComponent<CircleCollider2D>().radius = _flashRadius;
        yield return new WaitForSeconds(_flashDuration);
        _lightSource.GetComponent<CircleCollider2D>().radius = defaultRadius;
    }
}
