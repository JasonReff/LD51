using System;
using UnityEngine;

public class DamageCharacter : MonoBehaviour
{
    public static event Action OnPlayerDamaged;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerManager player))
        {
            OnPlayerDamaged?.Invoke();
        }
    }
}
