using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static float movementSpeed;

    private CharacterController characterController;

    private PlayerCrouch playerCrouch;

    public static bool isRunning;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        playerCrouch = GetComponent<PlayerCrouch>();
        
        characterController = GetComponent<CharacterController>();

        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        movementSpeed = 5.0f;

        if (PlayerCrouch.isSliding)
        {
            movementSpeed *= 3;
        }
        else 
        if (Input.GetKey(KeyCode.LeftShift) && !PlayerCrouch.isSliding) // Start sprinting...
        {
            if (PlayerCrouch.isCrouching) // If the player is crouching...
            {
                // ... then uncrouch
                playerCrouch.ToggleCrouch();
            }

            // Double movement speed if running
            movementSpeed *= 2;

            // Start the running animation
            isRunning = true;
            //animator.SetBool("IsRunning", true);

        }
        else
        {
            // Stop the running animation
            isRunning = false;
            //animator.SetBool("IsRunning", false);
        }

        animator.SetBool("IsRunning", isRunning);

        if (PlayerCrouch.isCrouching == true)
        {
            // Half movement speed if crouching
            movementSpeed /= 2;
            animator.SetBool("IsCrouched", true);
        }
        else
        {
            animator.SetBool("IsCrouched", false);
        }

        float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
        float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;

        Vector3 speed = new Vector3(sideSpeed, 0, forwardSpeed);
        speed = transform.rotation * speed;
        characterController.Move(speed * Time.deltaTime);

        // Check for movement
        float moveInputHorizontal = Input.GetAxis("Horizontal");
        float moveInputVertical = Input.GetAxis("Vertical");
        float combinedMoveInput = Mathf.Abs(moveInputHorizontal) + Mathf.Abs(moveInputVertical);

        if (Mathf.Abs(combinedMoveInput) > 0.1f)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

    }
}
