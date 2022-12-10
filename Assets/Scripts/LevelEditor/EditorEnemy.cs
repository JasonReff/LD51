using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LevelEditor
{
    public class EditorEnemy : EditorSpawnObject<EnemyBase>, IPointerClickHandler
    {
        public static event Action<EditorEnemy> OnEnemyClicked;
        private List<Transform> _patrolPoints = new List<Transform>();
        private bool _pointsVisible;
        [SerializeField] private LineRenderer _lr;
        public void OnPointerClick(PointerEventData eventData)
        {
            OnEnemyClicked?.Invoke(this);
        }

        public void Select()
        {
            OnEnemyClicked?.Invoke(this);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            TestButton.SpawnEnemies += OnSpawnEnemies;
            OnEnemyClicked += AnyEnemyClicked;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            TestButton.SpawnEnemies -= OnSpawnEnemies;
            OnEnemyClicked -= AnyEnemyClicked;
        }

        private void AnyEnemyClicked(EditorEnemy enemy)
        {
            if (enemy == this && !_pointsVisible)
            {
                _pointsVisible = true;
            }
            else
            {
                _pointsVisible = false;
            }
            TogglePatrolPoints(_pointsVisible);
        }

        private void TogglePatrolPoints(bool enabled)
        {
            foreach (var point in _patrolPoints)
                point.GetComponent<SpriteRenderer>().enabled = enabled;
        }

        private void OnSpawnEnemies()
        {
            SpawnObject();
            if (_instance is PatrolEnemy patrol)
            {
                patrol.SetPatrolPoints(_patrolPoints);
            }
        }

        public void AddPoint(Transform point)
        {
            _patrolPoints.Add(point);
        }

        public void RemovePoint(Transform point)
        {
            _patrolPoints.Remove(point);
        }

        public EditorPatrolPoint GetLastPatrolPoint()
        {
            if (_patrolPoints.Count > 0)
            {
                return _patrolPoints[_patrolPoints.Count - 1].GetComponent<EditorPatrolPoint>();
            }
            return null;
        }

        public void ShowPath()
        {
            Vector3[] positions = new Vector3[_patrolPoints.Count];
            for (int i = 0; i < _patrolPoints.Count; i++)
            {
                Transform point = _patrolPoints[i];
                positions[i] = point.position;
            }
            _lr.SetPositions(positions);
        }
    }
}
