using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField] private Image _keyImage;

    private void OnEnable()
    {
        PlayerManager.OnKeyChanged += OnKeyChanged;
    }

    private void OnDisable()
    {
        PlayerManager.OnKeyChanged -= OnKeyChanged;
    }
    private void OnKeyChanged(bool key)
    {
        _keyImage.enabled = key;
    }
}