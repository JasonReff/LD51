using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    private PlayerMovement _playerMovement;
    [SerializeField] private PlayerAbilityController _abilityController;
    [SerializeField] private Animator _tombstoneAnimator;
    [SerializeField] private SpriteRenderer _player, _ghost, _tombstone;
    [SerializeField] private float _deathDuration = 3f, _ghostRiseDuration = 1f, _ghostRiseDistance = 1f, _tombstoneDuration = 1f;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private CharacterSelectData _selectedCharacter;
    private Dictionary<int, bool> _playerKeys = new Dictionary<int, bool>();
    public bool IsVisibleToEnemies { get => _isVisibleToEnemies; set => _isVisibleToEnemies = value; }

    [SerializeField] private bool _isVisibleToEnemies = true;
    public static event Action<bool, int> OnKeyChanged;
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
        _playerMovement = GetComponent<PlayerMovement>();
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
        _playerMovement.CanMove = false;
        _abilityController.DisableAbilityVisual();
        AudioManager.PlaySoundEffect(_deathSound);
        Time.timeScale = 0f;
        yield return StartCoroutine(GraveCoroutine());
        yield return StartCoroutine(GhostCoroutine());
        OnPlayerDeath?.Invoke();
        _isVisibleToEnemies = false;
        _tombstone.enabled = false;
        GetComponent<Collider2D>().enabled = false;

        IEnumerator GraveCoroutine()
        {
            _player.enabled = false;
            _tombstone.enabled = true;
            _tombstoneAnimator.enabled = true;
            yield return new WaitForSecondsRealtime(_tombstoneDuration);
        }
        IEnumerator GhostCoroutine()
        {
            _ghost.enabled = true;
            _ghost.sprite = _player.sprite;
            _ghost.DOFade(0f, _ghostRiseDuration).SetUpdate(true);
            _ghost.transform.DOLocalMoveY(_ghost.transform.localPosition.y + _ghostRiseDistance, _ghostRiseDuration).SetUpdate(true);
            yield return new WaitForSecondsRealtime(_ghostRiseDuration);
        }
    }

    public void SetKey(int keyID, bool key)
    {
        _playerKeys[keyID] = key;
        OnKeyChanged?.Invoke(key, keyID);
    }

    public bool IsHoldingKey(int keyID)
    {
        return _playerKeys[keyID];
    }

    public void ResetLife()
    {
        _abilityController.ResetAbilityVisual();
        _playerMovement.CanMove = true;
        _player.enabled = true;
        _ghost.enabled = false;
        _tombstone.enabled = false;
        _tombstoneAnimator.enabled = false;
        _isVisibleToEnemies = true;
        GetComponent<Collider2D>().enabled = true;
    }
}