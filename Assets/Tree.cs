using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour {
	
	// when an axe is touching the tree, the portion of the trunk that has been touched will be DESTROYED +
	//the upper part will have a NEW PARENT
	public void cutted(Vector3 position){ 
		Transform previous = null;
		Transform newParent = new GameObject().transform;
		foreach(Transform chunk in this.transform){ // go through each chunk and see which one is the CLOSEST TO THE IMPACT
			if(previous == null || Vector3.Distance (chunk.position, position ) < Vector3.Distance (previous.position, position)){
				previous = chunk; // CLOSEST TO THE IMPACT
			}
		}
		// iterate through every chunk and the ones that are HIGHER THAN IMPACT get a NEW PARENT
		while(this.transform.GetChild (this.transform.childCount-1).position.y > previous.position.y){
			this.transform.GetChild (this.transform.childCount-1).transform.parent = newParent;
		}
		
		Destroy (previous.gameObject); // DESTROY CLOSEST TO THE IMPACT
	}
	
	// this function will make the tree fall in a certain direction
	private void fall(Vector3 direction, ContactPoint rotationPlace){
		
	}

}
