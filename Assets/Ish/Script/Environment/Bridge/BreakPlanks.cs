using UnityEngine;
using System.Collections;

public class BreakPlanks : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerStay(Collider other) {		
		
		if(other.tag == "Player") //if the tag is the player
		{
			Destroy(this.gameObject); //Destory the object that the script is attached to 
		}			
	}
}
