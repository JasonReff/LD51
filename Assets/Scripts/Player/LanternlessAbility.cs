public class LanternlessAbility : PlayerAbility
{
    private LightSource _lightSource;
    public override void Initialize(PlayerAbilityController controller)
    {
        base.Initialize(controller);
        _lightSource = _controller.GetComponentInChildren<LightSource>();
        _lightSource.enabled = false;
    }
}