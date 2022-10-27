using UnityEngine;

[CreateAssetMenu(menuName = "Ability")]
public class PlayerAbility : ScriptableObject
{
    [SerializeField] private float _cooldown;
    [SerializeField] private Color _color;
    public float Cooldown { get => _cooldown; }
    public Color Color { get => _color; }
    protected PlayerAbilityController _controller;
    
    public virtual void Initialize(PlayerAbilityController controller)
    {
        _controller = controller;
    }
    public void Tick()
    {
        
    }

    public virtual void UseAbility()
    {

    }
}
