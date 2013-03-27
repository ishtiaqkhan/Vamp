using UnityEngine;
using System.Collections;

//This script is done by Jonthan and Ishtiaq
public class Player : MonoBehaviour {

    //testing git
  
    private int maxHealth = 100;   
    private int maxMagic = 100;
    private int magic;
    private float healthBarLength;
    private float magicBarLength;
    public int currentHealth = 100;
    public int currentMagic = 100;
    public Transform Hit; //The effect that appears when the player is hit
    private Quaternion rotation; //stores the rotation of hit effect
	public bool Drain; //Used to see if the player health should be drained
	private bool  burnCooldown  = true; //Timer cooldown so the health does not go down too fast
	private CharacterController controller; //Used to see if the player is off the ground 
	private int FallDamage = 0;//Stores the time that the player is off the ground for
	private int FallLimit = 50;//The ammout of fall damage the player can take

	// Use this for initialization
	void Start () {        
		controller = gameObject.GetComponent<CharacterController>(); //Getting the character contoroller component on the player
        healthBarLength = Screen.width / 2;
        magicBarLength = Screen.width / 2;
        rotation = new Quaternion(90, 90, 0, 0); //Storing the hit effect rotation
	}
	
	// Update is called once per frame
	void Update () {

       
        Magic();
		//If the drain is true and the cooldown is true then hurt the player and then set the drain to false so the it does not drain the players health too fast
		if(Drain == true && burnCooldown == true)
		{		
			StartCoroutine("Burn",1.0f);
		}
			if(Drain == true)
		{
			Drain = false;			
		}
							
		//If the player is off the ground increase the FallDamage and if its higher than the FallLimit hurt the player. If the player lands on the ground rest the FallDamage
		if (controller.isGrounded == false)
		{
			FallDamage++;
		}
		else
		{		
		//FallDamage is divide by two so the number returned is not too big
			if(FallDamage/2 > FallLimit)
			{				
				currentHealth -= ((FallDamage/2) - FallLimit);	
				//	AdjustCurrentHealth(-currentHealth);
			}			
			FallDamage = 0;		
		}
	}
    /*
    void OnGUI()
    {
        if(currentHealth > 0)
        GUI.Box(new Rect(10, 10, healthBarLength / (maxHealth / currentHealth), 30), "Health: " + currentHealth + "/" + maxHealth);

        if(currentMagic > 0)
        GUI.Box(new Rect(10, 50, magicBarLength / (maxMagic / currentMagic), 30), "Magic: " + currentMagic + "/" + maxMagic); 
    }
     * */

    public void AdjustCurrentHealth(int adj)
    {

       
        currentHealth += adj;

        healthBarLength = (Screen.width / 2) * (currentHealth / (float)maxHealth);

        Instantiate(Hit, transform.position, rotation);
      
        if (currentHealth < 0)
            KillPlayer();// kill player and game over is health is less than 0 

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        if (maxHealth < 1)
            maxHealth = 1;        
    }

    public void AdjustCurrentMagic(int adj)
    {


        currentMagic += adj;

        if (magicBarLength < 10)
            magicBarLength = 10;

        magicBarLength = (Screen.width / 2) * (currentMagic / (float)maxMagic);

        if (currentMagic < 0)
            currentMagic = 0;

        if (currentMagic > maxMagic)
            currentMagic = maxMagic;

        if (maxMagic < 1)
            maxMagic = 1;

   

        
    }
	
    void KillPlayer()
    {
		currentHealth = 0; // set health to zero 
        //add code to remove player and make game over screen 
    }
	
	//The Enumerator used to take the players health
	IEnumerator Burn(float TimerToStartAgain)
	{	
	//The cooldown is false so the health lost is done only once
		burnCooldown = false;
		//The ammout to take off the player
		AdjustCurrentHealth(-10);
		//Wait for the time given it the parament TimerToStartAgain
		yield return new WaitForSeconds(TimerToStartAgain);
		//Change it back to true so the health can be lost again
		burnCooldown = true;
	}
	
		
    void Magic()
    {
        if (currentMagic < maxMagic)
        {
            float rateSpeed = 0.5f;
            float adj = Time.deltaTime * rateSpeed;
            

            AdjustCurrentMagic((int)adj);
        }
    } 
	
	
	public int ReturnCurrentMagic() 
	{
		return currentMagic; 	
	}
	
	public int ReturnMaxMagic()
	{
		return maxMagic; 
	}
}
