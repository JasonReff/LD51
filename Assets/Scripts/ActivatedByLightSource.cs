using UnityEngine;
using UnityEngine.Events;

public class ActivatedByLightSource : MonoBehaviour
{
    public UnityEvent OnLight, OnSnuff;
    [SerializeField] private bool _playerOnly;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DarknessSource darkness))
        {
            OnSnuff?.Invoke();
        }
        else if (collision.TryGetComponent(out LightSource light))
        {
            var player = collision.GetComponentInParent<PlayerManager>();
            if (_playerOnly && player == null)
                return;
            OnLight?.Invoke();
        }
    }
}