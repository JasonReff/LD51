using System.Collections;
using UnityEngine;
using DG.Tweening;

public class VanishAbility : PlayerAbility
{
    private PlayerManager _player;
    private SpriteRenderer _playerSprite;
    [SerializeField] private float _vanishDuration = 2f, _fadeTime = 0.25f, _fadeOpacity = 0.5f;

    public override void Initialize(PlayerAbilityController controller)
    {
        base.Initialize(controller);
        _player = _controller.GetComponent<PlayerManager>();
        _playerSprite = _controller.GetComponent<SpriteRenderer>();
    }

    public override void UseAbility()
    {
        base.UseAbility();
        _controller.StartCoroutine(AbilityCoroutine());
    }

    private IEnumerator AbilityCoroutine()
    {
        _player.IsVisibleToEnemies = false;
        _playerSprite.DOFade(_fadeOpacity, _fadeTime);
        yield return new WaitForSeconds(_vanishDuration);
        _player.IsVisibleToEnemies = true;
        _playerSprite.DOFade(1f, _fadeTime);
    }
}