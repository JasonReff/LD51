using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private AudioClip _keyGained;
    [SerializeField] private int _keyID;
    public int KeyID { get => _keyID; }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerManager player))
        {
            player.SetKey(_keyID, true);
            AudioManager.PlaySoundEffect(_keyGained);
            Destroy(gameObject);
        }
    }
}