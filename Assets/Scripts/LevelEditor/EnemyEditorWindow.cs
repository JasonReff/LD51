using UnityEngine;

namespace LevelEditor
{
    public class EnemyEditorWindow : MonoBehaviour
    {
        [SerializeField] private EnemyEditorManager _manager;

        public void CycleUp()
        {
            _manager.CycleEnemyUp();
        }

        public void CycleDown()
        {
            _manager.CycleEnemyDown();
        }

        public void AddPoint()
        {
            _manager.AddPointOnEnemy();
        }

        public void RemovePoint()
        {
            _manager.RemovePoint();
        }
    }
}
