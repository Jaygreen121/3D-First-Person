using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallRun : MonoBehaviour
{
    public static bool isWallRunning = false;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D) && !PlayerJump.groundedPlayer)
        {
            if (CheckWall(transform.right))
            {
                isWallRunning = true;
                Debug.Log(isWallRunning);
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
                Debug.Log(isWallRunning);
            }
            else
            {
                isWallRunning = false;
            }
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
}
