using UnityEngine;
using System.Collections;

/// <summary>
/// Movement. 
/// Controls the movement of the player 
/// adapted from 
/// </summary>

public class Movement :MonoBehaviour {
	
	
	ControllerMovement movement = new ControllerMovement();
	ControllerJump jump = new ControllerJump(); 
	ControllerDash dash = new ControllerDash(); 
	CharacterController controller; 
	
	//Respond to input
	bool canControl = true; 
	
	// where the player spawns
	public Transform spawnPoint; 
	
	//int extraHeight = 1; 
	bool isJumping = false; 
	//bool wasJumping = false; 
	//bool doubleJump = true; 
	//bool grounded = false; 
	
	
	
	private Transform activePlat;
	private Vector3 activeLocPlatPoint; 
	private Vector3 activeGlobalPlatPoint; 
	private Vector3 lastPlatformVel;
	private Vector3 moveDirection = Vector3.zero;
    private float lastYPos;
    public bool dashing = false;
	
	//private ParticleSystem parSystem;
	public float normalGravity = 60; 
	private float dashGravity = 30;
	private GameObject tempGO;
	private Player playerScript;
	private int dashCost = 10;
	private int currentMagic; 

    private float count = 2f;


    //for update effects 
	private bool EmittersOn = false; 
	
	
	void Awake() {
		movement.direction = transform.TransformDirection(Vector3.forward);
		controller = GetComponent<CharacterController>(); 
		//Spawn(); 
		
		

	}

	// Use this for initialization
	void Start () {
		
		//parSystem = GetComponentInChildren<ParticleSystem>(); 
		
		//parSystem.renderer.enabled = false;
		//emitter.emit = false; 
		
		tempGO = GameObject.FindGameObjectWithTag("Player");
		playerScript = (Player)tempGO.GetComponent("Player");
	
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
		currentMagic = playerScript.ReturnCurrentMagic(); 
		
		// set the last time buttons were pressed
		if(Input.GetButtonDown("Jump") && canControl){
			jump.lastButtonTime = Time.time; 
		}
	    /*
		if(Input.GetButton("Dash") && canControl){
			dash.lastButtonTime = Time.time; 	
		}
         * 
         */

       
		
		UpdateSmoothMovementDirection(); 
		
		//Debug.Log("Gravity is " + movement.gravity); 
		//Debug.Log("Dashing is currently " + dash.dashing);
		//Debug.Log(movement.speed); 
		
		ApplyGravity(); 
		Dash (); 
		
		ApplyJump(); 
		
		//ApplyDash(); 
		
		if(activePlat != null)
		{
			Vector3 newGlobalPlatPoint = activePlat.TransformPoint (activeLocPlatPoint);
			Vector3 moveDistance = (newGlobalPlatPoint - activeGlobalPlatPoint); 
			transform.position = transform.position + moveDistance;
			lastPlatformVel = (newGlobalPlatPoint - activeLocPlatPoint) / Time.deltaTime;
			
		}
		else 
			lastPlatformVel = Vector3.zero; 
		
		activePlat = null;
		
		Vector3 lastPosition = transform.position;
		
		Vector3 currentMovementOffset = movement.direction * movement.speed + new Vector3(0,
			movement.verticalSpeed, 0) + movement.airVelocity;
		
		currentMovementOffset *= Time.deltaTime;
		
		movement.collisionFlags = controller.Move(currentMovementOffset); 
		
		movement.velocity = (transform.position - lastPosition) / Time.deltaTime;
		
		if(activePlat != null)
		{
			activeGlobalPlatPoint = transform.position; 
			activeLocPlatPoint = activePlat.InverseTransformPoint(transform.position);
		}
		
		//rotates player graphics 
		if(movement.direction.sqrMagnitude > 0.01)
		{
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation
				(movement.direction), Time.deltaTime * movement.rotationSmoothing);
		}
		
		
		if(controller.isGrounded){
			movement.airVelocity = Vector3.zero;
			
			if(jump.jumping) {
				jump.jumping = false; 
				jump.doubleJumping = false; 
				jump.canDoubleJump = false; 
				SendMessage("DidLand", SendMessageOptions.DontRequireReceiver);
				
				Vector3 jumpMoveDirection = movement.direction * movement.speed + movement.airVelocity; 
				if(jumpMoveDirection.sqrMagnitude > 0.01)
					movement.direction = jumpMoveDirection.normalized; 
			}
			
			if(dash.dashing) {
				dash.dashing = false;
				
			}
			
			
		}
        

      
	}
	
	
	void FixedUpdate() {
		
		transform.position = new Vector3(transform.position.x, transform.position.y,
			0f);


	}
	
	void Spawn() {
		movement.verticalSpeed = 0.0f; 
		movement.speed = 0.0f;
		
		transform.position = spawnPoint.position; 
	}
	
	void Death()
	{
		Spawn (); 	
	}
	
	void UpdateSmoothMovementDirection()
	{
		float hori = Input.GetAxisRaw("Horizontal");
		
		if(!canControl)
			hori = 0.0f; 
		
		//movement is true if the absolute value of horizontal is greater thatn 0.1
		movement.isMoving = Mathf.Abs (hori) > 0.1;
		
		if(movement.isMoving && !dashing)
			movement.direction = new Vector3(hori,0,0);
		
		if(controller.isGrounded) {
			float curSmooth = movement.speedSmoothing * Time.deltaTime;	
			
			float targetSpeed = Mathf.Min (Mathf.Abs (hori), 1.0f); 
			
			if(Input.GetButton("Fire2")&& canControl)
				targetSpeed *= movement.runSpeed; 
			else 
				targetSpeed *= movement.walkSpeed; 
			
			movement.speed = Mathf.Lerp (movement.speed, targetSpeed, curSmooth); 
			
			movement.hangTime = 0.0f; 
			
			jump.jumping = false; 
		}
		
		else{
			// in air controls 
			movement.hangTime += Time.deltaTime; 
			if(movement.isMoving)
				movement.airVelocity += new Vector3(Mathf.Sign(hori), 0 , 0) * Time.deltaTime *
					movement.inAirContAcceleration; 
			
		}
	
		
	}
	
	void ApplyJump()
	{
		if(jump.lastTime + jump.repeatTime > Time.time)
			return; 
		
		if(controller.isGrounded) {
			if(jump.jumpEnabled && Time.time < jump.lastButtonTime + jump.timeout)
			{
				movement.verticalSpeed = CalculateJumpVerticalSpeed (jump.height);
				movement.airVelocity = lastPlatformVel; 
				SendMessage("DidJump", SendMessageOptions.DontRequireReceiver); 
			}
		}
	}
	
	/*
	void ApplyDash() 
	{

        float currentYPos = transform.position.y;
        
		//if(dash.lastTime + dash.repeatTime > Time.time)
			//return; 
     
        count -= Time.deltaTime;

        Debug.Log(count);
        if (count > 1)
        {
            if (controller.isGrounded)
            {

               // if (dash.dashEnabled && Time.time < dash.lastButtonTime + dash.timeout)
              //  {
                    //controller.Move(movement.direction * Time.deltaTime);
                    //movement.direction.x = dash.dashLength; 

                    //movement.speed = CalculateDashHorizontalSpeed(dash.dashLength);
                    //SendMessage("DidDash",SendMessageOptions.DontRequireReceiver);

                    transform.position = new Vector3(transform.position.x, lastYPos,
            transform.position.z);
                 //   transform.Translate(Vector3.forward * (dash.dashSpeed * Time.deltaTime));
                    SendMessage("DidDash", SendMessageOptions.DontRequireReceiver);
                    count = 2f;

            //    }

            }
            else if (!controller.isGrounded)
            {

                if (dash.dashEnabled && Time.time < dash.lastButtonTime + dash.timeout)
                {
                    //controller.Move(movement.direction * Time.deltaTime); 
                    //movement.speed = CalculateDashHorizontalSpeed(dash.inAirDashLength);
                    //SendMessage("DidDash",SendMessageOptions.DontRequireReceiver);

                    transform.position = new Vector3(transform.position.x, lastYPos,
            transform.position.z);
                  //  transform.Translate(Vector3.forward * (dash.dashSpeed * Time.deltaTime));
                    SendMessage("DidDash", SendMessageOptions.DontRequireReceiver);
                   // count = 2f;
                }
            }
        }

       // else
          //  count = 2f; 


        lastYPos = currentYPos;
        
	
	
	}
	*/

    void Dash()
    {
        float newDashSpeed;

        Vector3 forward = transform.TransformDirection(Vector3.forward);
       
		if(/*controller.isGrounded &&*/ canControl)
		{
			if(currentMagic <= 100 && currentMagic > 0 && currentMagic >= dashCost) 
			{

		        if (Input.GetButtonDown("Dash") && (dash.currentSpeed < dash.MaxSpeed))
		        {
		            dashing = true;
					
					playerScript.AdjustCurrentMagic(-dashCost);
				//	parSystem.renderer.enabled = true;
		             
		        }
		        else if (dash.currentSpeed == dash.MaxSpeed)
		        {
		            dashing = false;
		        }
				
			}
			
			else 
				dashing = false; 
		
	        if (dashing)
	        {
				
				 
	            newDashSpeed = dash.currentSpeed + dash.dashAccel;
	            dash.currentSpeed = newDashSpeed;
	               // transform.Translate(Vector3.forward * (dash.currentSpeed * Time.deltaTime));
	            controller.SimpleMove(forward * dash.currentSpeed); 
				//canControl = false;
				
				
	        }
	        else
			{
	            dash.currentSpeed = 0;
				canControl = true; 
			//	parSystem.renderer.enabled = false; 
			}
	
	        //Debug.Log("Dash current speed = " + dash.currentSpeed + " Dash Max speed = " + dash.MaxSpeed); 
			
	         
		}
		

        
    }


	
	void ApplyGravity() {
		
		bool jumpButton = Input.GetButton("Jump"); 
		bool dashButton = Input.GetButton ("Dash"); 
		
		if(dash.dashing) //&&((dash.lastTime + dash.repeatTime) > Time.time))
			movement.gravity = dashGravity;
		else
			movement.gravity = normalGravity; 
		
		
		
		if(!canControl)
		{
			jumpButton = false;
			dashButton = false;
			
		}

		
		
		if(jump.jumping && !jump.reachedApex && movement.verticalSpeed <= 0.0)
		{
			jump.reachedApex = true; 
			SendMessage("DidJumpReachApex", SendMessageOptions.DontRequireReceiver); 
			
		}
		
		if((jump.jumping && Input.GetButtonUp ("Jump") && !jump.doubleJumping)
			|| (!controller.isGrounded && !jump.jumping && !jump.doubleJumping && 
			movement.verticalSpeed < -12))
		{
			jump.canDoubleJump = true;  		
		}
		
		if(jump.canDoubleJump && Input.GetButtonDown("Jump") && !IsTouchingCeiling())
		{
			jump.doubleJumping = true; 
			movement.verticalSpeed = CalculateJumpVerticalSpeed(jump.doubleJumpHeight); 
			jump.canDoubleJump = false; 
		}
		
		bool extraPowerJump = jump.jumping && !jump.doubleJumping && movement.verticalSpeed > 0.0f &&
			jumpButton && transform.position.y < jump.lastStartHeight + jump.extraHeight &&
				!IsTouchingCeiling(); 
		
		if(extraPowerJump)
			return; 
		else if (controller.isGrounded)
		{
			movement.verticalSpeed = -movement.gravity * Time.deltaTime; 
			jump.canDoubleJump = false; 
		}
		else
			movement.verticalSpeed -= movement.gravity * Time.deltaTime; 
		
		movement.verticalSpeed = Mathf.Max(movement.verticalSpeed, -movement.maxFallSpeed);
	
		
	}


	
	float CalculateJumpVerticalSpeed(float targetJumpHeight)
	{
		return Mathf.Sqrt(2 * targetJumpHeight * movement.gravity); 
		
	}
	
	float CalculateDashHorizontalSpeed(float targetDashLength)
	{
		return Mathf.Sqrt(2 * targetDashLength); 	
	}
	
	void DidJump() 
	{
		jump.jumping = true; 
		jump.reachedApex = false; 
		jump.lastTime = Time.time;	
		jump.lastStartHeight = transform.position.y; 
		jump.lastButtonTime = -10;
	}
	
	bool canDoubleJump() 
	{
		return jump.canDoubleJump; 	
	}
	
	bool IsTouchingCeiling()
	{
		return (movement.collisionFlags & CollisionFlags.CollidedAbove) != 0; 	
	}
	

	
	void DidDash()
	{
		dash.dashing = true; 
		dash.reachedApex = false;
		dash.lastTime = Time.time;
		dash.lastButtonTime = -10; 
	}
		
	
		
		
}

class ControllerMovement {
	
	// speed of when walking
	public float walkSpeed = 7.0f; 
	// speed when holding down fire 1
	public float runSpeed = 10.0f; 
	
	public float inAirContAcceleration = 1.5f; 
	
	// player gravity
	public float gravity = 60.0f; 
	//max dash speed
	public float maxFallSpeed = 40.0f; 
	
	
	//how fast player changes speeds 
	public float speedSmoothing = 5.0f; 
	
	//speed of which the graphics turn around 
	public float rotationSmoothing = 10.0f; 
	
	// current direction
	[System.NonSerializedAttribute]
	public Vector3 direction = Vector3.zero; 
	
	[System.NonSerializedAttribute]
	public float verticalSpeed = 0.0f; 
	
	[System.NonSerializedAttribute]
	public float horizontalSpeed = 0.0f;
	//current speed, smoothed by speedSmoothing 
	[System.NonSerializedAttribute]
	public float speed = 0.0f; 
	
	// is the user pressing movement keys 
	[System.NonSerializedAttribute]
	public bool isMoving = false; 
	
	//last controller flags returned 
	[System.NonSerializedAttribute]
	public CollisionFlags collisionFlags ; 
	
	// characters velocity 
	[System.NonSerializedAttribute]
	public Vector3 velocity; 
	
	// velocity when not in air
	[System.NonSerializedAttribute]
	public Vector3 airVelocity = Vector3.zero ;
	
	// controll of how long the player has been in air 
	[System.NonSerializedAttribute]
	public float hangTime = 0.0f; 
	
	
	
	
	
}


class ControllerJump {
	
	// can the character jump 
	public bool jumpEnabled = true; 
	
	//jump height 
	public float height = 2.0f; 
	//extra heigh when holding down the jump button 
	public float extraHeight = 0.5f; 
	//height of doubl jump
	public float doubleJumpHeight = 3.1f; 
	
	//prevents jumping too quickly 
	[System.NonSerializedAttribute]
	public float repeatTime = 0.5f; 
	
	[System.NonSerializedAttribute]
	public float timeout = 0.15f; 
	
	//is the character jumping
	[System.NonSerializedAttribute]
	public bool jumping = false; 
	
	[System.NonSerializedAttribute]
	public bool reachedApex = false; 
	
	// can the character double jump(can only make one double jump pedr jump)
	[System.NonSerializedAttribute]
	public bool canDoubleJump = false; 
	
	//last time the jump button was pressed 
	[System.NonSerializedAttribute]
	public float lastButtonTime = -10.0f; 
	
	//last time a jump was performed 
	[System.NonSerializedAttribute]
	public float lastTime = -1.0f; 
	//height jumped from 
	[System.NonSerializedAttribute]
	public float lastStartHeight = 0.0f;
	
	//is the character double jumping
	[System.NonSerializedAttribute]
	public bool doubleJumping = false; 
	
}

class ControllerDash {
	
	public bool dashEnabled = true; 
	
	public float dashLength = 500.0f;

    public float MaxSpeed = 35.0f;

    public float currentSpeed = 0f;

    public float dashAccel = 1f;
	
	public float inAirDashLength = 100.0f; 
	
	[System.NonSerializedAttribute]
	public bool dashing = false; 
	
	[System.NonSerializedAttribute]
	public bool reachedApex = false; 
	[System.NonSerializedAttribute]
	public float repeatTime = 0.75f; 
	
	[System.NonSerializedAttribute]
	public float lastButtonTime = -10.0f; 
	
	[System.NonSerializedAttribute]
	public float lastTime = -1.0f; 
				
	[System.NonSerializedAttribute]
	public float timeout = 0.15f;
	
	[System.NonSerializedAttribute]
	public float dashEndTime = 1.25f; 
	
}





