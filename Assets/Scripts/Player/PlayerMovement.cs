using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = .05f;
    [SerializeField] private Animator _animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    public bool IsDashing, CanMove, IsOnIce;

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
            rb.velocity = movement * playerSpeed;
        //implement ice
    }

    private void UpdateAnimation()
    {
        _animator.SetBool("Moving", movement.magnitude > 0);
    }

    public void SetSpeed(float speed)
    {
        playerSpeed = speed;
    }
}
