using UnityEngine;
using System.Collections;

public static class mover{
	
	//void Start(){}
	
	//void Update(){}
	
	public static float distance = 0.0001f;
	
	private static Vector3 positionOfObject;
	
	static public void Move(float xaxis, float yaxis, Component theObject){
		Vector3 positionOfObject = theObject.transform.position;
	
		// deteriorated, MovePosition of rigidBody is a lot better theObject.transform.position = positionOfObject;

		float width = ((BoxCollider)theObject.collider).size.x;
		float height = ((BoxCollider)theObject.collider).size.z;

		
		int negative = 1;
		if(xaxis < 0 ){ negative = -1;}
		
		Ray rayx  = new Ray(theObject.transform.position + new Vector3((float)negative*width/2, 0, 0), (negative)*Vector2.right);
		Ray rayx1 = new Ray(theObject.transform.position + new Vector3((float)negative*width/2, 0, 0) + new Vector3(0, (float)height/2, 0), (negative)*Vector2.right);
		Ray rayx2 = new Ray(theObject.transform.position + new Vector3((float)negative*width/2, 0, 0) + new Vector3(0, (float)-height/2, 0), (negative)*Vector2.right);
		
		/*Ray rayx  = new Ray(theObject.transform.position, (negative)*Vector2.right);
		Ray rayx1 = new Ray(theObject.transform.position + new Vector3(0, (float)height/2, 0), (negative)*Vector2.right);
		Ray rayx2 = new Ray(theObject.transform.position + new Vector3(0, (float)-height/2, 0), (negative)*Vector2.right);
		*/
		
		RaycastHit hitx;
		RaycastHit hitx1;
		RaycastHit hitx2;
		if(Physics.Raycast(rayx, out hitx) == true){
			Debug.DrawLine(rayx.origin, hitx.point);
			if(hitx.distance <  distance + negative*xaxis ){
				xaxis = hitx.distance - distance;
				//xaxis = (float)(xaxis - (width /2));
				xaxis = negative * xaxis;
				Debug.Log ("bump!");
			}
		}
		if(Physics.Raycast(rayx1, out hitx1) == true){
			Debug.DrawLine(rayx1.origin, hitx1.point);
			if(hitx1.distance < distance + negative*xaxis ){
				xaxis = hitx1.distance - distance;
				//xaxis = (float)(xaxis - (width /2));
				xaxis = negative * xaxis;
				Debug.Log ("bump!");
			}
		}
		if(Physics.Raycast(rayx2, out hitx2) == true){
			Debug.DrawLine(rayx2.origin, hitx2.point);
			if(hitx2.distance < distance + negative*xaxis ){
				xaxis = hitx2.distance - distance;
				//xaxis = (float)(xaxis - (width /2));
				xaxis = negative * xaxis;
				Debug.Log ("bump!");
			}
		}
		
		
		negative = 1;
		if(yaxis < 0){ negative = -1;}
		Ray rayy  = new Ray(theObject.transform.position, negative*Vector2.up);
		Ray rayy1 = new Ray(theObject.transform.position+ new Vector3((float)width/2, 0, 0), negative*Vector2.up);
		Ray rayy2 = new Ray(theObject.transform.position+ new Vector3((float)-width/2, 0, 0), negative*Vector2.up);
		RaycastHit hity;
		RaycastHit hity1;
		RaycastHit hity2;
		if(Physics.Raycast(rayy, out hity) == true){
			Debug.DrawLine(rayy.origin, hity.point);
			if(hity.distance < (height/2) + negative*yaxis ){
				yaxis = hity.distance;
				yaxis = (float)(yaxis - (height/2));
				yaxis = negative * yaxis;
				Debug.Log ("bump!");
			}
		}
		if(Physics.Raycast(rayy1, out hity1) == true){
			Debug.DrawLine(rayy1.origin, hity1.point);
			if(hity1.distance < (height/2) + negative*yaxis ){
				yaxis = hity1.distance;
				yaxis = (float)(yaxis - (height/2));
				yaxis = negative * yaxis;
				Debug.Log ("bump!");
			}
		}
		if(Physics.Raycast(rayy2, out hity2) == true){
			Debug.DrawLine(rayy2.origin, hity2.point);
			if(hity2.distance < (height/2) + negative*yaxis ){
				yaxis = hity2.distance;
				yaxis = (float)(yaxis - (height/2));
				yaxis = negative * yaxis;
				Debug.Log ("bump!");
			}
		}
		
		
		positionOfObject.x = xaxis;
		positionOfObject.y = yaxis;
		positionOfObject.z = 0f;
		//positionOfObject += theObject.transform.position;
		//theObject.rigidbody.MovePosition(positionOfObject);
		theObject.transform.Translate (positionOfObject, Space.World);
		//Debug.Log ("moving" + positionOfObject);
	}
}