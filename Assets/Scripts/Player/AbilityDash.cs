using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDash : PlayerAbility
{
    private PlayerMovement _movement;
    [SerializeField] private float _dashDuration, _dashSpeed;

    public override void Initialize(PlayerAbilityController controller)
    {
        base.Initialize(controller);
        _movement = controller.GetComponent<PlayerMovement>();
    }
    public override void UseAbility()
    {
        base.UseAbility();
        _controller.StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        var normalSpeed = _movement.PlayerSpeed;
        _movement.PlayerSpeed = _dashSpeed;
        _movement.IsDashing = true;
        yield return new WaitForSeconds(_dashDuration);
        _movement.PlayerSpeed = normalSpeed;
        _movement.IsDashing = false;
    }
}
