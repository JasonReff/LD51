using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = .05f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.localPosition = transform.localPosition + new Vector3(0, playerSpeed, 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.localPosition = transform.localPosition + new Vector3(0, -playerSpeed, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.localPosition = transform.localPosition + new Vector3(-playerSpeed, 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition = transform.localPosition + new Vector3(playerSpeed, 0, 0);
        }
    }
}
