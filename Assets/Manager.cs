using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager : MonoBehaviour{
	
	public Character player;
	public Character monster;
	private string world = "open";
	//private KeyCode being
	
	Dictionary<int, KeyCode> mouseMap = new Dictionary<int, KeyCode>(){
		{0, KeyCode.Mouse0},
		{1, KeyCode.Mouse1}
	};
		
	void Start(){
		Physics.IgnoreCollision(monster.GetComponent<Character>().collider, player.GetComponent<Character>().collider,true);
	}

	void Update(){

	}
	
	void OnGUI(){
		
		Event e = Event.current;
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
