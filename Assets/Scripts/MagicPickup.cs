using UnityEngine;
using System.Collections;

/// <summary>
/// Magic pickup.
/// This script is for picking up magic boosting stuff. 
/// By Hamzah 
/// </summary>


public class MagicPickup : MonoBehaviour {
	
	
	//variables 
	private GameObject tempGO;
	private Player playerScript;
	private int addToMagic = 40; //ammount the pickup will add to magic 
	private int currentMagic; // value of current magic 
	private int magicDifference; // difference between max magic and the ammount the pick up will add to magic 

	// Use this for initialization
	void Start () {
		
		
		
		tempGO = GameObject.FindGameObjectWithTag("Player");// temp game object 
		playerScript = (Player)tempGO.GetComponent("Player");// script to get reference to scripts on the player object 
		
		magicDifference = 100 - currentMagic; 
	
	}
	
	// Update is called once per frame
	void Update () {
		
		currentMagic = playerScript.ReturnCurrentMagic(); // applying current magic 
	
	}
	
	void OnTriggerEnter(Collider col) 
	{
		
		//if the collider tag is the same as the player tag. then adusjt the magic 
		if(col.tag == "Player") 
		{
			if(currentMagic <= 100)
			{
				
				if(currentMagic >= magicDifference)
				{
					currentMagic = 100;
					Destroy (gameObject);
				}
				else
				{
					playerScript.AdjustCurrentMagic(addToMagic); 
					Destroy (gameObject);	
					
				}
				
			}
			
			
		}
			
	}
}
