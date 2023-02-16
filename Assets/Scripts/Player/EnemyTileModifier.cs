using System.Collections;
using UnityEngine;

public class EnemyTileModifier : MonoBehaviour
{
    [SerializeField] private Sprite _tileSprite;
    [SerializeField] private float _duration, _movementSpeed;
    [SerializeField] private bool _isSlippery;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out FloorMovementModifier floor))
        {
            floor.StartCoroutine(TileCoroutine(floor, _movementSpeed, _tileSprite));
        }
    }

    private IEnumerator TileCoroutine(FloorMovementModifier floor, float newSpeed, Sprite newSprite)
    {
        var movement = floor;
        var sr = floor.GetComponent<SpriteRenderer>();
        var sprite = sr.sprite;
        var baseSpeed = movement.MoveSpeed;
        sr.sprite = newSprite;
        movement.MoveSpeed = newSpeed;
        var slippery = movement.IsSlippery;
        movement.IsSlippery = _isSlippery;
        yield return new WaitForSeconds(_duration);
        sr.sprite = sprite;
        movement.MoveSpeed = baseSpeed;
        movement.IsSlippery = slippery;
    }
}