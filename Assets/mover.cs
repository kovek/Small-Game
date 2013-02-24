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
		
		float height = ((CharacterController)theObject.collider).radius;
		Ray rayy  = new Ray(theObject.transform.position, -1*Vector2.up);
		RaycastHit hity;
		if(Physics.Raycast(rayy, out hity) == true){
			Debug.DrawLine(rayy.origin, hity.point, Color.red);
			if(Vector3.Angle(hity.normal, negative*Vector3.right)%90 != 0){
				float down = Mathf.Tan ( ( Vector3.Angle (hity.normal, negative*Vector3.right)%90 ) * Mathf.Deg2Rad ) * negative * xaxis;
				if(hity.distance <= 2 * down){
					if(yaxis == 0.0f){
					yaxis -= down;
					}
				}
			}	
		}
		
		Vector3 movement = new Vector3(xaxis, yaxis, 0.0f);
		CharacterController player = theObject.GetComponent<CharacterController>();
		player.Move(movement);
	}
}