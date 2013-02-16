using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {
	
	public float speed = 0.2F;
	
	private Texture image;
	private bool stateCanChange = true;
	private bool resetIdx = false; 	// this boolean will reset the sprite's count when true. 
							//This way, the animation will not start at the middle of
							//an action, think of an uppercut.
	private int direction = 0; // right: 0, left: 1, up: 2, down: 3
	
	public  const float JUMPSPEED = 0.2f;
	private const float GRAVITY = 0.001f;
	
	private readonly int[] STANDING = {0, 0, 2};
	private readonly int[] WALKING = {1, 0, 2};
	private readonly int[] JUMPING = {2, 0, 2};
	Dictionary<KeyCode, int[]> states;
	
	public float animrate = 6f;
	public int tilesX = 3;
	public int tilesY = 3;
	public int[] state;
	public int aniSpeed = 1;
	
	float previousTime = 0f;
	private Manager theManager;
	
	private bool jumping = false;
	private float inAirClock = 0.0f;
	private int jumpingClock;
	public int maximumJump = 200;
	
	void Start () {
		states = new Dictionary<KeyCode, int[]>(){
			{KeyCode.RightArrow, WALKING},
			{KeyCode.LeftArrow, WALKING},
			{KeyCode.None, STANDING},
			{KeyCode.UpArrow, JUMPING}
		};
		theManager = ((GameObject)GameObject.Find("Shell")).GetComponent<Manager>();
		this.state = STANDING;
	}
	
	void Update () {
		
		if(!theManager.isInAir(this)){ // check if the character is in the air, if isInAir returns false, then give him the standing state
			this.state = STANDING;
		}
		
		float jumpy = 0.0f;
		if(jumping /*&& jumpingClock < maximumJump*/){
			jumpy = JUMPSPEED;
			jumpingClock++;
		}
		if(theManager.isInAir(this)){
			inAirClock = inAirClock + 1.0f;
			jumpy = jumpy - GRAVITY*inAirClock/2;
			
			Debug.Log ("gravity!?>>"+jumpy + "<<WTF>>" + GRAVITY*(float)inAirClock/2+"<<InAirClock=>>" + inAirClock);
		}else{
			inAirClock = 0;
			Debug.Log ("is not in air!");
		}
		mover.Move (0, jumpy ,this);
		
		float currentTime = Time.time;
		int idx = (int)((currentTime-previousTime)*aniSpeed);
		if(resetIdx==true){
			idx = 0;
			previousTime = currentTime;
		}
		
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
		
		if(stateCanChange){
			if(states[arrow] != state){ resetIdx = true; } // if it is a different state, reset the count. Put it to zero.
			
			this.state = states[arrow];
			
		}else{}
	}
	
	public void OnUpKey(){
		this.state = JUMPING;
		jumping = true;
		stateCanChange = false;
	}
	
	public void OnReleaseUpKey(){
		stateCanChange = true;
		jumping = false;
	}
	
	public void move(KeyCode direction){
		float deltax = 0.0f;
		float deltay = 0.0f;
		if(direction == KeyCode.RightArrow){
			deltax = speed;
		}
		if(direction == KeyCode.LeftArrow){
			deltax = -speed;
		}
		if(direction == KeyCode.UpArrow){
			//deltay = speed;
			this.OnUpKey ();
		}
		//transform.Translate(blocks, 0f, 0f, Space.World);
		mover.Move(deltax, deltay, this);
	}
	
	public void move(){}
}
