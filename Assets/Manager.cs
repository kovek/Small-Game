using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager : MonoBehaviour{
	
	public Character player;
	public Character monster;
	private string world = "open"; // menu? playing?
	
	// transfore Input.Button into Input.KeyCode!
	Dictionary<int, KeyCode> mouseMap = new Dictionary<int, KeyCode>(){
		{0, KeyCode.Mouse0},
		{1, KeyCode.Mouse1}
	};
		
	void Start(){
		// remove collisions between the player and monsters
		Physics.IgnoreCollision(monster.GetComponent<Character>().collider, player.GetComponent<Character>().collider,true);
	}
	
	// when an event occurs?
	void OnGUI(){		
		Event e = Event.current;
		// its a keyboard button or mouse click:
		if(world == "open" && (e.keyCode != KeyCode.None || e.isMouse) ){
			if( (e.isKey || e.isMouse) && (e.type == EventType.KeyDown || (e.type == EventType.MouseDown || e.type == EventType.MouseDrag) ) ){
				player.letMeKnow(e.keyCode, "pressed");
				if(e.isMouse){
					player.letMeKnow (mouseMap[e.button], "pressed");
				}
			}else if( (e.isKey || e.isMouse) && (e.type == EventType.KeyUp || (e.type == EventType.MouseDown || e.type == EventType.MouseDrag) ) ){
				player.letMeKnow(e.keyCode, "released");
			}
		}
	}
	
}
