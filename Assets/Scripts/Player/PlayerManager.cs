using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    [SerializeField] private Animator _tombstoneAnimator;
    [SerializeField] private SpriteRenderer _ghost, _tombstone;
    [SerializeField] private float _deathDuration = 3f, _ghostRiseDuration = 1f, _ghostRiseDistance = 1f, _tombstoneDuration = 1f;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private CharacterSelectData _selectedCharacter;
    private bool _isHoldingKey;
    public bool IsHoldingKey { get => _isHoldingKey; }
    public static event Action<bool> OnKeyChanged;

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
        GetComponent<SpriteRenderer>().sprite = _selectedCharacter.SelectedCharacter.CharacterSprite;
        GetComponent<Animator>().runtimeAnimatorController = _selectedCharacter.SelectedCharacter.CharacterAnimatorController;
    }

    private void PlayerDeath()
    {
        StartCoroutine(GhostAnimation());
        SceneLoader.Instance.ReloadScene(_deathDuration);
    }

    private IEnumerator GhostAnimation()
    {
        AudioManager.PlaySoundEffect(_deathSound);
        Time.timeScale = 0f;
        GetComponent<SpriteRenderer>().enabled = false;
        _tombstone.enabled = true;
        _tombstoneAnimator.enabled = true;
        yield return new WaitForSecondsRealtime(_tombstoneDuration);
        _ghost.enabled = true;
        _ghost.sprite = GetComponent<SpriteRenderer>().sprite;
        _ghost.DOFade(0f, _ghostRiseDuration).SetUpdate(true);
        _ghost.transform.DOLocalMoveY(_ghost.transform.localPosition.y + _ghostRiseDistance, _ghostRiseDuration).SetUpdate(true);
    }

    public void SetKey(bool key)
    {
        _isHoldingKey = key;
        OnKeyChanged?.Invoke(key);
    }
}
