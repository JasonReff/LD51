using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    [SerializeField] private LevelSelectLevel _levelPrefab;
    private List<LevelSelectLevel> _levels = new List<LevelSelectLevel>();
    [SerializeField] private LevelCompletionData _completionData;
    [SerializeField] private Transform _levelParent;
    private List<string> _stages = new List<string>();

    private void OnEnable()
    {
        ClearStages();
        GetStages();
    }

    private void ClearStages()
    {
        for (int i = _levels.Count - 1; i >= 0; i--)
        {
            Destroy(_levels[i].gameObject);
        }
        _levels.Clear();
    }

    private void GetStages()
    {
        _stages = _completionData.GetStageNames();
        foreach (var name in _stages)
            SpawnListLevel(name);
    }

    private void SpawnListLevel(string stageName)
    {
        var level = Instantiate(_levelPrefab, _levelParent);
        level.Initialize(stageName);
        _levels.Add(level);
    }

}