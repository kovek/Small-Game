using UnityEngine;
using System.Collections;

public class TreeBase : MonoBehaviour {
	
	public int blocks = 5;
	
	void Update () {
	
	}
	
	void Start(){
			for(int i = 1; i < blocks; i++){
				GameObject newBlock = GameObject.Find ("Trunk");
				GameObject newBlock2 = (GameObject)Instantiate(newBlock, this.transform.position + i * new Vector3(0f, this.transform.localScale.y*2f, 0f), this.transform.rotation);
				newBlock2.transform.parent = this.transform.parent;
			}
		
	}

}
