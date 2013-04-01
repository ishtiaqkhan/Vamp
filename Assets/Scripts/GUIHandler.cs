using UnityEngine;
using System.Collections;

public class GUIHandler : MonoBehaviour {

    //testing sending files to ish

    private Player player;
    private GameObject tempGO;
    private float healthBarLength;
    private float magicBarLength;
    private int maxHealth = 100;
    private int maxMagic = 100;
	private bool toggleTutorial = false; 
	private float toggleButtonLength; 
	private float tutorialButtonX;
	private GameObject guiText; 
	private textScript tScript; 

    public GUISkin guiSkin; 


	// Use this for initialization
	void Start () {

        tempGO = GameObject.FindGameObjectWithTag("Player");
        player = (Player)tempGO.GetComponent("Player");
		
		//tutorialButtonX = Screen.width - 400;
		
        healthBarLength = Screen.width / 2;
        magicBarLength = Screen.width / 2;
		toggleButtonLength = Screen.width / 6 ; 
		
		
		
		guiText = GameObject.FindGameObjectWithTag("GUITEXT"); 
		
		//tScript = (textScript)guiText.GetComponent<textScript>(); 
		
	
		
		
	
		
		

      
	
	}
	
	// Update is called once per frame
	void Update () {
        healthBarLength = (Screen.width / 2) * (player.currentHealth / (float)maxHealth);
		
		ToggleTutorial() ;
	}

    void OnGUI()
    {
		
		
		
       // GUI.skin = guiSkin;
        if(player.currentHealth > 0)
        GUI.Box(new Rect(10, 10, healthBarLength / (maxHealth / player.currentHealth), 30), "Health: " + player.currentHealth + "/" + maxHealth);

        if(player.currentMagic > 0)
        GUI.Box(new Rect(10, 50, magicBarLength / (maxMagic / player.currentMagic), 30), "Magic: " + player.currentMagic + "/" + maxMagic); 
    }
	
	
	void ToggleTutorial()
	{
		if(toggleTutorial)
		{
			tScript.enabled = false; 		
		}
	}
}
