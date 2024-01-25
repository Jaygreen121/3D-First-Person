using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{

    private CharacterController characterController;

    private float originalHeight;
    private float crouchingHeight;
    private bool allowCrouch = true;

    // Static variables accessed elsewhere
    public static bool isCrouching = false;
    public static bool isSliding = false;
    
    private float slideSpeed = 15.0f;

    private Animator animator;

    

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();    
        originalHeight = characterController.height;

        crouchingHeight = originalHeight / 2f;

        allowCrouch = PlayerJump.groundedPlayer;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (allowCrouch == true)
            {
                ToggleCrouch();
            }
  
        }

        animator.SetBool("IsSliding", isSliding);
    }

    public void ToggleCrouch()
    {
        if (isCrouching) // Come out of crouch
        {
            characterController.height = originalHeight;
            characterController.center = new Vector3(0, originalHeight / 2, 0);
            // TODO: reduce character movementSpeed + disable ability to sprint
        }
        else // Go into crouch
        {
            if (PlayerMovement.isRunning)
            {
                if (!isSliding)
                {
                    isCrouching = true;

                    characterController.height = crouchingHeight;
                    characterController.center = new Vector3(0, crouchingHeight / 2, 0);                    
                    
                    StartCoroutine(runningSlide(slideSpeed, 1.0f));
                }

            }
            else
            {
                characterController.height = crouchingHeight;
                characterController.center = new Vector3(0, crouchingHeight / 2, 0);
            }

        }

        isCrouching = !isCrouching;
    }

    IEnumerator runningSlide(float slideSpeed , float duration)
    {
        isSliding = true;

        yield return new WaitForSeconds(duration);

        isSliding = false;

        characterController.height = originalHeight;
        characterController.center = new Vector3(0, originalHeight / 2, 0);


    }
}
