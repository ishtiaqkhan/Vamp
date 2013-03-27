using UnityEngine;
using System.Collections;

public class SwtichButton : MonoBehaviour {

    private bool inRange = false; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter()
    {
        inRange = true;
    }

    void OnTriggerExit()
    {
        inRange = false; 
    }
}
