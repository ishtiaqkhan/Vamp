//written by Radu Ionita
//script used for the GUI tutorial text
using UnityEngine;
using System.Collections;

public class textScript : MonoBehaviour {
	
	public static string text; //this variable in the script will be accessable from any other script: textScript.text = "...";
	private float toggleButtonLength; 
	private float tutorialButtonX;
	private bool toggleTutorial = false; 

	// Use this for initialization
	void Start () {
	text = "Use A and D to move. Space to Jump.";
	guiText.material.color = Color.green;
		
		tutorialButtonX = toggleButtonLength + 750;
	}
	
	// Update is called once per frame
	void Update () {
		guiText.text = text;
		
		
		if(toggleTutorial)
			guiText.enabled = false; 
	}
	
	
	void OnGUI() 
	{
	
		//if(GUI.Button(new Rect(tutorialButtonX, 10, toggleButtonLength, 30), "Tutorial"))
		//		toggleTutorial = true; 
	}
	
	
	
}
