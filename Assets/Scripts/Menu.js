//Coded by Hamzah Shabbir
// Menu System allows audio to be played on hoover and click
// allows player to quit the game

//variables used
var levelToLoad : String;
var hover : AudioClip;
var click : AudioClip;
var QuitButton : boolean = false;
// When mouse hoovers  the button it plays the hover audio
function OnMouseEnter(){
audio.PlayOneShot(hover);
}
// when mouse clicks the button it plays the click audio
function OnMouseUp(){
audio.PlayOneShot(click);
yield new WaitForSeconds(0.35);
// Allows the player to quit the game using the quit button
if(QuitButton){
Application.Quit();
}
else{
// allows the player begin the game by indentifying the level to be loaded
Application.LoadLevel(levelToLoad);
}
}
// Accesses Audio Source
@script RequireComponent(AudioSource)