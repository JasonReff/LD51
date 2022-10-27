using UnityEngine;

[CreateAssetMenu(menuName = "CharacterData")]
public class CharacterData : ScriptableObject
{
    public Sprite CharacterSprite;
    public RuntimeAnimatorController CharacterAnimatorController;
    public PlayerAbility Ability;
    public Color AbilityColor;
}