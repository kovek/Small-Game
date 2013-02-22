using UnityEngine;
using System.Collections;

public static class mover{
	
	//void Start(){}
	
	//void Update(){}
	
	public static float distance = 0.0001f;
	
	private static Vector3 positionOfObject;
	private static bool diagonal = false;
	
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
			if(hitx.distance <  distance + negative*xaxis && hitx.collider.tag == "solid"){
				xaxis = hitx.distance - distance;
				//xaxis = (float)(xaxis - (width /2));
				xaxis = negative * xaxis;
				//Debug.Log ("bump!");
			}
		}
		if(Physics.Raycast(rayx1, out hitx1) == true){
			Debug.DrawLine(rayx1.origin, hitx1.point);
			if(hitx1.distance < distance + negative*xaxis && hitx1.collider.tag == "solid"){
				xaxis = hitx1.distance - distance;
				//xaxis = (float)(xaxis - (width /2));
				xaxis = negative * xaxis;
				//Debug.Log ("bump!");
			}
		}
		if(Physics.Raycast(rayx2, out hitx2) == true){
			Debug.DrawLine(rayx2.origin, hitx2.point);
			if(hitx2.distance < distance + negative*xaxis && hitx2.collider.tag == "solid"){
				/*
				xaxis = hitx2.distance - distance;
				xaxis = negative * xaxis;
				 */
				
				
				
				Debug.Log("eulerAngles: "+ hitx2.transform.eulerAngles); 	// OK THIS IS THE SHIET THAT APPEARS IN THE INSPECTOR. 
																			//YOU DO eulerAngles.x % 90 and it gives the angle on
																			// the right of the "triangle"
				
				//Debug.Log ("rotation: " + hitx2.normal.eulerAngles); // Ok this does not seem to work..
				
				
				// THIS WORKSSSSSSSSSSSSSSSSSS!!!!!!!!!
				Debug.Log ("real thing!!: " + Vector3.Angle (hitx2.normal, Vector3.left*negative) ); // this will give x y z coordinates like on a graphic,
																							// then you can just do: y/x to get the "pente"
				
				
				if(Vector3.Angle (hitx2.normal, Vector3.left*negative) > 45.0f){
					float desiredMov = 0.5f;
					float angle = 90f - Vector3.Angle (hitx2.normal, Vector3.left*negative);
					xaxis = Mathf.Cos(angle*Mathf.Deg2Rad)*desiredMov;
					yaxis = Mathf.Sin (angle*Mathf.Deg2Rad)*desiredMov;
					diagonal = true;
				}
				
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
			if(hity.distance < (height/2) + negative*yaxis  && hity.collider.tag == "solid"){
				yaxis = hity.distance;
				yaxis = (float)(yaxis - (height/2));
				yaxis = negative * yaxis;
				//Debug.Log ("bump!1");
			}
		}
		if(Physics.Raycast(rayy1, out hity1) == true){
			Debug.DrawLine(rayy1.origin, hity1.point);
			if(hity1.distance < (height/2) + negative*yaxis && hity1.collider.tag == "solid"){
				yaxis = hity1.distance;
				yaxis = (float)(yaxis - (height/2));
				yaxis = negative * yaxis;
				//Debug.Log ("bump2!");
			}
		}
		if(Physics.Raycast(rayy2, out hity2) == true){
			Debug.DrawLine(rayy2.origin, hity2.point);
			if(hity2.distance < (height/2) + negative*yaxis && hity2.collider.tag == "solid"){
				yaxis = hity2.distance;
				yaxis = (float)(yaxis - (height/2));
				yaxis = negative * yaxis;
				//Debug.Log ("bump3!");
			}
		}
		
		
		Vector3 positionOfObject2 = positionOfObject;
		positionOfObject.x = xaxis;
		positionOfObject.y = yaxis;
		positionOfObject.z = 0f;
		//positionOfObject.y = 0f;
		if(diagonal == true){
		Debug.Log (positionOfObject);
		}
		diagonal = false;
		//positionOfObject2.y = yaxis;
		//positionOfObject2.x = 0f;
		//positionOfObject2.z = 0f;
		
		
		theObject.transform.Translate (positionOfObject, Space.World);
		//theObject.transform.Translate (positionOfObject2, Space.World);
		//theObject.transform.Translate (positionOfObject, Space.World);
		
		
	}
}