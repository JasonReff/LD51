using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderManager : SingletonMonobehaviour<ThunderManager>
{
    [SerializeField] private SoundEffectPool _thunderPool;

    private void OnEnable()
    {
        LightningStrikeManager.OnLightningStrikeStart += PlayThunder;
    }

    private void OnDisable()
    {
        LightningStrikeManager.OnLightningStrikeStart -= PlayThunder;
    }

    private void PlayThunder()
    {
        AudioManager.PlaySoundEffect(_thunderPool.RandomClip());
    }
}
