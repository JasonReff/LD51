using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;

[CreateAssetMenu(menuName = "BestTimesData")]
public class BestTimesData : ScriptableObject
{
    [SerializeField] private float _defaultTime = 120f;
    [SerializeField] private List<StageData> _stageDatas = new List<StageData>();
    [Serializable]
    class StageData
    {
        public string StageName;
        public float BestTime;
    }

    public float PullBestTime(string stageName)
    {
        var stage = _stageDatas.FirstOrDefault(t => t.StageName == stageName);
        if (stage == null) return 0f;
        return _stageDatas.First(t => t.StageName == stageName).BestTime;
    }

    public void RecordBestTime(string stageName, float time)
    {
        var stage = _stageDatas.FirstOrDefault(t => t.StageName == stageName);
        if (stage == null) return;
        stage.BestTime = time;
    }

    public void ResetTimes()
    {
        foreach (var stage in _stageDatas)
        {
            stage.BestTime = _defaultTime;
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
                    var stageData = new StageData() { StageName = sceneName, BestTime = _defaultTime };
                    _stageDatas.Add(stageData);
                }
            }
        }
    }
#endif
}
