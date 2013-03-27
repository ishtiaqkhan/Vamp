using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

//Class done by Ishtiaq
public class DestoryGate : MonoBehaviour
{
//A static list to store all of the waypoints in the level. this can be accesed by every class and object derived from this class. This is becuase of static which allows other classes to access the list
	public static List<DestoryGate> Waypoints = new List<DestoryGate>();
	//The spawn so it can be moved if the player hits this checkpoint
	private Transform spawn;
	
	//When the gameobject is first loaded up into the game world it calls this
	void Awake()
    {
	//Adds this gameobject script to the static WayPoints list
		Waypoints.Add(this);
		//Finds the spawn point gameobject to store its location in spawn
		spawn = GameObject.FindGameObjectWithTag("Spawn").transform;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		
    }
	
	//If the player hits this gameobject move the spawn point to this check points position and destory the check point game object
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
			spawn.position = transform.position;
		    Destroy(this.gameObject);			
        }
    }
	
	//Once the gameobject is destroyed this method is called 
	 void OnDestroy()
    {        
	//removes this gameobject script from the list
        Waypoints.Remove(this);
    }
} 