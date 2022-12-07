using UnityEngine;
using DG.Tweening;

public class AudioManager : SingletonMonobehaviour<AudioManager>
{
    [SerializeField] private AudioSource _gameMusic, _ghostMusic, _pauseMusic, _effects;
    [SerializeField] private float _masterVolume, _musicVolume, _effectsVolume;
    [SerializeField] private SoundSettingsData _soundSettings;
    private static float _minPitch = 0.9f, _maxpitch = 1.1f, _fadeDuration = 0.25f;

    private void OnEnable()
    {
        foreach (var source in GetComponents<AudioSource>())
            if (source.playOnAwake)
                source.Play();
        TimeFreeze.OnTimeFrozen += OnTimeFrozen;
    }

    private void OnDisable()
    {
        foreach (var source in GetComponents<AudioSource>())
            source.Stop();
        TimeFreeze.OnTimeFrozen -= OnTimeFrozen;
    }

    private void Start()
    {
        
        _masterVolume = _soundSettings.MasterVolume;
        SetMusicVolume(_soundSettings.MusicVolume);
        SetEffectsVolume(_soundSettings.EffectsVolume);
    }

    public static void PlaySoundEffect(AudioClip audioClip)
    {
        if (audioClip == null)
            return;
        float randomPitch = Random.Range(_minPitch, _maxpitch);
        Instance._effects.pitch = randomPitch;
        Instance._effects.PlayOneShot(audioClip);
    }

    public static void SetMusicVolume(float volume)
    {
        Instance._gameMusic.volume = volume * Instance._masterVolume;
        Instance._pauseMusic.volume = volume * Instance._masterVolume;
    }

    public static void SwapMusic()
    {
        Instance._gameMusic.DOFade(0f, _fadeDuration);
        Instance._ghostMusic.DOFade(1f * Instance._musicVolume * Instance._masterVolume, _fadeDuration);
    }

    public static void PauseGameMusic()
    {
        Instance._gameMusic.Pause();
        if (Instance._pauseMusic.isPlaying)
            Instance._pauseMusic.UnPause();
        else Instance._pauseMusic.Play();
    }

    public static void UnpauseGameMusic()
    {
        Instance._gameMusic.UnPause();
        Instance._pauseMusic.Pause();
    }

    public static void SetEffectsVolume(float volume)
    {
        Instance._effects.volume = volume * Instance._masterVolume;
    }

    public static void SetMasterVolume(float volume)
    {
        Instance._masterVolume = volume;
        SetEffectsVolume(Instance._effectsVolume);
        SetMusicVolume(Instance._musicVolume);
    }

    public static float MusicVolume()
    {
        return Instance._musicVolume;
    }

    public static float EffectsVolume()
    {
        return Instance._effectsVolume;
    }

    public static float MasterVolume()
    {
        return Instance._masterVolume;
    }

    private void OnTimeFrozen(bool frozen)
    {
        if (frozen)
            _gameMusic.Pause();
        else _gameMusic.UnPause();
    }
}
