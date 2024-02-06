using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallRun : MonoBehaviour
{
    public static bool isWallRunning = false;
    private float wallRunSpeed = 5.0f;
    private CharacterController characterController;

    private Animator animator;

    public PlayerJump playerJump;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D) && !PlayerJump.groundedPlayer)
        {
            if (CheckWall(transform.right))
            {
                isWallRunning = true;
            }
            else
            {
                isWallRunning = false;
            }
        }

        animator.SetBool("IsWallRunning", isWallRunning);

        if (Input.GetKey(KeyCode.A) && !PlayerJump.groundedPlayer)
        {
            if (CheckWall(-transform.right))
            {
                isWallRunning = true;
            }
            else
            {
                isWallRunning = false;
            }
        }

        if (isWallRunning)
        {
            WallRunAction();
        }
        else
        {
            playerJump.ResetGravityValue();
        }

        // TODO set isWallRunning to false here aswell 
    }

    bool CheckWall(Vector3 direction)
    {
        RaycastHit hit;

        // Cast a ray in the specified direction
        if (Physics.Raycast(transform.position, direction, out hit, 1.0f))
        {
            // Check if there's a wall
            if (hit.collider != null)
            {
                return true; // Wall detected
            }
        }

        return false; // No wall detected
    }

    void WallRunAction()
    {
        PlayerJump.gravityValue = 0f;
        PlayerJump.verticalVelocity = 0f;
        
        // Calculate the forward direction along the wall
        Vector3 wallDirection = isWallRunning ? (transform.forward.normalized * wallRunSpeed) : Vector3.zero;

        // Apply movement along the wall
        characterController.Move(wallDirection * Time.deltaTime);
    }
}
