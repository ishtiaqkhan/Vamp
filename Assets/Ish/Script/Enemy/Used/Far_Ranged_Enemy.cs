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
public class Far_Ranged_Enemy : Enemy_BaseClass
{

    public static List<Far_Ranged_Enemy> allFarRangedEnemy = new List<Far_Ranged_Enemy>();//Creating a static array to store all of the far ranged enemies in the world
    public Transform projectile;//The gameobject that is created as the enemys' projectile
    private Transform player2;    //The variable that stores the player game object

	//Called once only. This is done when the gameobject is spawned in the game world
    void Awake()
    {       
        player2 = GameObject.FindGameObjectWithTag("Player").transform;//Linking the player gameobject to the player2 gameobject of this script
        SetUpEnemy(player2, transform.rotation, transform.position, 10, 10, 10, 10, 10, 10, 10, 10, 16, 10, 'R', true); //The first function needed to set up the enemy
        StartCoroutine("FireProjectile");//Starting the rountine which fires the projectiles at the player
        allFarRangedEnemy.Add(this);//Adding this gameobject script to the static array
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
		//If the above if statement is false then do this which is playing the idle animtaiton
        else
        {
            transform.FindChild("golem").animation.CrossFade("idle");
        }

		//If the postion is greater than the number in the if statement then the enemy can not move
        if (Position.x >= -35.18421)
        {
            Move = false;
        }
		//If the player position is less than the number in the if statement then the enemy can move
        if (player2.position.x <= -35.18421)
        {
            Move = true;
        }

		//If the enemy health is 0 then play the death animtation
        if (Health <= 0)
        {
            transform.FindChild("golem").animation.Play("death");     
        }
		
		//If drain is true and the cooldown is true then start the rountie to hurt the enemy
		if(Drain == true && BurnCooldown == true)
		{		
			StartCoroutine("Burn",1.0f);
		}
		
        CallUpdateEnemy(); //The second function needed to set up the enemy
		/*
         * To changed the position of the enemy change the variable Position. An example is the two code snippets below, however by changing the position you will override the position and rotation
         * change made by the AI system so be careful.
         * 
         * Position = new Vector3 (Position.x + 10 * Time.deltaTime,Position.y+ 10 * Time.deltaTime,Position.z + 10 * Time.deltaTime);
         * Position = new Vector3(100, 100, 100);
         * 
         */
		
		//If Drain is true then set it to false so the enemy does not die too fast		
		if(Drain == true)
		{
			Drain = false;
			
		}
    }

	
	//The enumerator used to fire the projectile at the player
    IEnumerator FireProjectile()
    {
	//Does this foereve because of the while(true)
        while (true)
        {

		//If the range is less then the attack range then do the second if statement
            if (Range.magnitude < AttackRange)
            {
				//if the difference between the player's postion and this postion is then the attack range the fire the projectile
                if (Vector3.Distance(Player.position, Position) < AttackRange)
                {           			
                    transform.FindChild("golem").animation.Play("punch");		//Play the punch animation of the enemy
                    Transform newBall = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity) as Transform;//Create the projectile as a transform
					newBall.LookAt(player2);//make the proejctile look at the player
					newBall.rigidbody.AddForce(newBall.transform.forward * 1000);   //move the projectile forward by a force so it slows down                 
                }
            }
            yield return new WaitForSeconds(0.5f + Random.value * 2);//wait for a certain ammout of time which is random value before firing again
        }
    }

	//This is called once the gameobject is destroyed
    void OnDestroy()
    {  
        allFarRangedEnemy.Remove(this);//remove is gameobject script from the static array
    }
	
	//The enumerator used to burn the enemy
	IEnumerator Burn(float TimerToStartAgain)		
	{	
		BurnCooldown = false;//Chaning the BurnCooldown to false or the enemy will die too fast
		LoseHealth();//tttaking health away from the enemy
		yield return new WaitForSeconds(TimerToStartAgain); //Waiting certain ammout of time which is got from TimerToStartAgain
		BurnCooldown = true;//Chaning the BurnCooldown to true to allow the enemy to get hurt again
	}
	
	//Taking health off the enemy
    void LoseHealth()
    {
        Health -= 10;
        transform.FindChild("golem").animation.Play("hit");
    }
}
