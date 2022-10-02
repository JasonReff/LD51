using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeTrigger : MonoBehaviour
{
    [SerializeField]
    AxeEnvironment axe;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Knight")
        {
            axe.EnterTrigger();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Knight")
        {
            axe.ExitTrigger();
        }
    }
}
