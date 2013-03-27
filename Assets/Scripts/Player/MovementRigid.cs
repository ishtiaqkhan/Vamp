using UnityEngine;
using System.Collections;
/* 
 * Player Controls.
 * 
 * Actions:
 * Idle, Walk, Run, Jump, DoubleJump, Fall, Dash. 
 * 
 * 
 * Can interact with:
 * 
 * Enemies		-dash to attack enemies, jump on the head to attack
 * 
 * 
 * Controls:
 * 
 * A or left arrow to move left
 * B of right arrow to move right 
 * Space to jump 
 * F or ctrl to Dash 
 * 
 */

public class MovementRigid : MonoBehaviour {
	
	public float walkSpeed = 6f;
	public float runSpeed = 12f; 
	public float jumpSpeed = 8f;
	public float fallSpeed = 2f; 
	public float gravity = 30.0f;
	public bool canMove = true;
	public bool canJump = true; 
	public bool canDoubleJump = true;
	
	
	private Vector3 velocity = Vector3.zero;
	private float dashSpeed = 8.0f ;
	private int dashCount = 0; 
	private int maxDash = 5;
	private float speedSmoothing = 5.0f; 
	private float lastYPos; 
	private float DashTime = -5f;
	private float DashTimeOut = 0.15f; 
	
	
	
	//[System.NonSerializedAttribute]
	//public bool isInAir = false; 
	
	
	private Vector3 moveDirection = Vector3.zero ;
	

	private CharacterController controller;  
	

	// Use this for initialization
	void Start() {
	
		controller = GetComponent<CharacterController>();
		moveDirection = transform.TransformDirection(Vector3.forward); 
	
	}
	
	
	// Update is called once per frame
	void Update() {
		
		Controls(); 
		
		ApplyGravity(); 
		
		TestMove(); 
		
		Debug.Log ("Time.time = " + Time.time);
		
		
	
	}
	
	void FixedUpdate() {
		
		//lock axis 
		transform.position = new Vector3(transform.position.x, transform.position.y,
			0f); 
		
	}
	
	void Controls() 
	{
		
		float hori = Input.GetAxis("Horizontal"); 
		float vert = Input.GetAxis("Vertical"); 
		
		if(canMove)
		{
		
			if(controller.isGrounded) 
			{
				
				float curSmooth = speedSmoothing * Time.deltaTime; 

				velocity = new Vector3(hori, 0, 0);
				
				if(velocity.x == 0) 
				{
					//Play Idle animations 	
				}
				
				if(dashCount < maxDash) 
				{
				
					if(Input.GetButtonUp("Dash"))
					{
						//velocity *= dashSpeed; 
						//dashCount++; 
						
						transform.position = new Vector3( transform.position.x + dashSpeed * Time.deltaTime,
							transform.position.y, transform.position.z); 
							
					}
					
				}
				else
				{
						
				}
				
				//if the fire button is not down we walk and walkspeed
				//if it is down we walk at runspeed
				if(!Input.GetButton("Fire1")){
					
					//Play walking animation
					
					velocity *= walkSpeed;
					//Debug.Log("Currently walking at a speed of " + walkSpeed);
					
				}
				else {
					//Play running animation
					
					velocity *= runSpeed; 
					//Debug.Log("Currently running at a speed of " + runSpeed);
					
				}
				
				
				//if jump button is pushed and jump is enabled we can jump 
				if(Input.GetButton("Jump") && canJump) 
				{
					velocity.y = jumpSpeed; 
					
				}
			}	
			
			if(!controller.isGrounded)
			{
				
				velocity.x = hori; 
				velocity.x *= walkSpeed; 
			}
			
				
	
			
			
			controller.Move(velocity * Time.deltaTime);
				
		}
	}
	
	
	void ApplyGravity()
	{
		velocity.y -= gravity * Time.deltaTime;
		
	}
	
	void TestMove()
	{/*
		if(controller.isGrounded)
		{
			transform.position = new Vector3(transform.position.x + dashSpeed * Time.deltaTime,
				transform.position.y, transform.position.z); 
		}
		else if (!controller.isGrounded)
		{
			gravity = 0;
			transform.position = new Vector3(transform.position.x + 20 * Time.deltaTime,
				transform.position.y, transform.position.z); 				
		}
		*/
		
		float currentYPos = transform.position.y; 
		
		if(!controller.isGrounded)
		{
			
			if(Input.GetButton("Dash")){
				
				DashTime = Time.time; 
				
				if( Time.time < DashTime + DashTimeOut)
				{
				
				
					Debug.Log ("LastYPos = " + lastYPos + ": " + "Y position = " + 
						currentYPos);
				
					gravity *= -1;
			 
					transform.position = new Vector3(transform.position.x, lastYPos , 
						transform.position.z); 
					transform.Translate(Vector3.right * (30f * Time.deltaTime));
					
					//gravity = 30f;
					
					SendMessage("DidDash", SendMessageOptions.DontRequireReceiver); 
						
				}
				
			}
			else 
			{
				gravity = 30.0f; 	
				
			}
			
		}
		
		lastYPos = currentYPos; 

	}
	
	void DidDash()
	{
		DashTime = -5f; 	
	}
				
				
				
}
