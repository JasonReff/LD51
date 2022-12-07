using System;
using UnityEngine;

namespace LevelEditor
{
    public class TestButton : MonoBehaviour
    {
        public static event Action OnTestStart, OnTestEnd;
        public void TestScene()
        {
            OnTestStart?.Invoke();
        }

        public void EndTest()
        {
            OnTestEnd?.Invoke();
        }
    }
}
