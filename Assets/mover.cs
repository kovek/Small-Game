using UnityEngine;
using System.Collections;

public static class mover{
	
	//void Start(){}
	
	//void Update(){}
	
	private static Vector3 positionOfObject;
	
	static public void Move(float xaxis, float yaxis, Component theObject){
		Vector3 positionOfObject = theObject.transform.position;
		positionOfObject.x += xaxis;
		positionOfObject.y += yaxis;
		theObject.transform.position = positionOfObject;
	}
}