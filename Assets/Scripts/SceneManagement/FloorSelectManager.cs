using System.Collections.Generic;
using UnityEngine;

public class FloorSelectManager : MonoBehaviour
{
    [SerializeField] private FloorSelectButton _floorPrefab;
    private List<FloorSelectButton> _floors = new List<FloorSelectButton>();
    [SerializeField] private LevelCompletionData _completionData;
    [SerializeField] private Transform _floorParent;
    [SerializeField] private LevelSelectManager _levelManager;

    private void OnEnable()
    {
        HideFloors();
        ShowFloors();
    }

    public void ShowFloors()
    {
        foreach (var floorNumber in _completionData.GetFloorNames())
        {
            SpawnFloorButton(floorNumber);
        }
    }

    public void HideFloors()
    {
        for (int i = _floors.Count - 1; i >= 0; i--)
        {
            Destroy(_floors[i].gameObject);
        }
        _floors.Clear();
    }

    private void SpawnFloorButton(string floorNumber)
    {
        var floor = Instantiate(_floorPrefab, _floorParent);
        floor.Initialize(this, floorNumber);
        _floors.Add(floor);
    }

    public void SelectFloor(string floorNumber)
    {
        HideFloors();
        _levelManager.gameObject.SetActive(true);
        _levelManager.GetFloorLevels(floorNumber);
    }
}