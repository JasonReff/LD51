using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityController : MonoBehaviour
{
    [SerializeField] private PlayerAbility _ability;
    [SerializeField] private SpriteRenderer _readyEffect;
    private bool _abilityReady = false;
    private float _cooldown, _cooldownTimer;

    public void SetAbility(PlayerAbility ability)
    {
        if (ability == null)
            return;
        _ability = ability;
        _ability.Initialize(this);
        _cooldown = _ability.Cooldown;
        _readyEffect.color = _ability.Color;
        SetAbility(true);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && _abilityReady)
        {
            if (_ability == null)
                return;
            _ability.UseAbility();
            StartCoroutine(CooldownCoroutine());
        }
    }

    private IEnumerator CooldownCoroutine()
    {
        SetAbility(false);
        yield return new WaitForSeconds(_cooldown);
        SetAbility(true);
    }

    private void SetAbility(bool ready)
    {
        _abilityReady = ready;
        _readyEffect.enabled = ready;
    }
}