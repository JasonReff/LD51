using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public class EnemyEditorManager : MonoBehaviour
    {
        [SerializeField] private EnemyEditorWindow _window;
        [SerializeField] private EditorPatrolPoint _patrolPointPrefab;
        private EditorEnemy _enemy;
        private EditorPatrolPoint _patrolPoint;
        private List<EditorEnemy> _allEnemies = new List<EditorEnemy>();

        private void OnEnable()
        {
            EditorEnemy.OnEnemyClicked += SelectEnemy;
            EditorPatrolPoint.OnPointClicked += OnPointClicked;
        }

        private void OnDisable()
        {
            EditorEnemy.OnEnemyClicked -= SelectEnemy;
            EditorPatrolPoint.OnPointClicked -= OnPointClicked;
        }

        private void SelectEnemy(EditorEnemy enemy)
        {
            if (_enemy != enemy)
                _enemy = enemy;
            else _enemy = null;
            ShowOrHideWindow();
        }

        private void OnPointClicked(EditorPatrolPoint point)
        {
            _patrolPoint = point;
            _enemy = _patrolPoint.Enemy;
        }

        private void ShowOrHideWindow()
        {
            if (_enemy != null)
            {
                _window.gameObject.SetActive(true);
            }
            else _window.gameObject.SetActive(false);
        }

        public void CycleEnemyUp()
        {
            var enemy = _allEnemies.GetNext(_enemy);
            enemy.Select();
        }

        public void CycleEnemyDown()
        {
            var enemy = _allEnemies.GetPrev(_enemy);
            enemy.Select();
        }

        public void AddPointOnEnemy()
        {
            var point = Instantiate(_patrolPointPrefab, _enemy.transform.position, Quaternion.identity, _enemy.transform);
            point.Enemy = _enemy;
            _enemy.AddPoint(point.transform);
            _patrolPoint = point;
        }

        public void RemovePoint()
        {
            if (_patrolPoint == null)
                return;
            _enemy.RemovePoint(_patrolPoint.transform);
            Destroy(_patrolPoint.gameObject);
            _patrolPoint = _enemy.GetLastPatrolPoint();
        }
    }
}
