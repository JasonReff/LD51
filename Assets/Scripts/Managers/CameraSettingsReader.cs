using UnityEngine;

public class CameraSettingsReader : MonoBehaviour
{
    [SerializeField] private CameraSettingsData _cameraSettingsData;

    private void Start()
    {
        if (_cameraSettingsData.PostProcessing)
        {

        }
    }
}