using UnityEngine;
using UnityEngine.AI;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Vector2 _buffer;
    [SerializeField] private Teleport _exit;
    [SerializeField] private bool _playerOnly;

    public void TeleportGameObject(GameObject gameObject)
    {
        Vector3 buffer = gameObject.transform.position - transform.position;
        if (gameObject.TryGetComponent(out PlayerManager player))
        {
            player.transform.position = _exit.transform.position - buffer;
        }
        if (_playerOnly)
            return;
        if (gameObject.TryGetComponent(out EnemyBase enemy))
        {
            enemy.GetComponent<NavMeshAgent>().Warp(_exit.transform.position - buffer);
        }
    }
}