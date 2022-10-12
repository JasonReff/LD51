using UnityEngine;
using UnityEngine.Events;

public class ActivatedByLightSource : MonoBehaviour
{
    public UnityEvent OnLight;
    [SerializeField] private bool _playerOnly;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out LightSource light))
        {
            if (_playerOnly && !collision.TryGetComponent(out PlayerManager player))
                return;
            OnLight?.Invoke();
        }
    }
}