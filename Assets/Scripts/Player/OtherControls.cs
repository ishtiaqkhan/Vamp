using UnityEngine;
using System.Collections;

public class OtherControls : MonoBehaviour {

    public bool interactBtnPressed = false; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Interact"))
        {
            //Debug.Log("Interact button pressed");
            interactBtnPressed = true;
        }
        else
            interactBtnPressed = false; 
	
	}
}
