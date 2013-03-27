using UnityEngine;
using System.Collections;

public class AnimationTest : MonoBehaviour {

    AnimationState walk;

	// Use this for initialization
	void Start () {

        walk = animation["walking"]; 
        foreach (AnimationState state in animation)
        {
            state.speed = 4f;
        }
     

       

	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Vertical") > 0.2)
        {
            animation.CrossFade("walking");
        }
        //else
      //  animation.CrossFade("idlestand");

        if (Input.GetKey(KeyCode.D))
        {
            animation.CrossFade("walking");
            walk.wrapMode = WrapMode.Loop; 
        }
       else 
       {
           walk.wrapMode = WrapMode.Clamp;  
       }

	
	}
}
