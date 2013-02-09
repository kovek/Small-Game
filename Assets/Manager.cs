using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour{
	
	public Character player;
	private string world = "open";
	KeyCode arrow = KeyCode.None; 	// this variable was made because holding an arrow causes 
									//a delay right after the first event of the arrow
	
	void Start(){
		
	}

	void Update(){
		if(arrow != KeyCode.None){ // every frame we check at the keys pressed
			if(world == "open" ){ // are we in the menu or in the game?
				player.move(arrow);
				player.giveState(arrow);
			}
		}
	}
	
	void OnGUI(){
		Event e = Event.current;
		if(e.isKey && e.type == EventType.KeyDown){
			if(e.keyCode == KeyCode.RightArrow || e.keyCode == KeyCode.LeftArrow ){
				arrow = e.keyCode; // setting the arrow which is being pressed
			}
		}
		if(e.type == EventType.KeyUp){
			if(e.keyCode == arrow)
			{arrow = KeyCode.None;} //  remove the currently pressed arrow
			if(world == "open") player.giveState (arrow); 	// The KeyCode is None, so
															//we are giving the idle state to the character
		}
	}
}
