using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ResolutionSettingsManager : MonoBehaviour
{
    [SerializeField] private List<Resolution> _resolutions;
    [SerializeField] private TextMeshProUGUI _textbox;
    private Resolution _currentRes;
    private bool _fullScreen;
    [Serializable]
    private struct Resolution
    {
        public int Width;
        public int Height;
    }

    private void Start()
    {
        var index = PlayerPrefs.GetInt("ResolutionIndex");
        _currentRes = _resolutions[index];
        SetResolution(_currentRes);
    }

    private void SetResolution(Resolution res)
    {
        Screen.SetResolution(res.Width, res.Height, _fullScreen);
        _textbox.text = $"{res.Width} x {res.Height}";
    }

    public void ToggleRes()
    {
        var index = _resolutions.IndexOf(_currentRes);
        int newIndex;
        if (index == _resolutions.Count - 1)
            newIndex = 0;
        else
            newIndex = index + 1;
        PlayerPrefs.SetInt("ResolutionIndex", newIndex);
        _currentRes = _resolutions[newIndex];
        SetResolution(_currentRes);
    }

    public void ToggleFullScreen()
    {
        _fullScreen = !_fullScreen;
        SetResolution(_currentRes);
    }
}