using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[CreateAssetMenu(menuName = "BestTimesData")]
public class BestTimesData : ScriptableObject
{
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
}