using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private CharacterController characterController;
    public static float verticalVelocity;
    private float groundedTimer;
    private float jumpHeight = 2f;
    public static float gravityValue = 9.81f;
    private bool allowSecondJump = false;

    public static bool groundedPlayer = true;
    private float groundCheckTolerance = 0.2f; // For some reason while stationary or going down ramps the player isn't recognised as grounded

    private Vector3 jump;

    private Animator animator;

    void Start()
    {
        // always add a controller
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        groundedPlayer = characterController.isGrounded || Physics.Raycast(transform.position, Vector3.down, groundCheckTolerance);
        animator.SetBool("IsGrounded", groundedPlayer);


        if (groundedPlayer)
        {
            // cooldown interval to allow reliable jumping even whem coming down ramps
            groundedTimer = 0.2f;
        }
        if (groundedTimer > 0)
        {
            groundedTimer -= Time.deltaTime;
        }

        // slam into the ground
        if (groundedPlayer && verticalVelocity < 0)
        {
            // hit ground
            verticalVelocity = -0.5f;
        }


        // allow jump as long as the player is on the ground
        if (Input.GetButtonDown("Jump"))
        {
            // must have been grounded recently to allow jump
            if (groundedTimer > 0)
            {
                // no more until we recontact ground
                groundedTimer = 0;

                // Physics dynamics formula for calculating jump up velocity based on height and gravity
                verticalVelocity += Mathf.Sqrt(jumpHeight * 2 * gravityValue);

                animator.SetTrigger("JumpTrigger");

                allowSecondJump = true;
            }
            else
            if (allowSecondJump)
            {
                verticalVelocity += Mathf.Sqrt(jumpHeight * 2 * gravityValue);

                animator.SetTrigger("SecondJumpTrigger");

                allowSecondJump = false;
            }

        }

        // inject Y velocity before we use it
        jump.y = verticalVelocity;

        // call .Move() once only
        characterController.Move(jump * Time.deltaTime);

    }

    public void ResetGravityValue()
    {
        gravityValue = 9.81f;
        // apply gravity always, to let us track down ramps properly
        verticalVelocity -= gravityValue * Time.deltaTime;
    }
}

