using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMovementModifier : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private bool _isSlippery;
    public bool IsSlippery { get => _isSlippery; set => _isSlippery = value; }
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IMovementController movementController))
        {
            movementController.SetSpeed(_moveSpeed, _isSlippery);
        }
    }
}
