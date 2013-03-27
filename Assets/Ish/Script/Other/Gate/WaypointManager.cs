using UnityEngine;
using System.Collections;

//Class done by Ishtiaq
public class WaypointManager : MonoBehaviour {
	
	private GameObject shell; //The gameobject used to access the player script
	private Player player;//Declaring a player sciprt to be linked to the player gameobject in the game scene
	private Transform spawn;//the spawn point of where the player spawns if he is dead
	public GUIText lives;//The text used to show how much lives the player has
	public int AmmoutOfLives;//The int storing the number of lives the player has
	
	// Use this for initialization
	//This is called when the gameobject is first created
	void Start () {		
	
		AmmoutOfLives = 0  ;	//The player has 3 lives
		lives.text = "Lives: "+AmmoutOfLives; //setting the text to display the players lives
		spawn = GameObject.FindGameObjectWithTag("Spawn").transform; //Storing the spawn point transform
		shell = GameObject.FindGameObjectWithTag("Player");//Storing the player gameobject which is in the game world
		player = (Player)shell.GetComponent("Player");// Linking the player scirpt to the one on the player gameobject in the game world
	}
	
	// Update is called once per frame
	void Update () {
	
	//If the player health is udner 0 then take a life off the player but if the player has no more lives then take him to the game over screen
		if (player.currentHealth <=0)
		{
             
			if(AmmoutOfLives > 0)
			{
				player.transform.position = spawn.position;
				AmmoutOfLives--;
				lives.text = "Lives: " + AmmoutOfLives;
				player.AdjustCurrentHealth(100);
			}
			else
			{
				//Go to Death Screen
				Application.LoadLevel(3);
                
			}
		}
	}	
}
