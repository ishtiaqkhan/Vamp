using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

//Class done by Ishtiaq
/* 
 * The point of this design is to allow the programmer to easily create enemies for the game. To create an enemy the programmer has to do the follwing steps and thats it:   
 *  1. Link the script to the base enemy class (OOP (Object-oriented programming)) 
 *  2. Create a variable that links the player to the base enemy classplayer variable
 *  3. Call two methods (SetUpEnemy and CallUpdateEnemy) that are in the base enemy class
 *
 * SetUpEnemy: This sets up the enemy to be used in the game. This function has the follwing arguments:
 *  Transform p = Player
 *  Quaternion rot = Rotation
 *  Vector3 pos =  Position
 *  int HP = Health
 *  int STR = Strenght
 *  float jumpHeight = JumpingHeight
 *  float spd = Speed
 *  float rotatingspd = RotatingSpeed
 *  float chaselenght = ChaseRange
 *  float attackrange = AttackRange
 *  char AiStyle = AI
 * 
 *CallUpdateEnemy: Updates the position and rotation variables stored in the base enemy class. It has no arguments.  
 *
 * Further Details:
 * 
 * char AiStyle = AI : This is the type of AI style is used on the enemy and is combined with a switch statement
 *     W = Waypoints
 *     P = Predictive Path
 *     R = Range Limit
 *     
 */

//Create the link between the base enemy class and enemy class
public class Close_Ranged_Enemy : Enemy_BaseClass
{
    public static List<Close_Ranged_Enemy> allCloseRangedEnemy = new List<Close_Ranged_Enemy>();//A static list to store all of the close range enemy in the world    
    private Transform player2; //The variable that stores the player game object
    private bool attack = false;//The cooldown for the attack that the enemy does

	//Done only once. When the gameobject is created this is called
    void Awake()    {
     
        player2 = GameObject.FindGameObjectWithTag("Player").transform;   //The first function needed to set up the enemy
        SetUpEnemy(player2, transform.rotation, transform.position, 20, 10, 10, 2, 1, 1, 4, 10, 5, 2, 'R', true);//The first function needed to set up the enemy
        allCloseRangedEnemy.Add(this);//Adding this to the static array
        StartAttack();//Calling the attack method
		Move = true;//Allowing the enemy to move
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		//If the position stored in the Postion from the base class is less than the postion of this gameobject then play the run animtaiton
        if (Position.x < transform.position.x || Position.x > transform.position.x || Position.z < transform.position.z || Position.z > transform.position.z)
        {
            transform.FindChild("golem").animation.CrossFade("run");
        }
		//else if the player is range to be attacked then attack the player
        else if (Vector3.Distance(Player.position, Position) < AttackRange)
        {
            transform.FindChild("golem").animation.Play("punch");//Play the punch animtation

			//If the attack boolean is true and the punch animation is playing then fade between each punch animtation to removed the sudden animtation jump between each frame
            if (attack == true && transform.FindChild("golem").animation.IsPlaying("punch"))
            {              
                transform.FindChild("golem").animation.CrossFade("punch");//Play the punch animtaiton
                player2.SendMessage("AdjustCurrentHealth", -10, SendMessageOptions.DontRequireReceiver);//Hurt the player by 10 health
                attack = false;//Attack is false so the player does not lose all of they health to fast
            }
        }
		//If none of the conditions are met then stand still
        else
        {
            transform.FindChild("golem").animation.CrossFade("idle");
        }
		//If this enemy has no health then play the death animtation
        if (Health <= 0)
        {
            transform.FindChild("golem").animation.Play("death");     
        }

        CallUpdateEnemy();//The second function needed to set up the enemy
		
		//If the Drain boolean and cooldown true then hurt the enemy
		if(Drain == true && BurnCooldown == true)
		{		
			StartCoroutine("Burn",1.0f);//Starts the routine to hurt the enemy
		}
			if(Drain == true)//If Drain is ture change it to false
		{
			Drain = false;
			
		}
        /*
         * To changed the position of the enemy change the variable Position. An example is the two code snippets below, however by changing the position you will override the position and rotation
         * change made by the AI system so be careful.
         * 
         * Position = new Vector3 (Position.x + 10 * Time.deltaTime,Position.y+ 10 * Time.deltaTime,Position.z + 10 * Time.deltaTime);
         * Position = new Vector3(100, 100, 100);
         * 
         */
    }
	
	//the enumerator used to burn the enemy
	IEnumerator Burn(float TimerToStartAgain)
	{	
		BurnCooldown = false;//changing BurnCooldown to false so the enemy does not die too fast
		LoseHealth();//Lose health on the enemy
		yield return new WaitForSeconds(TimerToStartAgain);//Wait for the time set in the parament TimerToStartAgain
		BurnCooldown = true;//Changing the BurnCooldown boolean to true to allow the enemy to get hurt again
	}
	
	
	//The function used to attack the player
    private void StartAttack()
    {
	//This reapeats this function every 0.5f seconds
        InvokeRepeating("Fire",0, 0.5f);
    }

	//This changes the attack to true to allow the enemy to punch the player
    void Fire()
    {
        attack = true;
    }

	//This is called ocne the enemy gameobject is destoryed 
    void OnDestroy()
    {        
        allCloseRangedEnemy.Remove(this);//remove this gameobject script from the static array
    }

	//Hurt the enemy by 10 health and play the hurt animation
    void LoseHealth()
    {
        Health -= 10;
        transform.FindChild("golem").animation.Play("hit");
    }  
}
