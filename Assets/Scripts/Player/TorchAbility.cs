using UnityEngine;

public class TorchAbility : PlayerAbility
{
    [SerializeField] private PlayerCheckpoint _torch;
    [SerializeField] private bool _usedThisLevel;

    public override void Initialize(PlayerAbilityController controller)
    {
        base.Initialize(controller);
        _usedThisLevel = false;
    }

    public override void UseAbility()
    {
        base.UseAbility();
        if (_usedThisLevel)
            return;
        var torch = Instantiate(_torch, _controller.transform.position, Quaternion.identity);
        _usedThisLevel = true;
    }
}
