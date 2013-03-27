// Written by Radu Ionita

using UnityEngine;
using System.Collections;





public class platformScript : MonoBehaviour {
	public int type;
	//Types of platforms
	//type = 0 --> normal static platform
	//type = 1 --> falling platform that never gets back
	//type = 2 --> up and down
	//type = 3 --> left and right
	private bool playerOn;
	private Vector3 pos;
	private float timer; //timer used for type 1 platform
	private bool isDroping; // for type 3 platform false is right true is left
	public float maxLDistance;
	public float maxRDistance;
	public float maxUDistance;
	public float maxDDistance;
	
	// Use this for initialization
	void Start () {
		
		playerOn = false;
		switch(type)
		{
			case 1:
				timer = 10;
				isDroping = false;
				break;
			case 2:
				isDroping = true;
				break;
			case 3:
				isDroping = true;
				break;
			default:
			
				break;
		}
			pos = gameObject.transform.position;
		
	}
	
	void OnTriggerEnter(Collider col)
	{
		
		if(col.gameObject.name == "Player")
		{
			playerOn = true;
		}
		
	}
	
	void OnTriggerExit(Collider col)
	{
		
		if(col.gameObject.name == "Player")
		{
			playerOn = false;
			switch(type)
			{
				case 1:
				timer = 10;
				break;
				default:
				break;
			}
		}
		
		
	}
	//method for moving the platform downward
	void Down()
	{
		gameObject.transform.position -= new Vector3(0.0f,2.0f*Time.deltaTime,0.0f);
	}
	//method for moving the platform upward
	void Up()
	{
		gameObject.transform.position += new Vector3(0.0f,2.0f*Time.deltaTime,0.0f);
	}
	//method for making a platform drop 
	void Drop()
	{
		gameObject.transform.position -= new Vector3(0.0f,10.0f*Time.deltaTime,0.0f);
	}
	//method for moving a platform to the right
	void Right()
	{
		//gameObject.rigidbody.AddForce(Vector3.up*10);
		gameObject.transform.position += new Vector3(2.0f*Time.deltaTime,0.0f,0.0f);
	}
	//method for moving a platform to the left
	void Left()
	{
		//gameObject.rigidbody.AddForce(-Vector3.up*10);
		gameObject.transform.position -= new Vector3(2.0f*Time.deltaTime,0.0f,0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
		switch(type)
		{
			case 1:
				if(playerOn)
			    {
					timer-=10*Time.deltaTime;
					
					if(timer<=0)
					{
						isDroping=true;
						Drop();
					}
				}
				else
				{
					if(isDroping)
					{
						timer-=10*Time.deltaTime;
						
						Drop();
						if(timer<-15)
							Destroy(gameObject);
					}
				}
				break;
			case 2:
				if(isDroping)
				{
					Down ();
				}
				else
				{
					Up ();
				}
				
				if(gameObject.transform.position.y<=maxDDistance)
				{
					isDroping = false;
				}
				else if(gameObject.transform.position.y>=maxUDistance)
				{
					isDroping = true;
				}
				break;
			case 3:
				if(isDroping)
				{
					Left ();
				}
				else
				{
					Right ();
				}
				
				if(gameObject.transform.position.x<=maxLDistance)
				{
					isDroping = false;
				}
				else if(gameObject.transform.position.x>=maxRDistance)
				{
					isDroping = true;
				}
				break;
			default:
				break;
				
		}
		
		
		
	}
}
