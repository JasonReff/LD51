using UnityEngine;
using System;
using System.Collections.Generic;

public class DestroyWithKey : MonoBehaviour
{
    public static event Action OnWallDestroyed;
    [SerializeField] private AudioClip _wallDestroyedClip;
    [SerializeField] private List<GameObject> _connectedWalls;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerManager player))
        {
            if (player.IsHoldingKey)
            {
                OpenDoor(player);
            }
        }
    }

    private void OpenDoor(PlayerManager player)
    {
        player.SetKey(false);
        AudioManager.PlaySoundEffect(_wallDestroyedClip);
        gameObject.SetActive(false);
        foreach (var wall in _connectedWalls)
            wall.SetActive(false);
        OnWallDestroyed?.Invoke();
    }
}
