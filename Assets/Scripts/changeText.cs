//written by Radu Ionita


using UnityEngine;
using System.Collections;

public class changeText : MonoBehaviour {

	public string text1; //public string used to change the text; just drag and drop the change text prefab to the scene
						 //and input the text you want to output in the inspector
	private float timer = 0;
	private bool hasTouched = false;
	
	void OnTriggerEnter(Collider col)
	{
		//if the player collides with the changeText prefab, the collider is moved from the player's reach
		//current text is change to text1 value inputed and a timer, used to make the text dissapear, starts
		if(col.gameObject.name == "Player")
		{
			textScript.text = text1;
			gameObject.transform.position+=new Vector3 (0,0,30.0f);
			timer = 5;
			hasTouched = true;
		} 
	}
	// Update is called once per frame
	
	
	void Update () {
		
		if(timer>=0&&hasTouched)
		{
			timer-=Time.deltaTime;
		}
		if (timer<0)
		{
			
			textScript.text = "";
			Destroy (gameObject);
		}
	
	}
}
