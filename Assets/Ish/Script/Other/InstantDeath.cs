using UnityEngine;
using System.Collections;

public class InstantDeath : MonoBehaviour {
	
	private Player player;//Used to link the player script on the player gameobject to this script
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

		if(other.tag == "Player")
		{
			shell = other.gameObject; //Linking the player or enemy gameobject to the shell to pass a reference to the script need 
		}
		
		if(other.tag == "Player") //if the tag is the player
		{
			player = (Player)shell.GetComponent("Player"); //Linking the player script on the player gameobject to this player script 
			player.KillPlayer(); //Kill the player
		}	
	}
}