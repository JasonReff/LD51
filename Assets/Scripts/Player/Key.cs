using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private AudioClip _keyGained;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerManager player))
        {
            player.SetKey(true);
            AudioManager.PlaySoundEffect(_keyGained);
            Destroy(gameObject);
        }
    }
}