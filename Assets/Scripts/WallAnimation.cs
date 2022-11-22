using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAnimation : MonoBehaviour
{
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private bool _startAtRandomTime, _randomAnimationOrder, _variableDelay;
    [SerializeField] private float _animationRate, _animationDelay, _minDelay, _maxDelay;
    private float _delayTimer;
    private SpriteRenderer _sr;
    private Coroutine _delayCoroutine;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (_startAtRandomTime)
            SetTimer();
        _delayCoroutine = StartCoroutine(DelayCoroutine());
    }

    private void SetTimer()
    {
        _delayTimer = Random.Range(0f, _animationDelay);
    }

    private IEnumerator DelayCoroutine()
    {
        if (_variableDelay)
            _animationDelay = Random.Range(_minDelay, _maxDelay);
        yield return new WaitForSeconds(_animationDelay - _delayTimer);
        StartCoroutine(AnimationCoroutine());
        _delayTimer = 0f;
        _delayCoroutine = StartCoroutine(DelayCoroutine());
    }

    private IEnumerator AnimationCoroutine()
    {
        var sprites = new List<Sprite>(_sprites);
        if (_randomAnimationOrder)
            sprites = _sprites.ShuffleAndCopy();
        foreach (var sprite in sprites)
        {
            _sr.sprite = sprite;
            yield return new WaitForSeconds(_animationRate);
        }
    }
}
