using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoTweenCapacityManager : MonoBehaviour
{
    
    void Start()
    {
        DOTween.SetTweensCapacity(2000, 100);
    }

}
