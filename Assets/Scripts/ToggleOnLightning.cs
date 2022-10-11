using UnityEngine;

public class ToggleOnLightning : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private bool _enabledDuringLightning;

    private void OnEnable()
    {
        LightningStrikeManager.OnLightningStrikeStart += EnableComponent;
        LightningStrikeManager.OnLightningStrikeEnd += DisableComponent;
    }

    private void OnDisable()
    {
        LightningStrikeManager.OnLightningStrikeStart -= EnableComponent;
        LightningStrikeManager.OnLightningStrikeEnd -= DisableComponent;
    }

    private void EnableComponent()
    {
        _gameObject.SetActive(_enabledDuringLightning);
    }

    private void DisableComponent()
    {
        _gameObject.SetActive(!_enabledDuringLightning);
    }
}