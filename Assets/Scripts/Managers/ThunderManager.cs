using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderManager : SingletonMonobehaviour<ThunderManager>
{
    [SerializeField] private SoundEffectPool _thunderPool;
    [SerializeField] private float _thunderDelay;

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
        StartCoroutine(ThunderCoroutine());

        IEnumerator ThunderCoroutine()
        {
            yield return new WaitForSeconds(_thunderDelay);
            AudioManager.PlaySoundEffect(_thunderPool.RandomClip());
        }
    }
}
