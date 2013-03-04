using UnityEngine;
using System.Collections;

public class TreeBase : MonoBehaviour {
	
	// height of the tree
	public int blocks = 5;
	
	// CREATE the TRUNK
	void Start(){
		for(int i = 0; i < blocks; i++){
			GameObject newBlock = GameObject.Find ("Trunk");
			GameObject newBlock2 = (GameObject)Instantiate(newBlock,
				this.transform.position
					+ i * new Vector3(0f, newBlock.GetComponent<BoxCollider>().size.z*newBlock.transform.localScale.z, 0f)
					+ new Vector3(0f, this.GetComponent<BoxCollider>().size.z*this.transform.localScale.z/2f, 0f),
				this.transform.rotation);
			newBlock2.transform.parent = this.transform.parent;
		}
	}

}