using TMPro;
using UnityEngine;

public class FloorSelectButton : MonoBehaviour
{
    [SerializeField] private string _floorNumber;
    public string FloorNumber { get => _floorNumber; }

    [SerializeField] private TextMeshProUGUI _floorTextbox;
    private FloorSelectManager _manager;

    public void Initialize(FloorSelectManager manager, string floor)
    {
        _floorNumber = floor;
        _floorTextbox.text = _floorNumber;
        _manager = manager;
    }

    public void ChooseFloor()
    {
        _manager.SelectFloor(_floorNumber);
    }
}