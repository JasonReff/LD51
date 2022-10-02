using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterFlip : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    protected Vector3 _scale, _flippedScale;

    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _scale = transform.localScale;
        _flippedScale = new Vector3(_scale.x * -1, _scale.y, _scale.z);
    }

    private void Update()
    {
        SetDirection();
    }

    protected virtual void SetDirection()
    {
        if (_rb.velocity.x < -0.2)
            transform.localScale = _flippedScale;
        else if (_rb.velocity.x > 0.2)
            transform.localScale = _scale;
    }
}
