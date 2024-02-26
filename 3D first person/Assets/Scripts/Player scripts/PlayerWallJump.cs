using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJump : MonoBehaviour
{
	private Animator animator;

	private bool isWallSliding = false;
	private bool alreadyJumped = false;

	public PlayerMovement playerMovement;
	public PlayerJump playerJump;

	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponent<Animator>();


	}

	// Update is called once per frame
	void Update()
	{
		if (isWallSliding)
		{
			if (Input.GetButtonDown("Jump") && !alreadyJumped)
			{
				playerJump.WallJumpAction();
				alreadyJumped = true;
			}

			if (PlayerJump.groundedPlayer && isWallSliding)
			{
				isWallSliding = false;
				playerJump.ResetGravityValue();
				animator.SetBool("IsWallSliding", false);
				alreadyJumped = false;
				Debug.Log("Grounded exit");
			}



		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (!Input.GetKey(KeyCode.LeftShift) && !PlayerJump.groundedPlayer) // They are not sprinting and they are not on the ground
		{
			isWallSliding = true;
			Debug.Log("Wall Sliding = " + isWallSliding);
			Debug.Log(collision.gameObject);
			animator.SetBool("IsWallSliding", true);
			playerJump.WallSlideAction();
		}

		Debug.Log(collision.gameObject);
	}

	void OnCollisionExit(Collision collision)
	{
		if (isWallSliding)
		{
			isWallSliding = false;
			playerJump.ResetGravityValue();
			animator.SetBool("IsWallSliding", false);
			alreadyJumped = false;
			Debug.Log("Collision exit");
		}

	}
}
