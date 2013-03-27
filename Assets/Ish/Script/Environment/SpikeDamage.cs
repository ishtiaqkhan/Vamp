using UnityEngine;
using System.Collections;

//Class done by Ishtiaq
public class SpikeDamage : MonoBehaviour {
	
	private Player player;//Used to link the player script on the player gameobject to this script
	private GameObject shell;//The gameobject used to act as the player gameobject
	
	// Use this for initialization
	//This is done once only when the gameobject is created
	void Start () {
		
		shell = GameObject.FindGameObjectWithTag("Player");//Linking the shell gameobject to the player gameobject in the game
		player = (Player)shell.GetComponent("Player");	//Linking the player script on the player gameobject to this player script 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//If the tag is Player which belongs to the player then the Drain boolean is true to hurt the player
	void OnTriggerStay(Collider other) {		
		
		if(other.tag == "Player")
		{
			player.Drain = true;
		}
	}
	
	//If the tag is Player which belongs to the player then the Drain boolean is false to stop hurting the player
	void OnTriggerExit(Collider other) {		
	
		if(other.tag == "Player")
		{
			player.Drain = false;
		}
	}
}
