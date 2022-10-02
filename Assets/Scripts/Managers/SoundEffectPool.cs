using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SoundEffectPool")]
public class SoundEffectPool : ScriptableObject
{
    public List<AudioClip> SoundEffects = new List<AudioClip>();

    public AudioClip RandomClip()
    {
        if (SoundEffects.Count == 0)
            return null;
        var random = UnityEngine.Random.Range(0, SoundEffects.Count);
        return SoundEffects[random];
    }
}