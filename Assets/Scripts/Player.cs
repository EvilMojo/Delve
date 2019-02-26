using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	//Player Identification
	public string playerName;
	public int playerIndex;

	//External Game Objects
	public GameObject mgrObj;
	public GameManager mgr;

	//Camerawork
	public GameObject playerModel;
	public GameObject camPointer;
	public GameObject playerCamera;
	public float minZoom = 10.0f;

	//Player Inventory
	public GameObject inventoryManager;
	public GameObject shoppingAt;
	public GameObject itemPickup;
	private int inventoryMoveTimer;

	//Sprites and animation
	Sprite[] testSprites;
	public GameObject hitbox;
	public int framesleft;
	public string attackstate;
	private int movepause;
	Vector2 gravity;

	//Physics
	float hspeed;
	float vSpeed;
	float jumpSpeed;
	bool initiated = false;
	bool grounded;

	//Inputs
	string LeftJoystickX;
	string LeftJoystickY;
	string RightJoystickX;
	string RightJoystickY;
	string A;
	string B;
	string X;
	string Y;
	double joystickDeadZone = 0.5;

	// Use this for initialization
	void Start () {
		hspeed = 2.0f;
		vSpeed = -2.0f;
		jumpSpeed = 12.0f;
		framesleft = 0;
		movepause = 0;
		attackstate = "passive";

		print ("Initialising Player " + (playerIndex + 1).ToString());
		LeftJoystickX = "Player " + (playerIndex+1).ToString() + " LeftJoystickX";
		LeftJoystickY = "Player " + (playerIndex+1).ToString() + " LeftJoystickY";
		RightJoystickX = "Player " + (playerIndex+1).ToString() + " RightJoystickX";
		RightJoystickY = "Player " + (playerIndex+1).ToString() + " RightJoystickY";
		A = "Player " + (playerIndex+1).ToString() + " A";
		B = "Player " + (playerIndex+1).ToString() + " B";
		X = "Player " + (playerIndex+1).ToString() + " X";
		Y = "Player " + (playerIndex+1).ToString() + " Y";
	
		inventoryManager = new GameObject ();
		inventoryManager.AddComponent<PlayerInventory> ();
		inventoryManager.name = "Player " + this.playerIndex.ToString () + " Inventory";
		inventoryManager.transform.SetParent(this.gameObject.transform);
		inventoryManager.GetComponent<PlayerInventory> ().owningPlayer = this.gameObject;

		//gravity = -42.5f;
		//spriteManager = GameObject.Find ("SpriteManager");
//		velocity = new Vector3 (0, 0, 0);
	}

	//Update is called once per frame

	void Update() {


		if (hitbox != null && framesleft == 0) {
			Destroy (hitbox);
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = (Sprite) testSprites [0];
			attackstate = "passive";
		}

		if (framesleft > 0) {
			//print ("framesleft: " + framesleft);
			framesleft--;
		}

		if (inventoryMoveTimer > 0) {
			inventoryMoveTimer--;
		}

		if (inventoryManager != null) {
			if (inventoryManager.GetComponent<PlayerInventory> ().inventoryOpen ()) {
				if (inventoryMoveTimer==0) {
					if (Input.GetAxis (LeftJoystickX) > 0.7) {
						inventoryManager.GetComponent<PlayerInventory> ().moveInventoryCursorX (1);
					} else if (Input.GetAxis (LeftJoystickX) < -0.7) {
						inventoryManager.GetComponent<PlayerInventory> ().moveInventoryCursorX (-1);
					} else {
					}
					if (Input.GetAxis (LeftJoystickY) > 0.7) {
						inventoryManager.GetComponent<PlayerInventory> ().moveInventoryCursorY (-1);
					} else if (Input.GetAxis (LeftJoystickY) < -0.7) {
						inventoryManager.GetComponent<PlayerInventory> ().moveInventoryCursorY (1);
					} else {
					}
					inventoryMoveTimer = 3;
				}
			}
		}

		if (Input.GetButtonDown (A)) {
			if (inventoryManager.GetComponent<PlayerInventory> ().inventoryOpen ()) {
				inventoryManager.GetComponent<PlayerInventory> ().action ();
			} else {
				if (isGrounded ()) {
					switch (attackstate) {
					case ("passive"):
						framesleft = 30;
						attackstate = "groundneutralattack1";
						this.gameObject.GetComponent<SpriteRenderer> ().sprite = (Sprite)testSprites [11];
						break;
					case ("groundneutralattack1"):
						if (framesleft <= 20) {
							framesleft = 45;
							attackstate = "groundneutralattack2";
							this.gameObject.GetComponent<SpriteRenderer> ().sprite = (Sprite)testSprites [13];
						}
						break;
					case ("groundneutralattack2"):
						if (framesleft <= 20) {
							framesleft = 60;
							attackstate = "groundneutralattack3";
							this.gameObject.GetComponent<SpriteRenderer> ().sprite = (Sprite)testSprites [16];
						}
						break;
					case ("groundneutralattack3"):
						break;
					}
				}
			}
		}

		if (Input.GetButtonDown (X)) {
			print (itemPickup);
			if (itemPickup != null) {
//				inventoryManager.GetComponent<PlayerInventory> ().backdrop.GetComponent<SpriteRenderer> ().enabled = false;
//				inventoryManager.GetComponent<PlayerInventory>().backdrop.SetActive (true);
				inventoryManager.GetComponent<PlayerInventory>().addItem (itemPickup);
//				inventoryManager.GetComponent<PlayerInventory>().backdrop.SetActive (false);
				itemPickup = null;
			}
		}

		if (Input.GetButtonDown (B)) {
			this.inventoryManager.GetComponent<PlayerInventory> ().toggleInventory ();

			if (shoppingAt != null) {
				if (shoppingAt.GetComponent<Shop> ().shopOpen ()) {
					shoppingAt.GetComponent<Shop> ().hideShopInventory ();
				} else {
					shoppingAt.GetComponent<Shop> ().displayShopInventory (this.gameObject);
				}
			}
		}

		switch (attackstate) {
		case ("passive"):
			break;
		case ("groundneutralattack1"):
			if (framesleft == 20) {
				//Generate hitbox on this frame
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = (Sprite)testSprites [12];

				hitbox = new GameObject ();
				hitbox.AddComponent<Hitbox> ();
				hitbox.GetComponent<Hitbox>().init(
					10, 
					new Vector2(1.0f,.4f), 
					this.gameObject.transform,
					new Vector3(.5f,0,0),
					this.gameObject.GetComponent<SpriteRenderer> ().flipX,
					new Vector2(1.0f, 0.0f),
					.0f, 2);
			}
			if (framesleft == 0) {
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = (Sprite)testSprites [18];
				attackstate = "passive";
			}
			break;
		case ("groundneutralattack2"):
			if (framesleft == 35) {
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = (Sprite)testSprites [14];
			}
			if (framesleft == 20) {
				//Generate hitbox on this frame
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = (Sprite)testSprites [15];
				hitbox = new GameObject ();
				hitbox.AddComponent<Hitbox> ();
				hitbox.GetComponent<Hitbox>().init(
					10, 
					new Vector2(1.0f,.4f), 
					this.gameObject.transform,
					new Vector3(.5f,0,0),
					this.gameObject.GetComponent<SpriteRenderer> ().flipX,
					new Vector2(2.0f, 0.2f),
					.0f, 5);
			}
			if (framesleft == 0) {
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = (Sprite)testSprites [18];
				attackstate = "passive";
			}
			break;
		case ("groundneutralattack3"):
			if (framesleft == 45) {
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = (Sprite)testSprites [17];
			}
			if (framesleft == 35) {
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = (Sprite)testSprites [18];
			}
			if (framesleft == 20) {
				//Generate hitbox on this frame
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = (Sprite)testSprites [19];
				hitbox = new GameObject ();
				hitbox.AddComponent<Hitbox> ();
				hitbox.GetComponent<Hitbox>().init(
					10, 
					new Vector2(1.0f,.4f), 
					this.gameObject.transform,
					new Vector3(.5f,0,0),
					this.gameObject.GetComponent<SpriteRenderer> ().flipX,
					new Vector2(50.0f, 500.0f),
					1.0f, 10);
			}
			if (framesleft == 5) {
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = (Sprite)testSprites [18];
			}
			if (framesleft == 0) {
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = (Sprite)testSprites [18];
				attackstate = "passive";
			}
			break;
		}
	}

	//FixedUpdate is called depending on physics requirements
	void FixedUpdate () {
		
		if (initiated) {

//			print (distToGround);
			//Vector2 velocity = new Vector2(Input.GetAxisRaw(LeftJoystickX)*hspeed, 0);
			Vector2 velocity = new Vector2(0,0);
			//playerBody.GetComponent<Rigidbody2D> ().gravityScale = 10.0f;
			if (attackstate == "passive") {
				if (Input.GetAxis (LeftJoystickX) > 0.2) {
					this.gameObject.GetComponent<SpriteRenderer> ().flipX = false;
				} else if (Input.GetAxis (LeftJoystickX) < -0.2) {
					this.gameObject.GetComponent<SpriteRenderer> ().flipX = true;
				}
				//print (inventoryManager.GetComponent<PlayerInventory> ().inventoryOpen ());
				if(inventoryManager!=null) {
					if (inventoryManager.GetComponent<PlayerInventory> ().inventoryOpen () ==false) {
						if (Input.GetAxis (LeftJoystickX) > 0.8) {
							velocity.x = 3;
						} else if (Input.GetAxis (LeftJoystickX) > joystickDeadZone) {
							velocity.x = 1;
						} else if (Input.GetAxis (LeftJoystickX) < -joystickDeadZone && Input.GetAxis (LeftJoystickX) > -0.79) {
							velocity.x = -1;
						} else if (Input.GetAxis (LeftJoystickX) < -0.8) {
							velocity.x = -3;
						} else {
							velocity.x = 0;
						}
					}
					//redundant, player can't move with inventory open
					//if (velocity.x != 0) {
					//	inventoryManager.GetComponent<PlayerInventory> ().hideInventory ();
					//}
				}
				if (this.isGrounded()) {
					vSpeed = 0;
					if (Input.GetButtonDown (Y)) {
						//print (Y);

						vSpeed = jumpSpeed;
					}
				}
			}
 

			vSpeed -= 30.0f * Time.deltaTime;
			velocity.y = vSpeed;

			if (!isGrounded ()) {
				if (velocity.y > 0) {
					this.gameObject.GetComponent<SpriteRenderer> ().sprite = (Sprite)testSprites [1];
				} else if (velocity.y < 0) {
					this.gameObject.GetComponent<SpriteRenderer> ().sprite = (Sprite)testSprites [2];
				}
			}

			if (isGrounded ()) {
				if (velocity.x == 0 && attackstate == "passive") {
					this.gameObject.GetComponent<SpriteRenderer> ().sprite = (Sprite)testSprites [0];
				} else { 
					if (movepause == 0) {

						movepause = 8;
						if (velocity.x == 3 || velocity.x == -3) {

							if (!(this.gameObject.GetComponent<SpriteRenderer> ().sprite == (Sprite)testSprites [7] ||
							    this.gameObject.GetComponent<SpriteRenderer> ().sprite == (Sprite)testSprites [8] ||
							    this.gameObject.GetComponent<SpriteRenderer> ().sprite == (Sprite)testSprites [9] ||
							    this.gameObject.GetComponent<SpriteRenderer> ().sprite == (Sprite)testSprites [10])) {
								this.gameObject.GetComponent<SpriteRenderer> ().sprite = (Sprite)testSprites [7];
							}
							if (this.gameObject.GetComponent<SpriteRenderer> ().sprite == (Sprite)testSprites [10]) {
								this.gameObject.GetComponent<SpriteRenderer> ().sprite = (Sprite)testSprites [7];
								//print ("10");
							} else if (this.gameObject.GetComponent<SpriteRenderer> ().sprite == (Sprite)testSprites [9]) {
								this.gameObject.GetComponent<SpriteRenderer> ().sprite = (Sprite)testSprites [10];
								//print ("9");
							} else if (this.gameObject.GetComponent<SpriteRenderer> ().sprite == (Sprite)testSprites [8]) {
								this.gameObject.GetComponent<SpriteRenderer> ().sprite = (Sprite)testSprites [9];
								//print ("8");
							} else if (this.gameObject.GetComponent<SpriteRenderer> ().sprite == (Sprite)testSprites [7]) {
								this.gameObject.GetComponent<SpriteRenderer> ().sprite = (Sprite)testSprites [8];
								//print ("7");
							}
						} else if (velocity.x == 1 || velocity.x == -1) {
							if (!(this.gameObject.GetComponent<SpriteRenderer> ().sprite == (Sprite)testSprites [3] ||
							   this.gameObject.GetComponent<SpriteRenderer> ().sprite == (Sprite)testSprites [4] ||
							   this.gameObject.GetComponent<SpriteRenderer> ().sprite == (Sprite)testSprites [5] ||
							   this.gameObject.GetComponent<SpriteRenderer> ().sprite == (Sprite)testSprites [6])) {
								this.gameObject.GetComponent<SpriteRenderer> ().sprite = (Sprite)testSprites [3];
							}
							if (this.gameObject.GetComponent<SpriteRenderer> ().sprite == (Sprite)testSprites [6]) {
								this.gameObject.GetComponent<SpriteRenderer> ().sprite = (Sprite)testSprites [3];
								//print ("10");
							} else if (this.gameObject.GetComponent<SpriteRenderer> ().sprite == (Sprite)testSprites [5]) {
								this.gameObject.GetComponent<SpriteRenderer> ().sprite = (Sprite)testSprites [6];
								//print ("9");
							} else if (this.gameObject.GetComponent<SpriteRenderer> ().sprite == (Sprite)testSprites [4]) {
								this.gameObject.GetComponent<SpriteRenderer> ().sprite = (Sprite)testSprites [5];
								//print ("8");
							} else if (this.gameObject.GetComponent<SpriteRenderer> ().sprite == (Sprite)testSprites [3]) {
								this.gameObject.GetComponent<SpriteRenderer> ().sprite = (Sprite)testSprites [4];
								//print ("7");
							}
						}
					} else
						movepause--;
				}
			}

			velocity.x *= hspeed;

			//print ("Velocity: (" + velocity + ")");
			//playerBody.GetComponent<Rigidbody2D> ().AddForce (velocity);
			playerModel.GetComponent<Rigidbody2D> ().MovePosition (playerModel.GetComponent<Rigidbody2D>().position + velocity * Time.deltaTime);

			playerCamera.GetComponent<Camera>().transform.position = new Vector3(playerModel.transform.position.x, 
														playerModel.transform.position.y,
														playerModel.transform.position.z - 20.0f);



			camPointer.transform.position = new Vector3 (playerModel.transform.position.x, playerModel.transform.position.y, playerModel.transform.position.z - 20.0f);
			camPointer.transform.parent = this.gameObject.transform;
			//velocity.y -= gravity * Time.deltaTime;
		}

	}

	public void init(GameObject playerBody) {

		mgrObj = GameObject.Find ("GameManager");
		mgr = mgrObj.GetComponent<GameManager>();

		//playerBody = new GameObject ();

		playerBody.AddComponent<SpriteRenderer> ();


		testSprites = new Sprite[24];
		testSprites = Resources.LoadAll<Sprite>("Sprites/testsprites" + (playerIndex+1).ToString());
		playerBody.GetComponent<SpriteRenderer> ().sprite = (Sprite) testSprites [0];
		//playerBody.GetComponent<SpriteRenderer> ().sprite = spriteManager.GetComponent<SpriteManager>().playerIdle;

		//playerBody.AddComponent<MeshFilter> ();

		//playerBody.GetComponent<MeshFilter> ().mesh = new Mesh ();
		//playerBody.GetComponent<MeshFilter> ().mesh = mgr.createMesh (.2f, .2f);


		//playerBody.AddComponent<MeshRenderer>();
		//playerBody.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load("bigtestpng") as Texture2D;

		//playerBody.AddComponent<BoxCollider2D> ();
		//playerBody.AddComponent<CharacterController>();
		playerBody.AddComponent<BoxCollider2D>();
		playerBody.AddComponent<Rigidbody2D>();
		//playerBody.name = "Player 1";

	//	"Use physics queries to detect collisions, scripts to decide where/how moves
		//moves via velocity
		//does not collide with other Kinematic Rbody2Ds or with Static Rigidbody2Ds... Only collides with Dynamic
		playerBody.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;

		//playerBody.AddComponent<Player> ();

		playerBody.GetComponent<Rigidbody2D> ().freezeRotation = true;
		/*if (playerBody.GetComponent<CharacterController> () == null) {
			controller = playerBody.AddComponent<CharacterController> ();
			controller.detectCollisions = true;
			controller.enabled = true;
		}*/

		this.playerModel = playerBody;

		playerBody.layer = 2;
		playerCamera = new GameObject ("playerCam");

		playerCamera.AddComponent<Camera> ();
		//playerCamera.AddComponent<CameraPointer> ();
		playerCamera.GetComponent<Camera>().orthographic = true;
		playerCamera.GetComponent<Camera>().transform.rotation = new Quaternion(0, 0, 0, 0);

		camPointer = new GameObject("camPointer");
		playerCamera.transform.parent = camPointer.transform;
		playerCamera.transform.position = new Vector3(0,0, camPointer.transform.position.z-2.0f);

		playerCamera.GetComponent<Camera> ().targetDisplay = 0;

		if (mgr.cameraSetting == "shared") {
			playerCamera.SetActive (false);
		}
		initiated = true;

		//distToGround = playerBody.GetComponent<BoxCollider2D> ().bounds.extents.y;
		//print (distToGround);
	}

	public bool isGrounded() {
		//Debug.DrawRay (playerModel.transform.position, -Vector2.up, Color.green, distToGround + 0.1f);
		Debug.DrawRay (new Vector3(playerModel.transform.position.x-playerModel.transform.localScale.x/2,
			playerModel.transform.position.y-playerModel.transform.localScale.y/1.8f, playerModel.transform.position.z), Vector2.right, Color.red, playerModel.transform.localScale.x);
		return Physics2D.Raycast (new Vector3(playerModel.transform.position.x-playerModel.transform.localScale.x/2,
			playerModel.transform.position.y-playerModel.transform.localScale.y/1.8f, playerModel.transform.position.z), Vector2.right, playerModel.transform.localScale.x);
		//return Physics2D.Ch
	/*	RaycastHit2D hit = Physics2D.Raycast (playerBody.transform.position, -Vector2.up, distToGround + 0.1f);

		if (hit.collider != playerBody.GetComponent<BoxCollider2D> ()) {
			print ("Something else");
		} 
		return false;*/
	}

	public void ground(bool g) {
		grounded = g;
	}

	public void toggleCamera(bool toggle) {
		playerCamera.GetComponent<Camera>().gameObject.SetActive(toggle);
	}

	public float getCameraPosX() {
		return playerCamera.transform.position.x;
	}

	public float getCameraPosY() {
		return playerCamera.transform.position.y;
	}

	public void createPlayer(string name, int playerIndex, int players) {
		//set name
		mgrObj = GameObject.Find ("GameManager");
		mgr = mgrObj.GetComponent<GameManager>();
		//print ("creating" + playerName);

		//create a player object
		this.playerName = playerName;
		this.playerIndex = playerIndex;

		playerModel = this.gameObject;

		playerModel.AddComponent<CharacterController>();

		playerModel.AddComponent<SpriteRenderer> ();

		testSprites = new Sprite[4];
		testSprites = Resources.LoadAll<Sprite>("Sprites/testsprites");
		playerModel.GetComponent<SpriteRenderer> ().sprite = (Sprite) testSprites [0];
		//set stats
		/*playerModel = new GameObject ();
		playerModel = Instantiate (Resources.Load ("Prefabs/playerModel", typeof(GameObject))) as GameObject;
		playerModel.transform.position = new Vector2 (0, 0);*/

		playerModel.AddComponent<playerController> ();
		playerController controlScript = playerModel.GetComponent<playerController> ();
		controlScript.assignPlayer (playerIndex);



		//create a camera to be centered on that object
		playerCamera = new GameObject ("Cam " + playerIndex.ToString ());

		playerCamera.AddComponent<Camera>();
		playerCamera.GetComponent<Camera> ().orthographic = true;
		playerCamera.GetComponent<Camera> ().transform.rotation = new Quaternion (0, 0, 0, 0);
		playerCamera.GetComponent<Camera> ().gameObject.SetActive (false);
		playerCamera.GetComponent<Camera> ().orthographicSize = minZoom;

		//Depending on number of players, create and assign appropriate number of cameras
		switch (players) {
		case 2:
			//Split cameras into top and bottom screen halves
			switch (playerIndex) {
			case 0:
				playerCamera.GetComponent<Camera> ().rect = new Rect (0f, 0.5f, 1f, 0.5f);
				break;
			case 1:
				playerCamera.GetComponent<Camera> ().rect = new Rect (0f, 0f, 1f, 0.5f);
				break;
			}
			break;
		case 3:
			//Split cameras into top, bottom left and bottom right screen portions
			switch (playerIndex) {
			case 0:
				playerCamera.GetComponent<Camera> ().rect = new Rect (0f, 0.5f, 1f, 0.5f);
				break;
			case 1:
				playerCamera.GetComponent<Camera> ().rect = new Rect (0f, 0f, 0.5f, 0.5f);
				break;
			case 2:
				playerCamera.GetComponent<Camera> ().rect = new Rect (0.5f, 0f, 0.5f, 0.5f);
				break;
			}
			break;
		case 4:
			//Split cameras into top left, top right, bottom left and bottom right portions
			switch (playerIndex) {
			case 0:
				playerCamera.GetComponent<Camera> ().rect = new Rect (0f, 0.5f, 0.5f, 0.5f);
				break;
			case 1:
				playerCamera.GetComponent<Camera> ().rect = new Rect (0.5f, 0.5f, 0.5f, 0.5f);
				break;
			case 2:
				playerCamera.GetComponent<Camera> ().rect = new Rect (0f, 0f, 0.5f, 0.5f);
				break;
			case 3:
				playerCamera.GetComponent<Camera> ().rect = new Rect (0.5f, 0f, 0.5f, 0.5f);
				break;

			}
			break;
		default:
			playerCamera.GetComponent<Camera> ().rect = new Rect (1f, 1f, 1f, 1f);
			break;
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		//print ("Collided with: " + col.gameObject);
		//print (col.gameObject.name);
		if (col.gameObject.name == ("DEATHBOX")) {
			Destroy (this.gameObject);
		} else if (col.gameObject.name == ("LEVELTRANSITION")) {
			//print("Changing Level");
			//Level storage generally happens at level generation, doesn't need to happen multiple times
			//mgr.levelManager.GetComponent<Level>().storeLevel(0);

			//print ("Clearing Level: " + col.gameObject.GetComponent<Terrain>().transitionFrom);
			//print ("Creating Level: " + col.gameObject.GetComponent<Terrain>().transitionTo);

			mgr.levelManager.GetComponent<Level>().clearLevelMeshes(col.gameObject.GetComponent<Terrain>().transitionFrom);
			//print ("Level Cleared");
			mgr.levelManager.GetComponent<Level>().loadLevel(col.gameObject.GetComponent<Terrain>().transitionTo);
			//print ("Level Created");

			//print ("Relocating Player " + playerIndex);
			mgr.spawnPlayer (this.gameObject, col.gameObject.GetComponent<Terrain>().transitionTo);
		} 
	}

	void OnCollisionStay2D(Collision2D col) {
		//print ("Stayed with: " + col.gameObject);
		//print ("Collided with: " + col.gameObject.name);
	}

	void OnCollisionExit2D(Collision2D col) {
		
	}

	void OnTriggerEnter2D(Collider2D col) {
		//print ("Collided with: " + col.gameObject.name);
	}
	void OnTriggerStay2D(Collider2D col) {

		/* This works, we can do item pickup/shop interaction here (But I haven't for consistency's sake)
		if (Input.GetButtonDown (X)) {
			if (col.gameObject.GetComponent<Item> () != null) {
				print ("Pick up the object");
			}
		}*/

		if (col.gameObject.GetComponent<Shop> () != null) {
			shoppingAt = col.gameObject;
		}
		if (col.gameObject.GetComponent<Item> () != null) {
			itemPickup = col.gameObject;
		}
		/*if (Input.GetAxis (LeftJoystickX) < joystickDeadZone && Input.GetAxis (LeftJoystickX) > -joystickDeadZone) {
			if (col.gameObject.name == ("shopkeep")) {
				shoppingAt = col.gameObject;
			}
		} else {
			if (shoppingAt != null) {
				shoppingAt.GetComponent<Shop> ().hideShopInventory ();
				shoppingAt = null;
			}
		}*/
	}
	void OnTriggerExit2D(Collider2D col) {
		//print ("Exited with: " + col.gameObject);
		/*if (col.gameObject.name ==("shopkeep")) {
			shoppingAt = null;
		}*/
		if (col.gameObject.GetComponent<Shop> () != null) {
			shoppingAt = null;
		} else {
		//	print ("Not a shop");
		}
	}

	void OnGUI() {
		GUI.Label (new Rect (10, 10, 200, 50), "State: " + this.attackstate);
		GUI.Label (new Rect (10, 60, 200, 50), "Framesleft: " + this.framesleft);
	}
}