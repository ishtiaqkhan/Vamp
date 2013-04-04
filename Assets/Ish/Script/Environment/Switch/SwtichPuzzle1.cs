using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class SwtichPuzzle1 : MonoBehaviour {
	
	public int PlatformType;
	private platformScript PlatformScript;
	public Transform PlatformUsed;	
	ArrayList Platforms = new ArrayList();
	private bool SpawnPlat;
	public List<float> EndPostion = new List<float>(); 
	public  List<Vector3> Position = new List<Vector3>();
	//public Vector3 [] Position ; 
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
		
		Position.Add(new Vector3(190.4832f,-143.8165f,0f));
		EndPostion.Add(-133f);
		
		
		
	}	
	
	void SpawnPlatforms()
	{			
			Transform clone;
			clone = Instantiate(PlatformUsed,Position[0],Quaternion.identity) as Transform;
			Platforms.Add(clone);		
			
		
		
		
			
			Move = true;	
	
			
	}
		
	void OnTriggerStay(Collider other) {		
		
		if(other.tag == "Player" && Pressed == false)
		{
			if(Input.GetButton("Interact"))
			{
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
			if(((Transform)Platforms[i]).transform.position.y<= EndPostion[0])
				
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
	