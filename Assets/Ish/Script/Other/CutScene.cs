using UnityEngine;
using System.Collections;

public class CutScene : MonoBehaviour {
	
	private Movement move;//Used to link the player script on the player gameobject to this script
	private GameObject shell;//The gameobject used to link the scripts of the enemy and player 
	private bool StartScene;
	public GUITexture Image;
	public Texture2D[] Cutscene;
	private int move2;

	// Use this for initialization	
	void Start () {
		
		move2 = 0;	
		StartScene = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(StartScene == true)
		{
			Image.enabled = true;
			Image.texture = Cutscene[move2];
			
			if(Input.GetKeyDown(KeyCode.Space) && move2 <= 3)
			{
				move2++;
				Image.texture = Cutscene[move2];		
				
			}
			if(move2 == 4 && Input.GetKeyDown(KeyCode.Space))
				{
					StartScene = false;		
					move.canControl = true;
					Application.LoadLevel(4);
				}
		}	
		else
		{
			Image.enabled = false;	
		}
	}
	//if the player or enemy hits the collider and stays then the drain bool on the scripts is true which hurts them
	void OnTriggerEnter(Collider other) {		

		if(other.tag == "Player")
		{
			shell = other.gameObject; //Linking the player or enemy gameobject to the shell to pass a reference to the script need 
		}
		
		if(other.tag == "Player") //if the tag is the player
		{
			move = (Movement)shell.GetComponent("Movement");//Linking the player script on the player gameobject to this movement script 
			move.canControl = false;
				StartScene = true;
		}				
	}
}