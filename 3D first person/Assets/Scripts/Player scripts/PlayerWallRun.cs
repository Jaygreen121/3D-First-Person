using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallRun : MonoBehaviour
{
    public static bool isWallRunning = false;
    private bool afterWallRun = false;

    private Animator animator;

    public PlayerJump playerJump;
    public PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckWall(transform.right) || CheckWall(-transform.right))
        {
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && !PlayerJump.groundedPlayer) // replace the check for seeing if you're grounded with a check to see if you are a certain distance off the ground
            {
                isWallRunning = true;
                playerMovement.WallRunAction();
                afterWallRun = true;
            }
            else
            {
                isWallRunning = false;
            }
        }
        else
        {
            isWallRunning = false;
        }

        animator.SetBool("IsWallRunning", isWallRunning);

        if (afterWallRun && !isWallRunning)
        {
            playerJump.ResetGravityValue();
            afterWallRun = false;
        }

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
}
