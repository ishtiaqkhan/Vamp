using UnityEngine;
using System.Collections;

public class WallDissolving : MonoBehaviour {
	

	private float Slice = 0.0f;
	public bool DestoryWall = false;	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (DestoryWall == true)
        {
            if (Slice <= 1)
            {
                Slice += 0.8f * Time.deltaTime;
            }
            if (Slice >= 1)
            {
                Destroy(this.gameObject);
            }
        }

        renderer.material.SetFloat("_SliceAmount", Slice);
	}
}