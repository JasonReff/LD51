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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        rb.velocity = movement * playerSpeed;
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
