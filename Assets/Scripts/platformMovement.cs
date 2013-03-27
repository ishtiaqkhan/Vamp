//written by Radu Ionita
//script used to move the player with the platforms(the ones going left and right)
using UnityEngine;
using System.Collections;

public class platformMovement : MonoBehaviour {
	private bool isTou; //boolean used to update the player position only if he is on the platform
	private GameObject plat; //platform that player is on
	private Vector3 platpos; //platform position
	
	
	
	// Use this for initialization
	void Start () {
		isTou = false;
	}
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Platform")
		{
			isTou = true;
			plat = col.gameObject;
			platpos = plat.gameObject.transform.position;
			
			
		}
	}
	
	void OnTriggerExit(Collider col)
	{
		if(col.gameObject.tag == "Platform")
		{
			isTou = false;
			//Debug.Log("colision ended");
		}
	}
	// Update is called once per frame
	void Update () {
			if(isTou)
			{
				
	     		gameObject.transform.position-=(platpos-plat.gameObject.transform.position);
				platpos=plat.gameObject.transform.position;
				//Debug.Log("Workszor");
			}
		
	}
}
