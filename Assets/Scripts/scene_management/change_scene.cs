using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class change_scene : MonoBehaviour
{
    [SerializeField] private int next_level_id;
    [SerializeField] private GameObject player;
    public void MoveToScene()
    {
        SceneManager.LoadScene(next_level_id);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (player.name == collision.gameObject.name)
        {
            MoveToScene();
        }
    }
}
