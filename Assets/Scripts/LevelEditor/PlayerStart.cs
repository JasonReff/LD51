namespace LevelEditor
{
    public class PlayerStart : EditorSpawnObject<PlayerManager>
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            TestButton.SpawnPlayer += SpawnObject;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            TestButton.SpawnPlayer -= SpawnObject;
        }
    }
}
