using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEditor;
using System.IO;

[CreateAssetMenu(menuName = "LevelCompletionData")]
public class LevelCompletionData : ScriptableObject
{
    [SerializeField] private float _defaultTime;
    [SerializeField] private List<CharacterData> _characterDatas;
    [SerializeField] private List<StageCompletionData> _stageDatas;
    private List<CharacterData> _completedCharacters = new List<CharacterData>();
    [Serializable]
    class StageCompletionData
    {
        public string StageName;
        public List<CharacterCompletion> CharactersCompleted = new List<CharacterCompletion>();

        public void ResetCompletion(float defaultTime)
        {
            for (int i = 0; i < CharactersCompleted.Count; i++)
            {
                CharacterCompletion completion = CharactersCompleted[i];
                completion.CompletionStatus = false;
                completion.BestCompletionTime = defaultTime;
            }
        }

        public void ResetCharacters(List<CharacterData> allCharacters, float defaultTime)
        {
            CharactersCompleted.Clear();
            foreach (var character in allCharacters)
            {
                CharactersCompleted.Add(new CharacterCompletion(character, false) { BestCompletionTime = defaultTime});
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

    public void SetCompletionStatus(CharacterData characterData, string stageName, bool completed, float completionTime)
    {
        var stageData = _stageDatas.FirstOrDefault(t => t.StageName == stageName);
        if (stageData != null)
        {
            CharacterCompletion characterCompletion;
            if (stageData.CharactersCompleted.Any(t => t.Character == characterData))
                characterCompletion = stageData.CharactersCompleted.FirstOrDefault(t => t.Character == characterData);
            else
            {
                Debug.LogError(characterData.name + " not found in completion data. Reset completion data or insert missing characters.");
                return;
            }
            characterCompletion.RecordTime(completionTime);
            characterCompletion.CompletionStatus = completed;
        }
    }

    public float PullBestTime(CharacterData characterData, string stageName)
    {
        var stageData = _stageDatas.FirstOrDefault(t => t.StageName == stageName);
        var time = _defaultTime;
        if (stageData != null)
        {
            CharacterCompletion characterCompletion;
            if (stageData.CharactersCompleted.Any(t => t.Character == characterData))
                characterCompletion = stageData.CharactersCompleted.FirstOrDefault(t => t.Character == characterData);
            else
            {
                Debug.LogError(characterData.name + " not found in completion data. Reset completion data or insert missing characters.");
                return time;
            }
            if (characterCompletion.CompletionStatus)
            {
                time = characterCompletion.BestCompletionTime;
            }
        }
        return time;
    }

    public void ResetAllCompletionData()
    {
        foreach (var stage in _stageDatas)
        {
            stage.ResetCompletion(_defaultTime);
        }
    }

    public void ResetAllCharacterData()
    {
        foreach (var stage in _stageDatas)
        {
            stage.ResetCharacters(_characterDatas, _defaultTime);
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
                    stageData.ResetCharacters(_characterDatas, _defaultTime);
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
    public float BestCompletionTime;

    public CharacterCompletion(CharacterData character, bool status)
    {
        Character = character;
        CompletionStatus = status;
    }

    public void RecordTime(float newTime)
    {
        if (CompletionStatus == false || newTime < BestCompletionTime)
            BestCompletionTime = newTime;
    }
}