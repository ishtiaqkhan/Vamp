using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {
	
	private WallDissolving wd;
	private GameObject shell;//The gameobject used to link the scripts of the enemy and player 
	private Transform player;
	
	// Use this for initialization
	void Start () {
		
		shell = GameObject.FindGameObjectWithTag("Wall"); //Linking the player or enemy gameobject to the shell to pass a reference to the script need 	
		wd = (WallDissolving)shell.GetComponent("WallDissolving");
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(wd.DestoryWall == true)
		{
			Move();	
		}	
	}
	
	void Move()
	{
		transform.LookAt(player);
		transform.position += transform.forward*5f*Time.deltaTime;
		transform.FindChild("BossEnemy").animation.Play("walk");
	}
}


//-121.6606
//-185.355
//54.53922