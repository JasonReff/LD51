using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraSettingsReader : MonoBehaviour
{
    [SerializeField] private CameraSettingsData _cameraSettingsData;
    [SerializeField] private CameraGlitchEffect _glitchEffect;
    [SerializeField] private PostProcessLayer _postProcessingLayer;
    [SerializeField] private ShaderEffect_BleedingColors _bleedingColors;

    private void Start()
    {
        UpdateSettings();
    }

    public void UpdateSettings()
    {
        var postProcessingEnabled = _cameraSettingsData.PostProcessing;
        _glitchEffect.enabled = postProcessingEnabled;
        _postProcessingLayer.enabled = postProcessingEnabled;
        _bleedingColors.enabled = postProcessingEnabled;
    }
}