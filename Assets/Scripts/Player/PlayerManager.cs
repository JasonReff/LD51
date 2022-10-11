using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    [SerializeField] private PlayerAbilityController _abilityController;
    [SerializeField] private Animator _tombstoneAnimator;
    [SerializeField] private SpriteRenderer _player, _ghost, _tombstone;
    [SerializeField] private float _deathDuration = 3f, _ghostRiseDuration = 1f, _ghostRiseDistance = 1f, _tombstoneDuration = 1f;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private CharacterSelectData _selectedCharacter;
    private bool _isHoldingKey;
    public bool IsHoldingKey { get => _isHoldingKey; }
    public static event Action<bool> OnKeyChanged;
    public static event Action OnPlayerDeath;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        DamageCharacter.OnPlayerDamaged += PlayerDeath;
    }

    private void OnDisable()
    {
        DamageCharacter.OnPlayerDamaged -= PlayerDeath;
    }

    private void Start()
    {
        _player = GetComponent<SpriteRenderer>();
        _player.sprite = _selectedCharacter.SelectedCharacter.CharacterSprite;
        GetComponent<Animator>().runtimeAnimatorController = _selectedCharacter.SelectedCharacter.CharacterAnimatorController;
        _abilityController.SetAbility(_selectedCharacter.SelectedCharacter.Ability);
    }

    private void PlayerDeath()
    {
        StartCoroutine(GhostAnimation());
    }

    private IEnumerator GhostAnimation()
    {
        AudioManager.PlaySoundEffect(_deathSound);
        Time.timeScale = 0f;
        _player.enabled = false;
        _tombstone.enabled = true;
        _tombstoneAnimator.enabled = true;
        yield return new WaitForSecondsRealtime(_tombstoneDuration);
        _ghost.enabled = true;
        _ghost.sprite = _player.sprite;
        _ghost.DOFade(0f, _ghostRiseDuration).SetUpdate(true);
        _ghost.transform.DOLocalMoveY(_ghost.transform.localPosition.y + _ghostRiseDistance, _ghostRiseDuration).SetUpdate(true);
        yield return new WaitForSecondsRealtime(_ghostRiseDuration);
        OnPlayerDeath?.Invoke();
        _player.enabled = true;
        _ghost.enabled = false;
        _tombstone.enabled = false;
        _tombstoneAnimator.enabled = false;
    }

    public void SetKey(bool key)
    {
        _isHoldingKey = key;
        OnKeyChanged?.Invoke(key);
    }
}