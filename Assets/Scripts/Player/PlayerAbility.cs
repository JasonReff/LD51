using UnityEngine;

[CreateAssetMenu(menuName = "Ability")]
public class PlayerAbility : ScriptableObject
{
    [SerializeField] private float _cooldown;
    public float Cooldown { get => _cooldown; }
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
