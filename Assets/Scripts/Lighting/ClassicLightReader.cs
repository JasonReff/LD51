using UnityEngine;

public class ClassicLightReader : MonoBehaviour
{
    [SerializeField] private CameraSettingsData _cameraSettings;
    [SerializeField] private IClassicLight _classicLight;

    private void Start()
    {
        if (_classicLight == null)
            _classicLight = TryGetComponent(out IClassicLight classicLight) ? classicLight : null;
        _classicLight?.SetClassicMode(_cameraSettings.ClassicLighting);
    }
}