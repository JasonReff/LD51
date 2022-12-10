using System;
using UnityEngine;

namespace LevelEditor
{
    public class TestButton : MonoBehaviour
    {
        public static event Action SpawnFloor, BakeTilemap, SpawnPlayer, SpawnEnemies, OnTestEnd;
        public void TestScene()
        {
            SpawnFloor?.Invoke();
            BakeTilemap?.Invoke();
            SpawnPlayer?.Invoke();
            SpawnEnemies?.Invoke();
        }

        public void EndTest()
        {
            OnTestEnd?.Invoke();
        }
    }
}
