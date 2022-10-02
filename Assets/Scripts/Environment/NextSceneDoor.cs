using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneDoor : MonoBehaviour
{
<<<<<<< Updated upstream:Assets/Scripts/Environment/NextSceneDoor.cs
    [SerializeField] private string _sceneName;
    public void MoveToScene()
=======
    [SerializeField] private int next_level_id;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject end_screen;
    void Update()
    {
        // Moves the object forward two units every frame.
        end_screen.transform.Translate(0, 0, 2);
    }
    public IEnumerator MoveToScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(next_level_id);
    }
    public void MoveToScene2()
>>>>>>> Stashed changes:Assets/Scripts/scene_management/change_scene.cs
    {
        SceneLoader.Instance.LoadScene(_sceneName, true);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerManager player))
        {
            //TransitionScreen();
            StartCoroutine(MoveToScene());
        }
    }
    //void TransitionScreen()
    //{
    //}
}
