using UnityEngine;
using System;

public class TimeFreeze : MonoBehaviour
{
    public static event Action<bool> OnTimeFrozen;
    public void FreezeTime(bool frozen)
    {
        if (frozen)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        OnTimeFrozen?.Invoke(frozen);
    }
}