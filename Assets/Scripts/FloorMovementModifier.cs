using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMovementModifier : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMovement playerMovement))
        {
            if (!playerMovement.IsDashing)
                playerMovement.SetSpeed(_moveSpeed);
        }
    }
}
