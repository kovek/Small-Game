using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour, controllable, moveable {
	
	bool jumping = false;
	float deltay = 0.0f;
	float deltax = 0.0f;
	float deltaySpeed = 0.0f;
	private float GRAVITY = 0.02f;
	private bool pressingJump = false;
	private bool pressingWalk = false;
	private KeyCode pressingleftright = KeyCode.None;
	private int jumpClock = 0;
	private int[] state;
	private readonly int[] STANDING = {0, 0, 2};
	private readonly int[] WALKING = {1, 0, 2};
	private readonly int[] JUMPING = {2, 0, 2};
	private readonly int[] FALLING = {2, 0, 2};
	public float SPEED = 0.5f;
	private int direction;
	private bool jumpHolding = false;
	public BoxCollider rectangleBox;
	
	// Use this for initialization
	void Start () {
		//this.gameObject.AddComponent<CharacterController>();
		this.gameObject.AddComponent<BoxCollider>();
		this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
		if(this.tag == "monster"){
		//	this.GetComponent<CharacterController>().isTrigger = true;
			//this.GetComponent<CharacterController>().enabled = false;
			//this.GetComponent<CharacterController>().radius = 0f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(pressingJump){
			jump ();
		}
		if(pressingWalk){
			deltax = direction*SPEED;
		}else{
			deltax = 0;
		}
		deltay += deltaySpeed;
		if(isOnAir ()){ // is in air?
			if(deltay > 0){ // means the character is going up
				deltaySpeed -= GRAVITY/2;
			}else{ // means he is falling or not moving on the y axis, then he must fall
				deltaySpeed = -0.02f;
			}
		}
		
		
		{ // changing the STATE
			int[] tempState = state;
			if(isOnLadder ()){
				// return something about ladder, same for up and down
			}
			if(false){ //  later on replace this by ACTION
				
			}
			if(deltay > 0){
				// going up sprite
			}else if( (deltay < 0 && isOnAir () ) || (deltay == 0 && isOnAir())){
				// going down sprite
				state = JUMPING;
			}else if(deltax != 0){
				// moving left or right
				state = WALKING;
			}else if(deltax == 0 && deltay == 0){
				// standing
				state = STANDING;
			}
			if(tempState != state){ resetIdx = true;}
			else{ resetIdx = false; } // if the state is different,
													// we will start back at the beginning
													// of the state
		}
		
		setSpriteImage();
		translate (deltax, deltay);
		if(deltay > 0.05f){
			Debug.Log("you tried to jump");
		}
		
		if(isOnFloor ()){
			jumpClock = 0;
			if(deltay < 0 && pressingJump == true){ // means he's been falling lately
				jumpHolding = true;
				pressingJump = false;
			}
			if(deltay > 0.05f ){Debug.Log ("muhahaha"); }
			
			deltay = 0.0f;
			deltaySpeed = 0.0f;
		}
	}
	public void letMeKnow(KeyCode keyStroke, string state){
		if(state == "pressed"){
			if(keyStroke == KeyCode.UpArrow){
				if(isOnFloor()){
					pressingJump = true;
				}
				if(isOnAir()){
					// nothing?
				}
				if(isOnLadder()){
					// climbing up the ladder
				}
			}
			if(keyStroke == KeyCode.LeftArrow || keyStroke == KeyCode.RightArrow){
				pressingWalk = true;
				pressingleftright = keyStroke;
				if(keyStroke == KeyCode.LeftArrow){
					direction = -1;
				}
				else{
					direction = 1;
				}
			}
			if(keyStroke == KeyCode.DownArrow){
				
			}
		}else if(state == "released"){
			if(keyStroke == KeyCode.UpArrow){
				if(isOnAir() || isOnFloor()){
					pressingJump = false;
					jumpHolding = false;
				}
				if(isOnLadder ()){
					// stop climbing up the ladder
				}
			}
			if(keyStroke == KeyCode.LeftArrow || keyStroke == KeyCode.RightArrow){
				if(keyStroke == pressingleftright){
					pressingWalk = false;
					pressingleftright = KeyCode.None;
				}
			}
		}
	}
	
	public void jump(){
		if(jumpHolding == false){
			
			if(isOnFloor ()){
				Debug.Log ("jumped");
				deltaySpeed += 0.13f;
				jumpClock++;
			}

			
			if(isOnFloor() || (jumpClock > 0 && jumpClock < 14) ){
				jumpClock ++;
				if(jumpClock > 7 && jumpClock < 10){
					deltaySpeed += 0.02f;
				}
			}
		}
	}
	
	public bool isOnFloor(){
		if(!isOnAir() && !isOnLadder()){ return true;}
		else{return false;}
	}
	
	public bool isOnLadder(){
		return false;
	}
	
	void walk(){
		
	}
	void crouch(){
		
	}
	void getHit(){
		
	}
	
	public void translate(float x, float y){
		mover.Move (x, y, this);
	}
	
	public void OnTriggerEnter(Collider collider){
		if(collider.tag == "monster" && this.tag == "player"){
			Debug.Log ("OUCH!!!");
			translate (8.0f, 4.0f);
		}
	}
	
	public bool isOnAir(){
		float width = ((CharacterController)this.collider).radius*2f*this.transform.localScale.x;
		float height = ((CharacterController)this.collider).radius*2f*this.transform.localScale.z;
		Ray rayy  = new Ray(this.transform.position, -1*Vector2.up);
		RaycastHit hity;
		if(Physics.Raycast(rayy, out hity) == true){
			//Debug.DrawLine(rayy.origin, hity.point, Color.red);
			if(hity.distance - 0.4f <= (float)(height)/2 && (hity.distance > height/2 && (Vector3.Angle(hity.normal, Vector3.up)%90f > 2.0001f )  ) && (hity.collider.tag == "solid" )){ // the distance is very small
				return false;
			}else{

			}
		}
		
		
		Ray rayy1 = new Ray(this.transform.position+ new Vector3((float)width/2, 0, 0), -1f*Vector2.up);
		Ray rayy2 = new Ray(this.transform.position+ new Vector3((float)-width/2, 0, 0), -1f*Vector2.up);
		RaycastHit hity1;
		RaycastHit hity2;

		if(Physics.Raycast(rayy1, out hity1) == true){
			//Debug.DrawLine(rayy1.origin, hity1.point);
			if(hity1.distance - 0.3f <= (height/2) && (hity1.distance > height/2 && Vector3.Angle(hity1.normal, Vector3.up)%90 < 0.0001f)&& ( hity1.collider.tag == "solid"  || (hity1.collider.tag == "solidsoft") ) && Vector3.Angle(hity1.normal, Vector3.right)%90 < 0.0001f){
				return false;
			}
		}
		if(Physics.Raycast(rayy2, out hity2) == true){
			//Debug.DrawLine(rayy2.origin, hity2.point);
			if(hity2.distance - 0.3f  <= (height/2) && (hity2.distance > height/2 && Vector3.Angle(hity2.normal, Vector3.up)%90 < 0.0001f) && ( hity2.collider.tag == "solid"  || (hity2.collider.tag == "solidsoft") ) && Vector3.Angle(hity2.normal, Vector3.right)%90 < 0.0001f){
				return false;
			}
		}
		
		if(this.GetComponent<CharacterController>().isGrounded){
			return false;
		}
		
		return true;
	}
	
	// SPRITE VARIABLES
	public int tilesX  = 3;
	public int tilesY = 3;
	float previousTime;
	float aniSpeed = 1.5f;
	bool resetIdx = false;
	
	private void setSpriteImage(){
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
		if(direction == -1){
			tilingX = tilingX*(-1f);
			offsetX = ((((float)(state[1]+(float)x))-tilesX+1)/(float)tilesX);
		}else if(direction == 1){
			offsetX = ((float)(state[1]+(float)x)/(float)tilesX);	
		}
		
		float offsetY = ((float)(tilesY-1-state[0])/(float)tilesY);
		renderer.material.SetTextureScale("_MainTex", new Vector2(tilingX, 1f/tilesY));
		renderer.material.SetTextureOffset("_MainTex", new Vector2(offsetX, offsetY));
	}
}

public interface controllable{
	void letMeKnow(KeyCode keyStroke, string state);
}

public interface human{
	void jump();
	void walk();
	void crouch();
	void getHit();
}

public interface moveable{
	void translate(float x, float y);
}