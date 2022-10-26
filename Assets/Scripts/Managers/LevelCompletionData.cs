using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEditor;
using System.IO;

[CreateAssetMenu(menuName = "LevelCompletionData")]
public class LevelCompletionData : ScriptableObject
{
    [SerializeField] private List<CharacterData> _characterDatas;
    [SerializeField] private List<StageCompletionData> _stageDatas;
    private List<CharacterData> _completedCharacters = new List<CharacterData>();
    [Serializable]
    class StageCompletionData
    {
        public string StageName;
        public List<CharacterCompletion> CharactersCompleted = new List<CharacterCompletion>();

        public void ResetCompletion()
        {
            for (int i = 0; i < CharactersCompleted.Count; i++)
            {
                CharacterCompletion completion = CharactersCompleted[i];
                completion.CompletionStatus = false;
            }
        }

        public void ResetCharacters(List<CharacterData> allCharacters)
        {
            CharactersCompleted.Clear();
            foreach (var character in allCharacters)
            {
                CharactersCompleted.Add((character, false));
            }
        }
    }

    public List<CharacterData> GetCompletedCharacters(string stageName)
    {
        _completedCharacters.Clear();

        var stage = _stageDatas.FirstOrDefault(t => t.StageName == stageName);
        if (stage != null)
        {
            foreach (var completion in stage.CharactersCompleted)
            {
                if (completion.CompletionStatus == true)
                    _completedCharacters.Add(completion.Character);
            }
        }

        return _completedCharacters;
    }

    public List<string> GetStageNames()
    {
        var names = new List<string>();
        foreach (var data in _stageDatas)
            names.Add(data.StageName);
        return names;
    }

    public void SetCompletionStatus(CharacterData characterData, string stageName, bool completed)
    {
        var stageData = _stageDatas.FirstOrDefault(t => t.StageName == stageName);
        if (stageData != null)
        {
            (CharacterData, bool) characterCompletion;
            if (stageData.CharactersCompleted.Any(t => t.Character == characterData))
                characterCompletion = stageData.CharactersCompleted.FirstOrDefault(t => t.Character == characterData);
            else
            {
                Debug.LogError(characterData.name + " not found in completion data. Reset completion data or insert missing characters.");
                return;
            }
            characterCompletion.Item2 = completed;
        }
    }

    public void ResetAllCompletionData()
    {
        foreach (var stage in _stageDatas)
        {
            stage.ResetCompletion();
        }
    }

    public void ResetAllCharacterData()
    {
        foreach (var stage in _stageDatas)
        {
            stage.ResetCharacters(_characterDatas);
        }
    }

#if UNITY_EDITOR
    public void AddMissingStages()
    {
        var scenes = EditorBuildSettings.scenes;
        foreach (var scene in scenes)
        {
            if (scene.path.Contains("Level") && scene.enabled)
            {
                var sceneName = Path.GetFileNameWithoutExtension(scene.path);
                if (!_stageDatas.Any(t => t.StageName == sceneName))
                {
                    var stageData = new StageCompletionData() { StageName = sceneName };
                    stageData.ResetCharacters(_characterDatas);
                    _stageDatas.Add(stageData);
                }
            }
        }
    }
#endif
}

[Serializable]
class CharacterCompletion
{
    public CharacterData Character;
    public bool CompletionStatus;

    public CharacterCompletion(CharacterData item1, bool item2)
    {
        Character = item1;
        CompletionStatus = item2;
    }

    public override bool Equals(object obj)
    {
        return obj is CharacterCompletion other &&
               EqualityComparer<CharacterData>.Default.Equals(Character, other.Character) &&
               CompletionStatus == other.CompletionStatus;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Character, CompletionStatus);
    }

    public void Deconstruct(out CharacterData item1, out bool item2)
    {
        item1 = Character;
        item2 = CompletionStatus;
    }

    public static implicit operator (CharacterData, bool)(CharacterCompletion value)
    {
        return (value.Character, value.CompletionStatus);
    }

    public static implicit operator CharacterCompletion((CharacterData, bool) value)
    {
        return new CharacterCompletion(value.Item1, value.Item2);
    }
}