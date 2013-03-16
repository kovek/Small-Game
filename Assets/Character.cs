using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour, controllable, moveable {
	
	// trying out the "edit in repo" functionality of github. How does this work??
	
	// movement variables
	float deltay = 0.0f;
	float deltax = 0.0f;
	float deltaySpeed = 0.0f;
	private float GRAVITY = 0.02f;
	private int direction;
	public float SPEED = 0.5f;

	// state variables
	private bool pressingJump = false;
	private bool pressingWalk = false;
	private KeyCode pressingleftright = KeyCode.None;
	private int jumpClock = 0;
	private bool jumpHolding = false;
	
	// state arrays for the display/ANIMATION of the CHARACTER
	private int[] state;
	private readonly int[] STANDING = {0, 0, 2};
	private readonly int[] WALKING = {1, 0, 2};
	private readonly int[] JUMPING = {2, 0, 2};
	private readonly int[] FALLING = {2, 0, 2};
	
	// SPRITE VARIABLES
	public int tilesX  = 3;
	public int tilesY = 3;
	float previousTime;
	float aniSpeed = 1.5f;
	bool resetIdx = false;
	
	// a torso, where the object would ACTUALLY GET HIT
	public BoxCollider rectangleBox;

	// skill variables, in this case, the axe swing distance
	public float axeSwingDistance = 10.0f;
	
	void Start () {
		this.gameObject.AddComponent<BoxCollider>();
		this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
		if(this.tag == "monster"){

		}
	}
	
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
			if(deltay > 0){ // means the character is GOING UP
				deltaySpeed -= GRAVITY/2;
			}else{ // means he is FALLING or NOT MOVING on the y axis, then he must fall
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
			else{ resetIdx = false; } // if the state is DIFFERENT,
										// we will start BACK AT THE BEGINNING
										// of the state
		}
		
		setSpriteImage();
		translate (deltax, deltay);
		
		if(isOnFloor ()){
			jumpClock = 0;
			if(deltay != 0f && pressingJump == true){ // means he's been falling lately
				jumpHolding = true;
				pressingJump = false;
			}else{
				deltay = 0.0f;
				deltaySpeed = 0.0f;
			}
		}
	}
	
	// receive the keyboard/mouse INPUTS
	public void letMeKnow(KeyCode keyStroke, string state){
		if(state == "pressed"){
			if(keyStroke == KeyCode.W){
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
			if(keyStroke == KeyCode.A || keyStroke == KeyCode.D){
				pressingWalk = true;
				pressingleftright = keyStroke;
				if(keyStroke == KeyCode.A){
					direction = -1;
				}
				else{
					direction = 1;
				}
			}
			if(keyStroke == KeyCode.Mouse0){
				// flashlight and shiet
				Debug.Log ("mouse0 clicked");
				Vector3 mousePos = Input.mousePosition;
				mousePos.z += 32.0f;
				Vector3 clickedPosition = Camera.main.ScreenToWorldPoint(mousePos);
				clickedPosition.z = 4.0f;
				Debug.DrawRay(this.transform.localPosition, clickedPosition, Color.red);
				Vector3 delta = clickedPosition - this.transform.position;
				if(Mathf.Abs(delta.y) > Mathf.Abs(delta.x) && delta.y > 0f){
					//up
				}else if(Mathf.Abs(delta.x) > Mathf.Abs(delta.y) && delta.x > 0f){
					Debug.Log ("right");
				}else if(Mathf.Abs(delta.x) > Mathf.Abs(delta.y) && delta.x < 0f){
					// left	
				}else if(Mathf.Abs(delta.y) > Mathf.Abs(delta.x) && delta.y < 0f){
					// down
				}
			}
			if(keyStroke == KeyCode.UpArrow){
				// axe to the top
			}
			if(keyStroke == KeyCode.LeftArrow || keyStroke == KeyCode.RightArrow){
				int negative = 1;
				if(keyStroke == KeyCode.RightArrow){
					negative = 1;
				}else if(keyStroke == KeyCode.LeftArrow){
					negative = -1;
				}
				Ray left = new Ray(this.transform.position, Vector3.right*negative);
				RaycastHit leftHit;
				if(Physics.Raycast(left, out leftHit) == true){
					Debug.DrawLine(this.transform.position, leftHit.point, Color.red);
					if(leftHit.distance <= axeSwingDistance){
						leftHit.collider.gameObject.GetComponent<Trunk>().cutted(leftHit.point);
					}
				}
			}
			if(keyStroke == KeyCode.DownArrow){
				
			}
		}else if(state == "released"){
			if(keyStroke == KeyCode.W){
				if(isOnAir() || isOnFloor()){
					pressingJump = false;
					jumpHolding = false;
				}
				if(isOnLadder ()){
					// stop climbing up the ladder
				}
			}
			if(keyStroke == KeyCode.A || keyStroke == KeyCode.D){
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
				translate (0.0f, 0.5f);
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
	
	// MOVE the character in a direction
	public void translate(float x, float y){
		mover.Move (x, y, this);
	}
	
	// COLLIDER with something else, a monster maybe?
	public void OnTriggerEnter(Collider collider){
		if(collider.tag == "monster" && this.tag == "player"){
			Debug.Log ("OUCH!!!");
			translate (8.0f, 4.0f);
		}
	}
	
	// indicates if the Cahracter is in the AIR or not.
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
	
	// this will check the state and apply an image to the character depending on the situation
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
