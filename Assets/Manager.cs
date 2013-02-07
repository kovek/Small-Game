using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour{
	
	public Character player;
	private string world = "open";
	KeyCode arrow = KeyCode.None;
	
	void Start(){}
	
	void Update(){
		if(arrow != KeyCode.None){
			if(world == "open" ){
				player.move(arrow);
			}
		}
	}
	
	void OnGUI(){
		Debug.Log ("received content");
		Event e = Event.current;
		if(e.isKey && e.type == EventType.KeyDown){
			if(e.keyCode == KeyCode.RightArrow || e.keyCode == KeyCode.LeftArrow ){
				Debug.Log("You pressed " +e.keyCode);
				arrow = e.keyCode;
			}
		}
		if(e.type == EventType.KeyUp){
			arrow = KeyCode.None;	
		}
	}
}
