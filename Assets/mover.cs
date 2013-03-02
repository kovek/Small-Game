using UnityEngine;
using System.Collections;

public static class mover{
	
	//void Start(){}
	
	//void Update(){}
	
	public static float distance = 0.0001f;
	
	private static Vector3 positionOfObject;
	private static bool diagonal = false;
	
	static public void Move(float xaxis, float yaxis, Component theObject){
		float negative;
		if(xaxis < 0){
			negative = -1.0f;
		}else{
			negative = 1.0f;
		}
		
		float width = theObject.GetComponent<BoxCollider>().size.x*theObject.transform.localScale.x;
		float height = width*(theObject.transform.localScale.z/theObject.transform.localScale.x);
		
		Ray rayy  = new Ray(theObject.transform.position, -1*Vector2.up);
		RaycastHit hity;
		if(Physics.Raycast(rayy, out hity) == true){
			//Debug.DrawLine(rayy.origin, hity.point, Color.red);
			if(Vector3.Angle(hity.normal, negative*Vector3.right)%90 != 0 && Vector3.Angle(hity.normal, negative*Vector3.right)%90 > 45f){
				float down = Mathf.Tan ( ( Vector3.Angle (hity.normal, negative*Vector3.right)%90 ) * Mathf.Deg2Rad ) * negative * xaxis;
				if(hity.distance <= down + height/2f){
					if(yaxis == 0.0f){
					yaxis -= down;
					diagonal = true;
					}
				}
			}
		}
		

		
		Ray rayx1 = new Ray(theObject.transform.position + new Vector3((float)negative*width/2, 0, 0) + new Vector3(0, (float)height/2, 0), (negative)*Vector2.right);
		Ray rayx2 = new Ray(theObject.transform.position + new Vector3((float)negative*width/2, 0, 0) + new Vector3(0, (float)-height/2, 0), (negative)*Vector2.right);
		
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
			//Debug.DrawLine(rayx2.origin, hitx2.point);
			if(hitx2.distance < distance + negative*xaxis && hitx2.collider.tag == "solid" && Vector3.Angle(hitx2.normal, negative*Vector3.up)%90 < 0.0001f && ((Character)theObject).isOnAir()){
				xaxis = hitx2.distance - distance;
				xaxis = negative * xaxis;
			}
		}
		
		
		negative = 1;
		if(yaxis < 0){ negative = -1;}
		if(!diagonal){
			Ray rayy1 = new Ray(theObject.transform.position+ new Vector3((float)width/2, 0, 0), negative*Vector2.up);
			Ray rayy2 = new Ray(theObject.transform.position+ new Vector3((float)-width/2, 0, 0), negative*Vector2.up);
			Ray rayy3 = new Ray(theObject.transform.position, negative*Vector2.up);
	
			RaycastHit hity1;
			RaycastHit hity2;
			RaycastHit hity3;
	
			if(Physics.Raycast(rayy1, out hity1) == true){
				//Debug.DrawLine(rayy1.origin, hity1.point);
				if(hity1.distance < (height/2) + negative*yaxis && (hity1.collider.tag == "solid" || ( hity1.collider.tag == "solidsoft" && negative == -1 ) ) && Vector3.Angle(hity1.normal, negative*Vector3.right)%90 < 0.0001f){
					yaxis = hity1.distance;
					yaxis = (float)(yaxis - (height/2));
					yaxis = negative * yaxis;
				}
			}
			if(Physics.Raycast(rayy2, out hity2) == true){
				//Debug.DrawLine(rayy2.origin, hity2.point);
				if(hity2.distance < (height/2) + negative*yaxis && (hity2.collider.tag == "solid" || ( hity2.collider.tag == "solidsoft" && negative == -1 ) ) && Vector3.Angle(hity2.normal, negative*Vector3.right)%90 < 0.0001f){
					yaxis = hity2.distance;
					yaxis = (float)(yaxis - (height/2));
					yaxis = negative * yaxis;
				}
			}
			if(Physics.Raycast(rayy3, out hity3) == true){
				//Debug.DrawLine(rayy3.origin, hity3.point);
				if(hity3.distance < (height/2) + negative*yaxis && (hity3.collider.tag == "solid" || ( hity3.collider.tag == "solidsoft" && negative == -1 ) ) && Vector3.Angle(hity3.normal, negative*Vector3.right)%90 < 0.0001f){
					yaxis = hity3.distance;
					yaxis = (float)(yaxis - (height/2));
					yaxis = negative * yaxis;
				}
			}
		}
		
		Vector3 movement = new Vector3(xaxis, yaxis, 0.0f);
		CharacterController player = theObject.GetComponent<CharacterController>();
		player.Move(movement);
		diagonal = false;
	}
}