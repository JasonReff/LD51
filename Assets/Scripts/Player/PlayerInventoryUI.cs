using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField] private List<Image> _keyImages = new List<Image>();

    private void OnEnable()
    {
        PlayerManager.OnKeyChanged += OnKeyChanged;
    }

    private void OnDisable()
    {
        PlayerManager.OnKeyChanged -= OnKeyChanged;
    }
    private void OnKeyChanged(bool key, int keyID)
    {
        _keyImages[keyID].enabled = key;
    }
}