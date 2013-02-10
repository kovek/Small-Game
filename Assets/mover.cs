using UnityEngine;
using System.Collections;

public static class mover{
	
	//void Start(){}
	
	//void Update(){}
	
	private static Vector3 positionOfObject;
	
	static public void Move(float xaxis, float yaxis, Component theObject){
		Vector3 positionOfObject = theObject.transform.position;
	
		// deteriorated, MovePosition of rigidBody is a lot better theObject.transform.position = positionOfObject;
		
		int negative = 1;
		if(xaxis < 0 ){ negative = -1;}
		Ray rayx = new Ray(theObject.transform.position, (negative)*Vector2.right);
		RaycastHit hitx;
		if(Physics.Raycast(rayx, out hitx) == true){
			BoxCollider width = (BoxCollider)theObject.collider;
			if(hitx.distance < width.size.x / 2 ){xaxis = 0;}
		}
		
		Ray rayy = new Ray(theObject.transform.position, (negative)*Vector2.up);
		RaycastHit hity;
		if(Physics.Raycast(rayy, out hity) == true){
			BoxCollider height = (BoxCollider)theObject.collider;
			if(hity.distance < height.size.y / 2){yaxis = 0;}
		}
		
		positionOfObject.x += xaxis;
		positionOfObject.y += yaxis;
		
		theObject.rigidbody.MovePosition(positionOfObject);
	}
}