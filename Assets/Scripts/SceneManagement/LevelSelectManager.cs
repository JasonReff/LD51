using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{
    [SerializeField] private LevelSelectLevel _levelPrefab;
    private List<LevelSelectLevel> _levels = new List<LevelSelectLevel>();
    [SerializeField] private LevelCompletionData _completionData;
    [SerializeField] private Transform _levelParent;
    private List<string> _stages = new List<string>();
    [SerializeField] private FloorSelectManager _floorSelect;
    [SerializeField] private LevelSelectData _levelData;
    [SerializeField] private Image _enemyImage;

    public void GetFloorLevels(string floorNumber)
    {
        ClearStages();
        GetStages(floorNumber);
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

    private void GetStages(string floorNumber)
    {
        _stages = _completionData.GetStageNames();
        foreach (var name in _stages)
            if (name[name.Length - 3].ToString() == floorNumber)
                SpawnListLevel(name);
        int floor = int.Parse(floorNumber);
        LoadEnemySprite(floor);
    }

    private void SpawnListLevel(string stageName)
    {
        var level = Instantiate(_levelPrefab, _levelParent);
        level.Initialize(stageName);
        _levels.Add(level);
    }

    public void ExitLevelSelect()
    {
        _floorSelect.ShowFloors();
        gameObject.SetActive(false);
    }

    private void LoadEnemySprite(int floor)
    {
        _enemyImage.sprite = _levelData.EnemySprites[floor - 1];
    }
}
