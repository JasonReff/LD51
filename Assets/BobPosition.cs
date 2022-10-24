using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobPosition : MonoBehaviour
{
    [SerializeField] private float _distance, _speed;
    private float _startingY;

    private void Start()
    {
        _startingY = transform.localPosition.y;
    }

    private void Update()
    {
        float pivot = Mathf.Sin(Time.time * _speed) * _distance;
        transform.localPosition = new Vector2(transform.localPosition.x, _startingY + pivot);
    }

    
}
