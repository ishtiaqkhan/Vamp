using UnityEngine;
using System.Collections;

/// <summary>
/// Spike. 
/// This is for the spikes, controls movement of the spikes. 
/// Jonathan and Radu. 
/// </summary>

public class Spike : MonoBehaviour {
	
	Transform myTransform;
	public float speed = 5.0f;	
	public float fallSpeed = 3.0f; 
	bool movDownB = false; 
	bool movUpB = true; 
	float delay = 2.0f; 
	float minHeight;
	float maxHeight;
	float length ;
	float lengthMod = 10; 

	// Use this for initialization
	void Start () {
		
		myTransform = transform; 
		minHeight = myTransform.position.y; 
		
		length = transform.localScale.y; 
		
		maxHeight = minHeight + length * lengthMod; 
		myTransform.position = new Vector3(myTransform.position.x, minHeight , myTransform.position.z); 
	
	}
	
	// Update is called once per frame
	void Update () {
		
		Moving(); 
		
		/*
		myTransform.position = new Vector3 (myTransform.position.x, myTransform.position.y + speed * Time.deltaTime ,
			myTransform.position.z); 
			*/ 
		
			
		//MoveUp();
		
		//Debug.Log (myTransform.position.y); 
		
	
		
	
		/*
		if(myTransform.position.y <= minHeight)
		{
			MoveUp(); 
		}
		
		*/
		
		
		
	
	}
	
	
	//initialise moving 
	void Moving()
	{
		if(myTransform.position.y > maxHeight)
			movUpB = false; 
		else if (myTransform.position.y < minHeight)
		{
			
			movUpB = true; 
		}
		
		if(movUpB)
			MoveUp(); 
		else 
		{
			//yield return WaitDelay(); 
			MoveDown(); 
			
		}
	}
	
	IEnumerator WaitDelay()
	{
		yield return new WaitForSeconds(delay); 
		
		
	}
	
	
	//method to move up 
	void MoveUp ()
	{
		
		
			
			myTransform.position = new Vector3 (myTransform.position.x, myTransform.position.y + speed * Time.deltaTime ,
				myTransform.position.z); 

		
	}
	//method to move down 
	void MoveDown() 
	{
		
	
			
			myTransform.position = new Vector3 (myTransform.position.x, myTransform.position.y - fallSpeed * Time.deltaTime ,
				myTransform.position.z); 
	}
	
}
