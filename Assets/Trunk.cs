using UnityEngine;
using System.Collections;

public class Trunk : MonoBehaviour {
	
	// call the cutted method of the parent
	public void cutted(Vector3 position){
		this.transform.parent.GetComponent<Tree>().cutted(position);
	}
}
