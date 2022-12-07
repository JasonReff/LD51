using UnityEngine;

namespace LevelEditor
{
    public class EditorSpawnObject<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private T _prefab;
        [SerializeField] private SpriteRenderer _sr;
        private T _instance;
        private void OnEnable()
        {
            TestButton.OnTestStart += OnSceneStart;
            TestButton.OnTestEnd += OnSceneEnd;
        }

        private void OnDisable()
        {
            TestButton.OnTestStart -= OnSceneStart;
            TestButton.OnTestEnd -= OnSceneEnd;
        }

        public virtual void OnSceneStart()
        {
            _sr.enabled = false;
            _instance = Instantiate(_prefab, transform.position, transform.rotation);
        }

        public virtual void OnSceneEnd()
        {
            _sr.enabled = true;
            if (_instance.gameObject != null)
                Destroy(_instance.gameObject);
            _instance = null;
        }
    }
}
