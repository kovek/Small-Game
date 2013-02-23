using UnityEngine;
using System.Collections;

public static class mover{
	
	//void Start(){}
	
	//void Update(){}
	
	public static float distance = 0.0001f;
	
	private static Vector3 positionOfObject;
	private static bool diagonal = false;
	
	static public void Move(float xaxis, float yaxis, Component theObject){
		Vector3 movement = new Vector3(xaxis, yaxis, 0.0f);
		CharacterController player = theObject.GetComponent<CharacterController>();
		player.Move(movement);
		
		float height = ((CharacterController)this.collider).radius;
		Ray rayy  = new Ray(this.transform.position, -1*Vector2.up);
		RaycastHit hity;
		if(Physics.Raycast(rayy, out hity) == true){
			Debug.DrawLine(rayy.origin, hity.point, Color.red);
			if(Vector3.Angle (hity.normal, Vector3.right)%90 != 0){
				// angle
			}
		}
	}
}