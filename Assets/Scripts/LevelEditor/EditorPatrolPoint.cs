using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LevelEditor
{
    public class EditorPatrolPoint : MonoBehaviour, IPointerClickHandler, IDragHandler
    {
        public static event Action<EditorPatrolPoint> OnPointClicked;
        public EditorEnemy Enemy;
        private SpriteRenderer _sr;
        private Collider2D _collider;

        private void OnEnable()
        {
            _sr = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();
            TestButton.SpawnEnemies += OnSceneStart;
            TestButton.OnTestEnd += OnSceneEnd;
        }

        private void OnDisable()
        {
            TestButton.SpawnEnemies -= OnSceneStart;
            TestButton.OnTestEnd -= OnSceneEnd;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnPointClicked?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = point;
        }

        private void OnSceneStart()
        {
            _sr.enabled = false;
            _collider.enabled = false;
        }

        private void OnSceneEnd()
        {
            _sr.enabled = true;
            _collider.enabled = true;
        }
    }
}
