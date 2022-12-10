using UnityEngine;

namespace LevelEditor
{
    public class EditorSpawnObject<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] protected T _prefab;
        [SerializeField] private SpriteRenderer _sr;
        protected T _instance;
        protected virtual void OnEnable()
        {
            TestButton.OnTestEnd += OnSceneEnd;
        }

        protected virtual void OnDisable()
        {
            TestButton.OnTestEnd -= OnSceneEnd;
        }

        public virtual void OnSceneEnd()
        {
            _sr.enabled = true;
            if (_instance.gameObject != null)
                Destroy(_instance.gameObject);
            _instance = null;
        }

        protected void SpawnObject()
        {
            _sr.enabled = false;
            T go = Instantiate(_prefab, transform.position, _prefab.transform.rotation);
            _instance = go;
        }
    }
}
