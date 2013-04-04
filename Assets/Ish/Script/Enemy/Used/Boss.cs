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
		transform.position+= transform.forward * 10f * Time.deltaTime;
		transform.FindChild("BossEnemy").animation.Play("walk");		
	}
}
//private WallDissolving wd;
//	private GameObject shell;//The gameobject used to link the scripts of the enemy and player 
//	private SmoothFollow sf;
//	private  bool IncreaseOn; 
//	private BoxCollider box;
//	public GameObject[] Des;
//	public Transform deathbox;
//
//	// Use this for initialization
//	void Start () {
//		
//			shell = GameObject.FindGameObjectWithTag("Wall"); //Linking the player or enemy gameobject to the shell to pass a reference to the script need 		
//		    this.box = gameObject.GetComponent<BoxCollider>();
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		
//		if(IncreaseOn == true)
//		{
//			IncreaseCamera();
//		}
//	}
//	
//	void IncreaseCamera()
//	{
//		if(sf.distance <= 42)
//		{
//			sf.distance += 2 /* Time.deltaTime*/;	
//		}
//		else{
//			
//			Destroy(gameObject);					
//		}
//	}	
//	
//	void OnTriggerEnter(Collider other) {		
//		
//		if(other.tag == "Player") //if the tag is the player
//		{
//			wd = (WallDissolving)shell.GetComponent("WallDissolving");
//			wd.DestoryWall = true;
//			shell = GameObject.FindGameObjectWithTag("MainCamera");
//			sf = (SmoothFollow)shell.GetComponent("SmoothFollow");
//			IncreaseOn = true;				
//		
//			for (int i = 0;i < Des.Length ;i++)
//			{
//				Destroy(Des[i]);				
//			}	
//			
//			Transform clone;
//			clone = Instantiate(deathbox,new Vector3(-117.9965f,-208.029f,0f),Quaternion.identity) as  Transform;
//			clone.localScale = new Vector3 (2f,1f,1f);
//			Destroy(box);
//		}
//	}
//}	
////-121.3757
//
////-208.029
////0