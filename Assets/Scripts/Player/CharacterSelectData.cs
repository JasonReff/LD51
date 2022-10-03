using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterSelectData")]
public class CharacterSelectData : ScriptableObject
{
    [SerializeField] private CharacterData _selectedCharacter;

    public CharacterData SelectedCharacter { get => _selectedCharacter; set => _selectedCharacter = value; }
}
