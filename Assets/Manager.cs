using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour{
	
	public Character player;
	private string world = "open";
	KeyCode arrow = KeyCode.None; 	// this variable was made because holding an arrow causes 
									// a delay right after the first event of the arrow
	
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
			if(e.keyCode == KeyCode.RightArrow || e.keyCode == KeyCode.LeftArrow || e.keyCode == KeyCode.UpArrow){
				arrow = e.keyCode; // setting the arrow which is being pressed
			}
		}
		if(e.type == EventType.KeyUp){
			if(e.keyCode == arrow)
			{
				arrow = KeyCode.None;
				if(e.keyCode == KeyCode.UpArrow){
					player.OnReleaseUpKey();
				}
			} //  remove the currently pressed arrow
			if(world == "open") player.giveState (arrow); 	// The KeyCode is None, so
															//we are giving the idle state to the character
		}
	}
	
	public bool isInAir(Component theObject){
		bool output = false;
		float width = ((BoxCollider)theObject.collider).size.x;
		float height = ((BoxCollider)theObject.collider).size.z;
		Ray rayy  = new Ray(theObject.transform.position, -1*Vector2.up);
		Ray rayy1 = new Ray(theObject.transform.position+ new Vector3((float)width/2, 0, 0), -1*Vector2.up);
		Ray rayy2 = new Ray(theObject.transform.position+ new Vector3((float)-width/2, 0, 0), -1*Vector2.up);
		RaycastHit hity;
		RaycastHit hity1;
		RaycastHit hity2;
		if(Physics.Raycast(rayy, out hity) == true){
			if(hity.distance - 0.000001 > (float)(height/2) ){
				output = true;
			}
		}
		if(Physics.Raycast(rayy1, out hity1) == true){
			if(hity1.distance - 0.000001 > (float)(height/2) ){
				output = true;
			}
		}
		if(Physics.Raycast(rayy2, out hity2) == true){
			if(hity2.distance - 0.000001 > (float)(height/2) ){
				output = true;
			}
		}
		//Debug.Log ("output" + hity.distance + " >> " + (float)height/2 );
		return output;
	}
}
