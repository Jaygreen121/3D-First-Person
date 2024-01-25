using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 respawnPosition;
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();  
        respawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -60f) // Falls of the map
        {
            characterController.enabled = false; // To stop the character controller from interfering
            transform.position = respawnPosition;
            characterController.enabled = true;
        }
    }
}
