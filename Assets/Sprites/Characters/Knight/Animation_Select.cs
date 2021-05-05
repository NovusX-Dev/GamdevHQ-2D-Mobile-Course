using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Select : MonoBehaviour {


	Animator  anima;
	private Rigidbody2D rbJump;
	private bool isGround;
	public Transform groundCheck;
	private bool	verifyJumping;
	private string 	animJump;
	private float 	cont;
	private bool 	airAttack;

	void Start()
	{
		rbJump = gameObject.GetComponent<Rigidbody2D> ();
		anima = gameObject.GetComponent<Animator> ();
	}

	void FixedUpdate(){

		isGround = Physics2D.OverlapCircle (groundCheck.position,0.01f);
		JumpVerify ();
		verifyAirAttack ();

	}

	public void Animation(string anim)
	{
		verifyJumping = false;
		anima.Play (anim);
		airAttack = false;
	}

	public void Walk(string anim)
	{
		verifyJumping = false;
		anima.Play (anim);
	}


	public void AirAtaque(string anim)
	{
		verifyJumping = false;
		rbJump.AddForce (new Vector2 (0, 4500));
		anima.Play (anim);
		StartCoroutine ("AttackPulo");
	}

	IEnumerator AttackPulo()
	{
		yield return new WaitForSeconds (0.3f);
		anima.Play ("Air_Attack");
		airAttack = true;
	}

	public void Jump(string Anim)
	{
		airAttack = false;
		if (isGround) 
		{
			anima.Play (Anim);
			verifyJumping = true;
			rbJump.AddForce (new Vector2 (0, 4500));
			animJump = Anim;

		}

	}

	public void JumpVerify()
	{
		if (verifyJumping) 
		{

			if (rbJump.velocity.y < 0) 
			{
				if (animJump == "Jump") 
				{
					anima.Play ("falling");
				} 
				else 
				{
					anima.Play ("falling_With_Sword");
				}

				if (isGround) 
				{
				
					if (animJump == "Jump") 
					{
						anima.Play ("Idle");
					} 
					else 
					{
						anima.Play ("Idle_With_Sword");
					}

					verifyJumping = false;
					airAttack = false;

				}
			}
		}
	}


	public void verifyAirAttack()
	{
		if (airAttack) 
		{
			if (isGround) 
			{
				anima.Play ("Idle_With_Sword");
				airAttack = false;
			}

		}
		
	}



}
