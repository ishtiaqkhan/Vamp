using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Class done by Ishtiaq
//This is linked to the MonoBehaviour class because it uses some of the functions found in the MonoBehaviour class
public class Enemy_BaseClass : MonoBehaviour
{

    //The private variables which are inherited to the derived classes,however all theses private variables are made into properties to allow them to be edited by the derived classes
    #region Private variables
    private int health;
    private int strenght;
    private float attackRange;
    private float chaseRange;
    private float maxMoveSpeed;
    private float minMoveSpeed;
    private float speedDamage;
    private float speedRecover;
    private float currentSpeed;
    private float rotatingSpeed;
    private char Ai;
    private Vector3 range;
    private Vector3 position;
    private Transform player;
    private Quaternion rotation;
    private RaycastHit hit;
    private Vector3 tempPos;
    private bool grounded;
    private bool move;
    private int bodyLifeTime;
    private CapsuleCollider spherecollider;
    private BoxCollider boxcollider;
	private bool drain;
	private bool burnCooldown;
		
    #endregion

    //The private variables are wrapped into properties to allow the programmer to edit the private variables 
    #region Properties(Acessing private variables)
    //An example of a property
    public int Health
    {
        //Returns the private health variable
        get { return health; }
        //Then sets the Health value as the value for health
        set { health = value; }
    }
    public int Strenght
    {
        get { return strenght; }
        set { strenght = value; }
    }
    public float AttackRange
    {
        get { return attackRange; }
        set { attackRange = value; }
    }
    public float ChaseRange
    {
        get { return chaseRange; }
        set { chaseRange = value; }
    }  
    public float MaxMoveSpeed
    {
        get { return maxMoveSpeed; }
        set { maxMoveSpeed = value; }
    }
    public float MinMoveSpeed
    {
        get { return minMoveSpeed; }
        set { minMoveSpeed = value; }
    }
    public float SpeedDamage
    {
        get { return speedDamage; }
        set { speedDamage = value; }
    }
    public float SpeedRecover
    {
        get { return speedRecover; }
        set { speedRecover = value; }
    }
    public float CurrentSpeed
    {
        get { return currentSpeed; }
        set { currentSpeed = value; }
    }
    public float RotatingSpeed
    {
        get { return rotatingSpeed; }
        set { rotatingSpeed = value; }
    }
    public char AI
    {
        get { return Ai; }
        set { Ai = value; }
    }
    public Vector3 Range
    {
        get { return range; }
        set { range = value; }
    }
    public Vector3 Position
    {
        get { return position; }
        set { position = value; }
    }
    public Transform Player
    {
        get { return player; }
        set { player = value; }
    }
    public Quaternion Rotation
    {
        get { return rotation; }
        set { rotation = value; }
    }
    public bool Grounded
    {
        get { return grounded; }
        set { grounded = value; }
    }
    public bool Move
    {
        get { return move; }
        set { move = value; }
    }
    public int BodyLifeTime
    {
        get { return bodyLifeTime; }
        set { bodyLifeTime = value; }
    }	
	   public bool Drain
    {
        get { return drain; }
        set { drain = value; }
    } 
	   public bool BurnCooldown
    {
        get { return burnCooldown; }
        set { burnCooldown = value; }
    } 
    #endregion

    //If the programmer does not want to access the private variables through the properties the programmer can use the functions instead, which are below.
    #region Acessing priavte variables functions
    //The functions are divide up to make it eaiser on the programmer

    #region Health functions
    //An example of a function

    //The function returns nothing and just substracts the variable AmmoutTaken from Health
   public  void RemoveHealth(int AmmoutTaken)
    {
        Health -= AmmoutTaken;
    }

    //The function just adds the variable AmmoutAdd to Health
    public void AddHealth(int AmmoutAdd)
    {
        Health += AmmoutAdd;
    }

    //This returns the current Health
   public int ReturnHealth()
    {
        return Health;
    }
    #endregion

    #region Strenght functions
   public void SetStrenght(int NewStrenght)
    {
        Strenght = NewStrenght;
    }
    #endregion

    #region AttackRange functions
    void SetAttackRange(float NewAttackRange)
    {
        AttackRange = NewAttackRange;
    }

    float ReturnAttackRange()
    {
        return AttackRange;
    }
    #endregion

    #region ChaseRange functions
    void SetChaseRange(float NewChaseRange)
    {
        ChaseRange = NewChaseRange;
    }

    float ReturnChaseRange()
    {
        return ChaseRange;
    }
    #endregion    

    #region Speed functions
    void SetSpeed(float NewSpeed)
    {
        CurrentSpeed = NewSpeed;
    }

    float ReturnSpeed()
    {
        return CurrentSpeed;
    }
    #endregion

    #region RotatingSpeed functions
    void SetRotatingSpeed(float NewRotatingSpeed)
    {
        RotatingSpeed = NewRotatingSpeed;
    }

    float ReturnRotatingSpeed()
    {
        return RotatingSpeed;
    }
    #endregion

    #region AI char functions (acessing the variable AI)
    void SetAI(char NewAI)
    {
        AI = NewAI;
    }

    char ReturnAI()
    {
        return AI;
    }
    #endregion

    #endregion
    
   //This is the method that sets up the enemy, which is called in the derived classes
    #region The method that sets up the enemy

    //This takes some arguments  to passes that value into the properties
    public void SetUpEnemy(Transform p, Quaternion rot, Vector3 pos, int HP, int STR, float maxspd, float minspd, float spddamage, float spdrec, float curspd, float rotatingspd, float chaselenght, float attackrange, char AiStyle, bool ground)
    {
        //The keyword this just means in this classes. So "this.Player" means the Player varible in this classes. This is then paassed the value "p". 
        this.Player = p;
        this.Position = pos;
        this.Rotation = rot;
        this.Health = HP;
        this.Strenght = STR;     
        this.MaxMoveSpeed = maxspd;
        this.MinMoveSpeed = minspd;
        this.SpeedDamage = spddamage;
        this.SpeedRecover = spdrec;
        this.CurrentSpeed = curspd;
        this.RotatingSpeed = rotatingspd;
        this.ChaseRange = chaselenght;
        this.AttackRange = attackrange;
        this.AI = AiStyle;
        this.Grounded = ground;
        this.BodyLifeTime = 5;
        this.spherecollider = gameObject.GetComponent<CapsuleCollider>();
        this.boxcollider = gameObject.GetComponent<BoxCollider>();
		this.BurnCooldown = true;
    }
    #endregion  

    //This is the main AI system
    #region AI Movement (How the enemy moves)

    //This is a switch statement to decide on the AI method to call
    #region AI switch statement (decides on what function to call)

    //This is private so no other derived class can mess with the function
    private void AIType()
    {

        /*
         * Only one AI method is done so far the others will be added but may change depending on the time and team decicsion
         * 
         * "W" or "w" = Waypoints
         * "P" or "p" = Predictive Path
         * "R" or "r" = Range Limit
         * 
         * Waypoint: The enemy follows a set of waypoints as it's path and if it sees the player it chases the player, however if the player is out of sight it carries on with the waypoint path
         * Predictive Path: The enemy knows where the player is heading and intercepts the player
         * Range Limit: If the player is in certain range the enemy will chase the player
         *           
         * Char W and P are not done, however waypoints is almost done and Predictive Path needs a bit of research in trigonometry
         * 
         */
        switch (AI)
        {
            //case 'W':
            //case 'w':
            //    {
            //        Waypoints();
            //        break;
            //    }
            //case 'P':
            //case 'p':
            //    {
            //        PredictivePath();
            //    }
            case 'R':
            case 'r':
                {
                    RangeLimit();
                    break;
                }
            //If a wrong letter or if a AI char is not assigned Range Limit is used instead
            default:
                {
                    RangeLimit();
                    break;
                }
        }
    }
    #endregion

    //The methods of the AI such as Waypoints are stored here

    //The Range Limit AI method
    #region RangeLimit
    //An example of a completed AI techinque
    private void RangeLimit()
    {
        if (Move == true)
        {
            //If the player is within range chase him/her
            if (Range.magnitude < ChaseRange)
            {
                // If the distance between the player and enemy is greater than the attack range then move foward 
                if (Vector3.Distance(Player.position, Position) > AttackRange)
                {
                    //Quaternion: Its like a vector but to do with rotation instead
                    //Quaternion.Slerp: just turns the rotation between the start point and the angle given with the speed
                    //Quaternion.LookRotation: what direction to look at
                    Rotation = Quaternion.Slerp(Rotation, Quaternion.LookRotation(Player.position - Position), RotatingSpeed * Time.deltaTime);
                    Position += transform.forward * CurrentSpeed * Time.deltaTime;
                }
                //Place holder for the attack of the enemy once it is added just replace Attack(); with the actual attacking method
                else
                {
                    transform.LookAt(Player);
                }
            }
        }
    }
    #endregion

    #endregion

    //Since derivved classes cannont access the Update or UpdateEnemy function this was created to allow them to call it
    #region Call Private functions in derived classes

    //This calls the UpdateEnemy function
    #region Call the update enemy function
    public void CallUpdateEnemy()
    {
        UpdateEnemy();
    }
    #endregion

    //This calls the Update function
    #region Call the update function
    public void CallUpdate()
    {
        Update();
    }
    #endregion

    #endregion

    //The Update function it is not used right now but later it maybe used
    void Update()
    {
    }

    //The UpdateEnemy function
    #region Update enemey function
    //This just updates the varaibles that need to be updated regularly such as the postion and rotation
    private void UpdateEnemy()
    {
        ////The derrived class postion and rotation being changed by the Position and Rotation variable
        transform.position = Position;
        transform.rotation = Rotation;
        ////The range being updated
        Range = Player.position - Position;
        ////Calling the AI system
       AIType();

	   //If the current speed is less than the max speed increases the enemies speed
        if (CurrentSpeed < MaxMoveSpeed)
        {
            CurrentSpeed += SpeedRecover * Time.deltaTime;
        }
		//If the speed is greater than the max speed then set the speed to be the max speed
        if (CurrentSpeed > MaxMoveSpeed)
        {
            CurrentSpeed = MaxMoveSpeed;
        }

		//If the enemy can not fly then if the enemy raycast hits the ground tag with Terrain make the Y position to be 1.3f higher then the ground. This means the enemies feet do not go below the ground
        if (Grounded == true)
        {
            tempPos = Position;//Stroing the enemy postion

            if (Physics.Raycast(transform.position, Vector3.down, out hit, 10.0f))
            {
                if (hit.collider.tag == "Terrain")
                {
                    tempPos.y = hit.point.y + 1.3f;
                    Position = tempPos;
                }
            }
            Debug.DrawRay(transform.position, Vector3.down * 100, Color.red);
        }
       
	   //If the enemy raycast which is cast in front of him hits another enemy gameobject then this enemy speed is 0.This makes sure two enemies do not bump into each other
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1.0f))
        {
            if (hit.collider.tag == "Enemy")
            {    
                CurrentSpeed = 0;
			
            }
        }	
		
		Debug.DrawRay(transform.position, transform.forward * 1, Color.magenta);
		
		//If the enemy health is less or equal to 0 then kill the gameobject,rigibody and colliders
        if (Health <= 0)
        {    
            Destroy(this.gameObject);
            Destroy(boxcollider);
            Destroy(spherecollider);
            Destroy(rigidbody);
        }		
    }    
    #endregion

	//If the enemy collides and stay in the collision area of the player or enemy then slow the enemy down a bit. This is done just in case the enemy whacs another enemy from the side and the front raycast does not pick it up
    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player" && collision.gameObject != gameObject)//If the gameobject tag is the player or enemy but not this gameobject enemy tag
        {
		//If the speed the enemy is moving at is greater than the minunum speed of the enemy then slow this enemy gameobject down
            if (CurrentSpeed > MinMoveSpeed)
            {
                CurrentSpeed -= SpeedDamage;
            }
            Position += (Position - collision.gameObject.transform.position).normalized * 0.1f;//Adds the difference between the two gameobjects to this gameobjects position. Normalize is done so the direction is got and the value got is not too high. Since the value got is too smaller it is times by 0.1f 
			
        }
    }
}