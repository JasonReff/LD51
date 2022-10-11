using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneDoor : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    private float _minimumDuration = 1f;
<<<<<<< HEAD
    public void MoveToScene()
    {
        Time.timeScale = 1;
=======
    public static event Action OnLevelFinished;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioClip _doorOpenSound;
    public void MoveToScene()
    {
        OnLevelFinished?.Invoke();
        PlayAnimation();
>>>>>>> Jason
        SceneLoader.Instance.LoadScene(_sceneName, _minimumDuration);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerManager player))
        {
            MoveToScene();
        }
    }

    private void PlayAnimation()
    {
        _animator.SetBool("OpenDoor", true);
        AudioManager.PlaySoundEffect(_doorOpenSound);
    }
}