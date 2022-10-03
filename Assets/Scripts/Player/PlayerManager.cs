using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    [SerializeField] private Animator _tombstoneAnimator;
    [SerializeField] private SpriteRenderer _ghost, _tombstone;
    [SerializeField] private float _deathDuration = 3f, _ghostRiseDuration = 1f, _ghostRiseDistance = 1f, _tombstoneDuration = 1f;
<<<<<<< HEAD
=======
    [SerializeField] private AudioClip _deathSound;
>>>>>>> master

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

    private void PlayerDeath()
    {
        StartCoroutine(GhostAnimation());
<<<<<<< HEAD
        SceneLoader.Instance.ReloadScene(_deathDuration);
=======
        //SceneLoader.Instance.ReloadScene(_deathDuration);
>>>>>>> master
    }

    private IEnumerator GhostAnimation()
    {
<<<<<<< HEAD
=======
        AudioManager.PlaySoundEffect(_deathSound);
>>>>>>> master
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
}
