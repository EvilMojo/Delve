using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

	public GameObject[] playerObjects;
	public GameObject player;
	//GameObject spriteManager;
	public GameObject dummy;
	public GameObject stick, potion, axe;
	public GameObject levelManager;
	public string cameraSetting;
	public GameObject sharedCamera;
	public GameObject coreCanvas;


	// Use this for initialization
	void Start () {

		//spriteManager = new GameObject();
		//spriteManager.AddComponent<SpriteManager> ();
		//spriteManager.name = "SpriteManager";

		cameraSetting = "shared";

		int x = 2;

		//generate level
		levelManager = new GameObject();
		levelManager.name = "LevelManager";
		levelManager.AddComponent<Level>();
		levelManager.GetComponent<Level>().generateLevel(0);
		//levelManager.GetComponent<Level>().generateLevel("demo");
		//levelManager.GetComponent<Level>().generateLevel("town");
		//levelManager.GetComponent<Level> ().storeLevel (0);

		if (cameraSetting == "shared") {
			
			sharedCamera = new GameObject ();
			sharedCamera.AddComponent<Camera> ();
			sharedCamera.AddComponent<CameraPointer> ();
			sharedCamera.GetComponent<Camera>().orthographic = true;
			sharedCamera.GetComponent<Camera>().orthographicSize = 8;
			sharedCamera.transform.position = new Vector3(0,0, -20);
			sharedCamera.transform.SetParent (this.gameObject.transform);
			sharedCamera.name = "Shared Camera";

			coreCanvas = new GameObject ();
			coreCanvas.AddComponent<Canvas> ();
			coreCanvas.AddComponent<CanvasScaler> ();
			coreCanvas.AddComponent<GraphicRaycaster> ();
			coreCanvas.GetComponent<Canvas> ().name = "Core Canvas";
			coreCanvas.transform.SetParent (sharedCamera.transform);
		}
		//calculate start/end positions

		//create new gameObjects
		playerObjects = new GameObject[x];

		//attach player scripts to gameObjects

		//place players at start
		for (int i = 0; i < playerObjects.GetLength(0); i++) {
			playerObjects[i] = new GameObject();
			//playerObjects[i].layer = LayerMask.NameToLayer ("Player");
			playerObjects[i].name = "Player " + i;
			playerObjects[i].AddComponent<Player>();
			playerObjects[i].GetComponent<Player> ().playerIndex = i;
			playerObjects[i].GetComponent<Player> ().init (playerObjects[i]);
			playerObjects[i].layer = LayerMask.NameToLayer ("Player");
			//print (playerObjects [i].GetComponent<Player> ());
		}


		//print(player.GetComponent<Player> ().name);
		//player.GetComponent<Player> ().createPlayer ("Player", 0, 0);

		//place enemies

		/*if (player == null) {
			player = new GameObject ();
			player.name = "Player";
			player.AddComponent<Player>();
			player.GetComponent<Player>().init(player.gameObject);
		}*/

		if (stick == null) {
			stick = new GameObject ();
			stick.name = "stick";
			stick.layer = LayerMask.NameToLayer("Item");

			stick.AddComponent<SpriteRenderer>();
			stick.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/stick");

			stick.AddComponent<BoxCollider2D> ();
			stick.AddComponent<Rigidbody2D> ();


			stick.AddComponent<Item> ();
			stick.transform.Translate(new Vector2(0, 0));
			stick.transform.localScale = stick.transform.localScale*.5f;

		}


		if (potion == null) {
			GameObject potion = new GameObject ();
			potion.name = "recoveryPotion";
			potion.layer = LayerMask.NameToLayer ("Item");

			potion.AddComponent<SpriteRenderer> ();
			potion.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/recoveryPotion");

			potion.AddComponent<BoxCollider2D> ();
			potion.AddComponent<Rigidbody2D> ();


			potion.AddComponent<Item> ();
			potion.transform.Translate(new Vector2(3, 0));
			potion.transform.localScale = potion.transform.localScale*.5f;
		}

		if (axe == null) {
			GameObject axe = new GameObject ();
			axe.name = "axe";
			axe.layer = LayerMask.NameToLayer ("Item");

			axe.AddComponent<SpriteRenderer> ();
			axe.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/axe");

			axe.AddComponent<BoxCollider2D> ();
			axe.AddComponent<Rigidbody2D> ();


			axe.AddComponent<Item> ();
			axe.transform.Translate(new Vector2(5, 0));
			axe.transform.localScale = axe.transform.localScale*.5f;
		}


		if (dummy == null) {
			dummy = new GameObject();
			dummy.name = "Dummy";
			dummy.layer = LayerMask.NameToLayer ("Player");

			dummy.AddComponent<SpriteRenderer> ();

			Sprite spr = Resources.Load<Sprite>("Sprites/dummy") as Sprite;
			dummy.GetComponent<SpriteRenderer> ().sprite = (Sprite) spr;

			dummy.AddComponent<BoxCollider2D>();
			dummy.AddComponent<Rigidbody2D>();
			dummy.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;
			dummy.GetComponent<Rigidbody2D> ().freezeRotation = true;

			dummy.AddComponent<Enemy> ();

			//playerBody.AddComponent<Player> ();


			dummy.transform.Translate(new Vector2(2, 0));
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void spawnPlayer (GameObject player, int destination) {
		levelManager.GetComponent<Level>().spawnPlayer (player, destination);
	}

	void OnGUI() {
		//GUI.Label(new Rect(0,0,50,20), Input.GetAxis("LeftJoystickX").ToString());
		//GUI.Label(new Rect(0,25,50,20), Input.GetAxis("LeftJoystickY").ToString());

		//Joystick has a value of -1 to 1
		//-1 left, 1 right
		//-1 up, 1 down

	}
}
