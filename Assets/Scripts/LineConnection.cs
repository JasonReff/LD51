using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LineConnection : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private GameObject _otherConnection;
    private Transform _otherTransform;
    private bool _characterTouched;
    public UnityEvent CharacterTouchesLine;

    private void OnEnable()
    {
        _otherTransform = _otherConnection.transform;
    }

    private void Update()
    {
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, _otherTransform.position);
        if (DetectPlayer())
            CharacterTouchesLine?.Invoke();
    }

    private bool DetectPlayer()
    {
        if (_characterTouched)
            return false;
        List<RaycastHit2D> results = new List<RaycastHit2D>();
        int hits = Physics2D.Linecast(transform.position, _otherTransform.position, new ContactFilter2D().NoFilter(), results);
        foreach (var hit in results)
        {
            if (hit.collider.TryGetComponent(out PlayerManager player))
            {
                _characterTouched = true;
                return true;
            }
        }
        return false;
    }
}
