using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WallHider : MonoBehaviour
{
    [SerializeField] private bool _drawGizmos;
    [SerializeField] private int _wallCount, _minimumCorners = 1;
    private BoxCollider2D _collider;
    private SpriteRenderer _sr;
    private ShadowCaster2D _shadow;
    private List<Light2D> _lights, _pointLights;
    private Light2D _nearestLight;
    private List<Vector2> _corners = new List<Vector2>();

    private void OnEnable()
    {
        _sr = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
        _shadow = GetComponent<ShadowCaster2D>();
        _lights = Light2DManager.lights;
    }

    private void OnDrawGizmos()
    {
        if (!_drawGizmos)
            return;
        foreach (var corner in _corners)
        {
            Gizmos.DrawSphere(corner, 0.1f);
            if (PlayerManager.Instance != null)
                Gizmos.DrawLine(corner, PlayerManager.Instance.transform.position);
        }
    }

    private void Start()
    {
        GetCorners();
    }

    private void Update()
    {
        _pointLights = _lights.Where(t => t.enabled && t.lightType != Light2D.LightType.Global).ToList();
        _nearestLight = _pointLights.OrderBy(t => Vector2.SqrMagnitude(t.transform.position - transform.position)).FirstOrDefault();
        if (_nearestLight != null)
        {
            _shadow.selfShadows = !AreAnyEdgesVisible(_nearestLight.transform.position);
        }
    }

    private void GetCorners()
    {
        var bottomLeft = (Vector2)_sr.bounds.center - (Vector2)(0.5f * _sr.bounds.size);
        var bottomRight = (Vector2)_sr.bounds.center + new Vector2(0.5f*_sr.bounds.size.x, 0.5f*-_sr.bounds.size.y);
        var topLeft = (Vector2)_sr.bounds.center + new Vector2(0.5f*-_sr.bounds.size.x, 0.5f*_sr.bounds.size.y);
        var topRight = (Vector2)_sr.bounds.center + (Vector2)(0.5f * _sr.bounds.size);
        _corners = new List<Vector2>() { bottomLeft, bottomRight, topLeft, topRight};
    }

    private bool AreAnyEdgesVisible(Vector2 lightPosition)
    {
        int visibleCorners = 0;
        foreach (var corner in _corners)
        {
            if (!IsHittingWall(corner, lightPosition))
            {
                visibleCorners++;
            }
        }
        if (visibleCorners >= _minimumCorners)
            return true;
        return false;
    }

    private bool IsHittingWall(Vector2 corner, Vector2 newPosition)
    {
        List<RaycastHit2D> results = new List<RaycastHit2D>();
        int hits = Physics2D.Linecast(corner, newPosition, new ContactFilter2D().NoFilter(), results);
        int wallCount = 0;
        foreach (var hit in results)
        {
            if (hit.transform.gameObject.tag == "Wall")
            {
                wallCount++;
            }
        }
        if (wallCount > _wallCount)
            return true;
        return false;
    }
}
