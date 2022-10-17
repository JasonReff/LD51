using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private float _delay, _duration;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioClip _onActivationSound, _onDeactivationSound;
    public UnityEvent OnActivate, OnDeactivate;
    private Coroutine _coroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerManager player))
        {
            _coroutine = StartCoroutine(ActivateCoroutine());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerManager player))
        {
            _coroutine = StartCoroutine(DeactivateCoroutine());
        }
    }

    private IEnumerator ActivateCoroutine()
    {
        yield return new WaitForSeconds(_delay);
        OnActivate?.Invoke();
        _animator.SetBool("IsActive", true);
        AudioManager.PlaySoundEffect(_onActivationSound);
    }

    private IEnumerator DeactivateCoroutine()
    {
        yield return new WaitForSeconds(_duration);
        OnDeactivate?.Invoke();
        _animator.SetBool("IsActive", false);
        AudioManager.PlaySoundEffect(_onDeactivationSound);
    }
}