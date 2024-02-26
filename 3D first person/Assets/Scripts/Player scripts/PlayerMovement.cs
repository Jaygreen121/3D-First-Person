using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static float movementSpeed;

    private float turnSmoothTime = 0.075f;
    private float turnSmoothVelocity;
    public Transform cam;
    public CinemachineFreeLook freeLookCamera; // For when you want to restrict the camera. i.e., keep it at 0.5 on the y-axis whenever your character is moving

    private float wallRunSpeed = 7.0f;
    private bool isWallRunning = false;

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


        if (!isWallRunning) // Or if space is pressed (the player is trying to jump off the wall)
        {
            // OLD MOVEMENT CODE
            // float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
            // float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;

            // Vector3 speed = new Vector3(sideSpeed, 0, forwardSpeed);
            // speed = transform.rotation * speed;
            // characterController.Move(speed * Time.deltaTime);

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f) // i.e., is receiving input...?
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                characterController.Move(moveDirection.normalized * movementSpeed * Time.deltaTime);

                // Locks the camera to a certain height on the Y axis when moving
                // freeLookCamera.m_YAxis.Value = 0.5f;
            }
        }
        else
        {
            isWallRunning = PlayerWallRun.isWallRunning; // Assign isWallRunning to the value of the original isWallRunning
        }


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
    public void WallRunAction()
    {
        isWallRunning = true;

        PlayerJump.gravityValue = 0f;

        PlayerJump.originalVelocity = PlayerJump.verticalVelocity;
        PlayerJump.verticalVelocity = 0f;

        // Calculate the forward direction along the wall
        Vector3 wallDirection = transform.forward.normalized * wallRunSpeed;

        // Apply movement along the wall
        characterController.Move(wallDirection * Time.deltaTime);
    }
}
