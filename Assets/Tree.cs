using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void cutted(Vector3 position){
		Transform previous = null;
		foreach(Transform chunk in this.transform){
			if(previous == null || Vector3.Distance (chunk.position, position ) < Vector3.Distance (previous.position, position)){
				previous = chunk;
			}
			Destroy (previous);
		}
	}
}
