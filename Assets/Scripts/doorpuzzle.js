// coded by Hamzah Shabbir
// Script allows the implementation of the door puzzle which once triggered a timer begins and the door opens 
// The player must pass the door before the timer or it closes 
	#pragma strict
	 
	// Variables Used
	private var restSeconds : int;
	private var roundedRestSeconds : int;
	private var displaySeconds : int;
	private var displayMinutes : int;
	private var isTriggered : boolean = false;
	public var countDownSeconds : int = 180;
	private var triggerTimer : float = 0.0;
	private var textTriggerTimer : String = "";
	public var triggerTimerMax : float = 10.0;
	
	 // Update
	function Update() 
	{
	    // trigger timer 
	    if ( isTriggered ) 
	    { 
	       triggerTimer -= Time.deltaTime;
	       textTriggerTimer = Mathf.CeilToInt( triggerTimer ).ToString();
	      // If the time runs out
	       if ( triggerTimer <= 0 )
	       {
	          textTriggerTimer = "";
	         // door closes
	         GameObject.Find("Door").animation.Play("Door Close");
	       }
	    }
	}
	 // Timer appears when function is triggered
	function OnGUI() 
	{
	    if ( isTriggered ) 
	    { 
	       GUI.Label( Rect( 790, 50, 200, 100 ), textTriggerTimer ); // trigger timer label
	    }
	}
	 // If player collides with object the timer starts
	function OnTriggerEnter( other : Collider )
	{
	    if ( other.gameObject.tag == "Player" ) 
	    {
	       isTriggered = true;
	       triggerTimer = triggerTimerMax; // set timer to max for countdown to zero
	       //Play's animation
	       GameObject.Find("Door").animation.Play("Door Open");    
	    }
}        