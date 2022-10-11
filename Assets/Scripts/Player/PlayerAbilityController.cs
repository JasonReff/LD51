using UnityEngine;

public class PlayerAbilityController : MonoBehaviour
{
    [SerializeField] private PlayerAbility _ability;
    private float _cooldown, _cooldownTimer;

    public void SetAbility(PlayerAbility ability)
    {
        if (_ability == null)
            return;
        _ability = ability;
        _ability.Initialize(this);
        _cooldown = _ability.Cooldown;
    }

    private void Update()
    {
        if (_cooldownTimer > 0f)
        {
            _cooldownTimer -= Time.deltaTime;
            return;
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (_ability == null)
                return;
            _ability.UseAbility();
            _cooldownTimer = _cooldown;
        }
    }
}