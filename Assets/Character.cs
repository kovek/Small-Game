using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {
	
	private float speed = 0.2F;
	private Texture image;
	private bool stateCanChange = true;
	private bool resetIdx = false; 	// this boolean will reset the sprite's count when true. 
							//This way, the animation will not start at the middle of
							//an action, think of an uppercut.
	private int direction = 0; // right: 0, left: 1, up: 2, down: 3
	
	private readonly int[] STANDING = {0, 0, 2};
	private readonly int[] WALKING = {1, 0, 2};
	Dictionary<KeyCode, int[]> states;
	
	public float animrate = 6f;
	public int tilesX = 3;
	public int tilesY = 3;
	public int[] state;
	public int aniSpeed = 1;
	
	float previousTime = 0f;
	
	void Start () {
		states = new Dictionary<KeyCode, int[]>(){
			{KeyCode.RightArrow, WALKING},
			{KeyCode.LeftArrow, WALKING},
			{KeyCode.None, STANDING}
		};
		this.state = STANDING;
	}
	
	void Update () {
		
		float currentTime = Time.time;
		int idx = (int)((currentTime-previousTime)*aniSpeed);
		if(resetIdx==true){
			idx = 0;
			previousTime = currentTime;
		}
		
		Debug.Log ("resetIdx: "+ (resetIdx));
		
		resetIdx = false;
		
		int x = idx % ( state[2] - state[1] + 1 );
		
		float tilingX = 1f/tilesX;
		
		float offsetX = ((float)(state[1]+(float)x)/(float)tilesX);	
		if(direction == 1){
			tilingX = tilingX*(-1f);
			offsetX = ((((float)(state[1]+(float)x))-tilesX+1)/(float)tilesX);
		}else if(direction == 0){
			offsetX = ((float)(state[1]+(float)x)/(float)tilesX);	
		}
		
		float offsetY = ((float)(tilesY-1-state[0])/(float)tilesY);
		
		renderer.material.SetTextureScale("_MainTex", new Vector2(tilingX, 1f/tilesY));
		renderer.material.SetTextureOffset("_MainTex", new Vector2(offsetX, offsetY));
	}
	
	public void giveState(KeyCode arrow){
		if(arrow == KeyCode.RightArrow) this.direction = 0;
		if(arrow == KeyCode.LeftArrow) this.direction = 1;
		
		if(stateCanChange == true){
		Debug.Log ("The key was " + arrow +", the direction:"+ direction);
		if(states[arrow] != state){ resetIdx = true; } // if it is a different state, reset the count. Put it to zero.
		this.state = states[arrow];
		}else{}
	}
	
	public void move(KeyCode direction){
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
