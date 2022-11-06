using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private Stack<PlayerCheckpoint> _activeCheckpoints = new Stack<PlayerCheckpoint>();
    private float _spawnDelay = 3f;

    private void OnEnable()
    {
        PlayerManager.OnPlayerDeath += OnPlayerDeath;
        PlayerCheckpoint.OnCheckpointActivated += OnCheckpointActivated;
        PlayerCheckpoint.OnCheckpointSnuffed += OnCheckpointSnuffed;
    }

    private void OnDisable()
    {
        PlayerManager.OnPlayerDeath -= OnPlayerDeath;
        PlayerCheckpoint.OnCheckpointActivated -= OnCheckpointActivated;
        PlayerCheckpoint.OnCheckpointSnuffed -= OnCheckpointSnuffed;
    }

    private void OnPlayerDeath()
    {
        if (_activeCheckpoints.Count == 0)
        {
            Time.timeScale = 1;
            SceneLoader.Instance.ReloadScene(true);
            return;
        }
        else
        {
            Time.timeScale = 1;
            StartCoroutine(RespawnCoroutine());
        }

        IEnumerator RespawnCoroutine()
        {
            yield return new WaitForSeconds(_spawnDelay);
            var checkpoint = _activeCheckpoints.Pop();
            PlayerManager.Instance.transform.position = checkpoint.transform.position;
            PlayerManager.Instance.ResetLife();
            checkpoint.UseCheckpoint();
        }
    }

    private void OnCheckpointActivated(PlayerCheckpoint checkpoint)
    {
        _activeCheckpoints.Push(checkpoint);
    }

    private void OnCheckpointSnuffed(PlayerCheckpoint checkpoint)
    {
        var checkpointList = _activeCheckpoints.ToList();
        checkpointList.Remove(checkpoint);
        _activeCheckpoints.Clear();
        for (int i = checkpointList.Count - 1; i >= 0; i--)
        {
            PlayerCheckpoint item = checkpointList[i];
            _activeCheckpoints.Push(item);
        }
    }
}
