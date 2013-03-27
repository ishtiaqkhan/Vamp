using UnityEngine;
using System.Collections;

//Class done by Ishtiaq
public class Hit : MonoBehaviour {
	
	private Player player;//Used to link the player script on the player gameobject to this script
	private Enemy_BaseClass Base;//Used to link the base enemy script on the enemy gameobject to this script
	private GameObject shell;//The gameobject used to link the scripts of the enemy and player 

	// Use this for initialization
	void Start () {	
		
		//StartCoroutine("Switch");
	}
	
	// Update is called once per frame
	void Update () {		

	}
	//if the player or enemy hits the collider and stays then the drain bool on the scripts is true which hurts them
	void OnTriggerStay(Collider other) {		

		if(other.tag == "Enemy" || other.tag == "Player")
		{
			shell = other.gameObject; //Linking the player or enemy gameobject to the shell to pass a reference to the script need 
		}
		
		if(other.tag == "Player") //if the tag is the player
		{
			player = (Player)shell.GetComponent("Player");//Linking the player script on the player gameobject to this player script 
			player.Drain = true;//Chaning the Drain boolean which hurts the player
		}
		
		else if(other.tag == "Enemy")//If the tag is the enemy
		{
			Base = (Enemy_BaseClass)shell.GetComponent("Enemy_BaseClass");//Linking the enemy base script on the enemy gameobject to this enemy base script 
			Base.Drain = true;	//Chaning the Drain boolean which hurts the enemy		
		}
	}
	
	//If the player or enemy exits the collider changing the Drain boolean to false so the player or enemy does not carry one dying
	void OnTriggerExit(Collider other) {		
	
		if(other.tag == "Enemy" || other.tag == "Player")
		{
			shell = other.gameObject;//Linking the player or enemy gameobject to the shell to pass a reference to the script need 
		}
		
		if(other.tag == "Player")//If the tag is the player
		{
			player = (Player)shell.GetComponent("Player");//Linking the player script on the player gameobject to this player script 
			player.Drain = false;	//Chaning the Drain boolean to false	
		}
		
		else if(other.tag == "Enemy")//If the tag is the enemy
		{
			Base = (Enemy_BaseClass)shell.GetComponent("Enemy_BaseClass");//Linking the enemy base script on the enemy gameobject to this enemy base script 
			Base.Drain = false;	//Chaning the Drain boolean to false		
		}
	}
}

