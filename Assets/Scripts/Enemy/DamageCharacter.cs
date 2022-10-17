using System;
using System.Collections.Generic;
using UnityEngine;

public class DamageCharacter : MonoBehaviour
{
    public static event Action OnPlayerDamaged;
    private List<Collider2D> contacts = new List<Collider2D>();

    private void Start()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!enabled)
            return;
        if (collision.gameObject.TryGetComponent(out PlayerManager player))
        {
            OnPlayerDamaged?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enabled)
            return;
        if (collision.TryGetComponent(out PlayerManager player))
        {
            OnPlayerDamaged?.Invoke();
        }
    }

    private void OnEnable()
    {
        GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D().NoFilter(), contacts);
        foreach (var contact in contacts)
            if (contact.TryGetComponent(out PlayerManager player))
                OnPlayerDamaged?.Invoke();
    }

    public void CharacterTakesDamage()
    {
        OnPlayerDamaged?.Invoke();
    }
}
