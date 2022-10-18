using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoTweenCapacityManager : MonoBehaviour
{
    [SerializeField] private int _maxTweens, _maxSequences;
    void Start()
    {
        DOTween.SetTweensCapacity(_maxTweens, _maxSequences);
    }

}
