using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour{
	
	public Character player;
	public Character monster;
	private string world = "open";
	//private KeyCode being

	
	void Start(){
		Physics.IgnoreCollision(monster.GetComponent<Character>().collider, player.GetComponent<Character>().collider,true);
	}

	void Update(){

	}
	
	void OnGUI(){
		
		Event e = Event.current;
		if(world == "open" && e.keyCode != KeyCode.None){
			if(e.isKey && e.type == EventType.KeyDown){
				player.letMeKnow(e.keyCode, "pressed");
			}else if(e.isKey && e.type == EventType.KeyUp){
				player.letMeKnow(e.keyCode, "released");
			}
		}
	}
	
}
