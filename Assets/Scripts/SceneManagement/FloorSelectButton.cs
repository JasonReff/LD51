using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FloorSelectButton : MonoBehaviour
{
    [SerializeField] private Image _floorImage;
    [SerializeField] private string _floorNumber;
    public string FloorNumber { get => _floorNumber; }

    [SerializeField] private TextMeshProUGUI _floorTextbox;
    private FloorSelectManager _manager;

    public void Initialize(FloorSelectManager manager, string floor, Sprite floorSprite)
    {
        _floorNumber = floor;
        _floorTextbox.text = _floorNumber;
        _manager = manager;
        _floorImage.sprite = floorSprite;
    }

    public void ChooseFloor()
    {
        _manager.SelectFloor(_floorNumber);
    }
}