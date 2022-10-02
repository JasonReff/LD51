using UnityEngine;
using DG.Tweening;

public class AudioManager : SingletonMonobehaviour<AudioManager>
{
    [SerializeField] private AudioSource _gameMusic, _ghostMusic, _pauseMusic, _effects;
    [SerializeField] private float _masterVolume, _musicVolume, _effectsVolume;
    private static float _minPitch = 0.9f, _maxpitch = 1.1f, _fadeDuration = 0.25f;

    public static void PlaySoundEffect(AudioClip audioClip)
    {
        float randomPitch = Random.Range(_minPitch, _maxpitch);
        Instance._effects.pitch = randomPitch;
        Instance._effects.PlayOneShot(audioClip);
    }

    public static void SetMusicVolume(float volume)
    {
        Instance._musicVolume = volume;
        Instance._gameMusic.volume = volume * Instance._masterVolume;
        Instance._pauseMusic.volume = volume * Instance._masterVolume;
    }

    public static void SwapMusic()
    {
        Instance._gameMusic.DOFade(0f, _fadeDuration);
        Instance._ghostMusic.DOFade(1f, _fadeDuration);
    }

    public static void PauseGameMusic()
    {
        Instance._gameMusic.Pause();
        Instance._pauseMusic.UnPause();
    }

    public static void UnpauseGameMusic()
    {
        Instance._gameMusic.UnPause();
        Instance._pauseMusic.Pause();
    }

    public static void SetEffectsVolume(float volume)
    {
        Instance._effectsVolume = volume;
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
}