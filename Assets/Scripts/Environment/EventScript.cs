//Written by Jonathan Andrews & Radu Ionita
//script used to spawn collectables/platforms/costum events
using UnityEngine;
using System.Collections;


public class EventScript : MonoBehaviour {
//var eventText:textScript;
//public Pickup col;
public Transform pla;
ArrayList colArray = new ArrayList();
ArrayList pArray = new ArrayList();
private bool moveFin = false;
private GameObject tempGO;
private OtherControls controls;
private bool inRange = false;
private bool switchActivated = false;
private Vector3 plat1 = new Vector3(12, -10, 0);
private Vector3 plat2 = new Vector3(17, -10, 0);
private Vector3 plat3 = new Vector3(22, -10, 0);
private Vector3 plat4 = new Vector3(27, -10, 0);
private Vector3 plat5 = new Vector3(32, -10, 0);
private float platFormOfset = 15f;
private Shader shaderDiffuse ;
private Shader shaderIllumin;
private bool platformMoving = false; 
    //method used to spawn Platforms
	void spawnPlat()
	{
		Transform clone;
		clone = Instantiate(pla, plat1, Quaternion.identity) as Transform;
		pArray.Add(clone);
		((Transform)pArray[0]).name = "plat1";
		clone = Instantiate(pla, plat2, Quaternion.identity) as Transform;
		pArray.Add(clone);
		((Transform)pArray[1]).name = "plat2";
		clone = Instantiate(pla, plat3, Quaternion.identity) as Transform;
		pArray.Add(clone);
		((Transform)pArray[2]).name = "plat3";
		clone = Instantiate(pla, plat4, Quaternion.identity) as Transform;
		pArray.Add(clone);
		((Transform)pArray[3]).name = "plat4";
		clone = Instantiate(pla, plat5, Quaternion.identity) as Transform;
		pArray.Add(clone);
		((Transform)pArray[4]).name = "plat5";
	}
	
	void OnTriggerEnter(Collider col)
	{


        if (inRange && Input.GetButton("Interact"))
        {
            Debug.Log("Switch pressed");
        }
		
		if (col.gameObject.tag == "Player"){
            inRange = true; 
		    spawnPlat();
		//spawnCol ();

            Debug.Log("Player has entered");
            
       
       
		//gameObject.transform.position-=new Vector3(0,30,0);
		}

      
        else if (inRange)
        {
            Debug.Log("Switch not pressed"); 
        }

            

        
	}
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            inRange = false; 
        }
    }
	//method used to spawn collectables
    /*
	void spawnCol()
	{
		Pickup clone;
		clone = Instantiate(col, new Vector3(-32, 32, 3), Quaternion.identity) as Pickup;
		colArray.Add(clone);
		((Pickup)colArray[0]).name = "col1";
		clone = Instantiate(col, new Vector3(-32, 32, 5), Quaternion.identity) as Pickup;
		colArray.Add(clone);
		((Pickup)colArray[1]).name = "col2";
		clone = Instantiate(col, new Vector3(-30, 32, 3), Quaternion.identity) as Pickup;
		colArray.Add(clone);
		((Pickup)colArray[2]).name = "col3";
//		
	}*/
	//method used to move platforms
	void MovePlatforms()
	{
       // float maxHeight = ((Transform)pArray[i]).position.y + platFormOfset; 
		for(int i=0; i<pArray.Count; i++)
		{
            
             
			if(((Transform)pArray[i]).transform.position.y<= 4)
			{
                Debug.Log("Platform moving");
				((Transform)pArray[i]).transform.position+= new Vector3(0,0.217f,0);
			}
			else moveFin = true;
		}
	}

	// Use this for initialization
	void Start () {
        tempGO = GameObject.FindGameObjectWithTag("Player");
        shaderDiffuse = Shader.Find("Diffuse");
        shaderIllumin = Shader.Find("Self-Illumin/Bumped Diffuse"); 

  



        controls = (OtherControls)tempGO.GetComponent("OtherControls"); 

        
       
	
	}
	
	// Update is called once per frame
	void Update () {


        if (inRange)
            renderer.material.shader = shaderIllumin;
        else
            renderer.material.shader = shaderDiffuse;  

        Switch();

        if (switchActivated)
        {
            platformMoving = true; 
        }

        if (platformMoving)
            MovePlatforms(); 
        
        //Debug.Log (colArray.Count);
		//if(colArray.Count>0)
	//	{
		//	for(int i=0; i<colArray.Count; i++)
		//	{
				/*if(((Pickup)colArray[i])._triggered)
				{
					colArray.RemoveAt(i);
				}*/
		//	}
//}
	//	if(colArray.Count==0)
	//	MovePlatforms();
	//	if(moveFin)
	//	Destroy(gameObject);
	}

    void Switch()
    {

        if (inRange && Input.GetButtonDown("Interact")&&!switchActivated)
        {
            switchActivated = true;
            Debug.Log("Switch pressed");
       

        }

    }
}
