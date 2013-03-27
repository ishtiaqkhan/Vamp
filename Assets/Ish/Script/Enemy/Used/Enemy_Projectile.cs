using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Class done by Ishtiaq
public class Enemy_Projectile : MonoBehaviour
{

    public static List<Enemy_Projectile> allProjectiles = new List<Enemy_Projectile>();//A static list that can be accssed by all other scripts
    public float lifeTime = 4f;//How long the projectile lives for
    public Transform DeathEffect;//The death effect of the projectile

	//This is done only once the gameobject is spawned in the game world
    void Awake()
    {
        allProjectiles.Add(this);//Adds this gameobject script to the static array 
        StartCoroutine(LifeTimeDestroy());//Starts the routine which will kill the projectile once the lifetimer expires
    }

	//Once the gameobject is destroyed this is called
    void OnDestroy()
    {        
        allProjectiles.Remove(this);//removes the gameobject script from the static list
    }
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
	}

	//The enumerator used to destroy the projectile
    IEnumerator LifeTimeDestroy()
    {
	
        yield return new WaitForSeconds(lifeTime);//Wait for how long the enemy life time is
        Destroy(gameObject);//Destorys the projectile gameobject
        Instantiate(DeathEffect, transform.position, Quaternion.identity);//Spawns the death effect where the projectile was
    }
    
	//If this collides with the player hurt him but if it collides with anything else kill the projectile
    void OnCollisionEnter(Collision collision)
    {
		//If the gameobject tag is anything but the player,enemy or untagged then destory the projectile and spawn the death effect
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Terrain" && collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Untagged")
        {           
            Destroy(gameObject);//Destorys the projecitile
            Instantiate(DeathEffect, transform.position, Quaternion.identity);//Spawns the death effect
        } 

		//If the projectile collides with the gameobject with the tag Player then hurt that gameobject
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.SendMessage("AdjustCurrentHealth",-10, SendMessageOptions.DontRequireReceiver);//Call the AdjustCurrentHealth method on the player and take off 10 health from it
            Destroy(gameObject);//Destory the projectile
            Instantiate(DeathEffect, transform.position, Quaternion.identity);//Spawn the Death effect
        }
    }    
}
