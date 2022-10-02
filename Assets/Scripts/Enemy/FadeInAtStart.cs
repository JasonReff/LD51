using DG.Tweening;
using UnityEngine;

public class FadeInAtStart : MonoBehaviour
{
    [SerializeField] private float _fadeDuration;

    private void Start()
    {
        GetComponent<SpriteRenderer>().DOFade(1f, _fadeDuration);
    }
}