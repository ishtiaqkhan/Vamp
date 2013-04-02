using UnityEngine;
using System.Collections;

public class SwtichPuzzle1 : MonoBehaviour {
	
	public int PlatformType;
	private platformScript PlatformScript;
	public Transform PlatformUsed;	
	ArrayList Platforms = new ArrayList();
	private bool SpawnPlat;
	private Vector3 test = new Vector3(190.4832f,-143.8165f,0f);
	private bool Pressed = false;//133  /124
	private bool Move = false;
	// - 9
	// Use this for initialization
	void Start () {	
		SpawnPlat = true;
		PlatformScript = (platformScript)PlatformUsed.GetComponent("platformScript");
		PlatformScript.AttackEffect = true;
		PlatformScript.type = 1;		
	}
	
	void SpawnPlatforms()
	{			
		//if(SpawnPlat == true)
		//{
			Transform clone;
			clone = Instantiate(PlatformUsed,test,Quaternion.identity) as Transform;
			Platforms.Add(clone);
			Move = true;	
		//	SpawnPlat = false;	
		//}		
	}
	
	void DestoryPlatforms()
	{	
		//if(SpawnPlat = false)
		//{
		//	if((Transform)Platforms[0].transform.position != test)
		//	(Transform)Platforms[0].transform.position = test;
		//}
//		if(SpawnPlat == false)
//		{
//			SpawnPlat = true;	
//		}	
		/*
		
		
		destory 
		
		*/
	}
	
	void OnTriggerStay(Collider other) {		
		
		if(other.tag == "Player" && Pressed == false)
		{
			if(Input.GetButton("Interact"))
			{
				DestoryPlatforms();
				SpawnPlatforms();
				Pressed = true;	
			}			
		}
	}
		
	void OnTriggerExit(Collider other) {	
		
		if(other.tag == "Player" && Pressed == true)
		{
			Pressed = false;
		}
	}
//		
//		if(Input.GetButton("Interact"))
//			{
//				print ("THIS");
//				if(SpawnPlat == false)
//				{
//					DestoryPlatforms();
//				}			
//				else				
//				{
//					SpawnPlatforms();
//				}				
//			}
//		}
	
	void MovePlatforms()
	{		 
		for(int i=0; i<Platforms.Count; i++)
		{        
			if(((Transform)Platforms[i]).transform.position.y<= -133.8165f)
			{ 
				((Transform)Platforms[i]).transform.position+= new Vector3(0,0.2f,0);
			}
		}
	}
	
	
	// Update is called once per frame
	void Update () {	
	
		if(Move == true)
		{
			MovePlatforms();	
		}
	}
}
	
	
	
	
	


//shell = other.gameObject; //Linking the player or enemy gameobject to the shell to pass a reference to the script need 
//		}
//		
//		if(other.tag == "Player") //if the tag is the player
//		{
//			player = (Player)shell.GetComponent("Player"); //Linking the player script on the player gameobject to this player script 
//			player.KillPlayer(); //Kill the player
//		}private Player player;//Used to link the player script on the player gameobject to this script
//	private GameObject shell;//The gameobject used to link the scripts of the enemy and player 	