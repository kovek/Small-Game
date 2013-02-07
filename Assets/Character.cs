using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	
	private float speed = 0.2F;
	private Texture[] buffer = new Texture[2]; 
	private Texture image;
	
	void Start () {
		image = Resources.Load("sprite_sheet") as Texture;
		Debug.Log ("Is null?" + (image==null));
	}
	
	void OnGUI()
	{
		if (image == null) return;
		
		
		GUI.DrawTextureWithTexCoords(new Rect(100, 100, 100, 100), image, new Rect(0f, 0f, 0.333f, 0.333f), true);
	}
	
	void Update () {
		
	}
	
	public void move(KeyCode direction){
		Debug.Log("received a click");
		float blocks = 0.0f;
		if(direction == KeyCode.RightArrow){
			blocks = speed;
		}
		if(direction == KeyCode.LeftArrow){
			blocks = -speed;
		}
		mover.Move(blocks, 0, this);
	}
}
