using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovementController
{
    [SerializeField]
    private float playerSpeed = .05f;
    [SerializeField] private Animator _animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    public bool IsDashing, CanMove, IsSlipping;

    public float PlayerSpeed { get => playerSpeed; set => playerSpeed = value; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        if (CanMove)
        {
            if (IsSlipping)
            {
                rb.AddForce(movement * playerSpeed);
            }
            else
            {
                rb.velocity = movement * playerSpeed;
            }
        }
            
    }

    private void UpdateAnimation()
    {
        _animator.SetBool("Moving", movement.magnitude > 0);
    }

    public void SetSpeed(float speed, bool slippery)
    {
        if (!IsDashing)
            playerSpeed = speed;
        IsSlipping = slippery;
    }
}

public interface IMovementController
{
    public void SetSpeed(float speed, bool slippery);
}
