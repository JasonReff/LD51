using UnityEngine;

namespace LevelEditor
{
    public class EditorWall : EditorSpawnObject<MonoBehaviour>
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            TestButton.SpawnFloor += SpawnObject;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            TestButton.SpawnFloor -= SpawnObject;
        }
    }
}
