using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayerInventory : MonoBehaviour {

	//Parent Player
	public GameObject owningPlayer;
	public GameObject gameManager;

	//Inventory UI Components
	public GameObject inventoryCamera;
	public GameObject inventoryCanvas;
	public GameObject backdrop;

	//Player Equipment
	public GameObject helmet;
	public GameObject armour;
	public GameObject weapon;

	//Player Inventory Slots
	public GameObject[,] inventorySlot;
	public GameObject activeInventorySlot;
	public GameObject cursor;
	public int activeInventorySlotX;
	public int activeInventorySlotY;
	public bool selectingShop;

	public string cameraType;
	public Rect setBSize;

	// Use this for initialization
	void Start () {

		gameManager = GameObject.Find ("GameManager");

		//A Single player, half screen
		//B Multiplayer, shared screen but buggy
		//C Shared Camera - Just use GameManager's camera
		cameraType = "D";
		setBSize.x = 10;
		setBSize.y = 12;

		helmet = armour = weapon = null;


		inventoryCamera = new GameObject ();
		inventoryCamera.AddComponent<Camera> ();

		if (cameraType == "A") {
			inventoryCamera.GetComponent<Camera> ().rect = new Rect (0.5f, 0.0f, 0.5f, 1.0f);
		} else if (cameraType == "B") {
			inventoryCamera.GetComponent<Camera> ().rect = new Rect (0.0f, 0.0f, 1.0f, 1.0f);
		} else if (cameraType == "C") {
			inventoryCamera.GetComponent<Camera> ().rect = new Rect (0.0f, 0.0f, 1.0f, 1.0f);
		}

		inventoryCamera.transform.SetParent (this.gameObject.transform);
		inventoryCamera.name = owningPlayer.name + " Inventory Camera";
		//inventoryCamera.GetComponent<Camera> ().fieldOfView = 5;
		inventoryCamera.transform.Translate(new Vector3(0,0,-20));
		inventoryCamera.GetComponent<Camera>().orthographic = true;
		inventoryCamera.GetComponent<Camera>().farClipPlane = 25;
		//inventoryCamera.GetComponent<Camera>().orthographicSize = 1;
		inventoryCamera.GetComponent<Camera>().transform.rotation = new Quaternion(0, 0, 0, 0);
		inventoryCamera.SetActive (false);

		inventoryCanvas = new GameObject ();
		inventoryCanvas.AddComponent<Canvas> ();
		inventoryCanvas.AddComponent<CanvasScaler> ();
		inventoryCanvas.AddComponent<GraphicRaycaster> ();
		inventoryCanvas.GetComponent<Canvas> ().name = owningPlayer.name + "Player1Inventory Canvas";
		//inventoryCanvas.SetActive (false);

		backdrop = new GameObject ();
		backdrop.AddComponent<CanvasRenderer> ();
		backdrop.AddComponent<Image> ();
		backdrop.GetComponent<Image>().name = owningPlayer.name + "Backdrop";
		backdrop.GetComponent<Image> ().sprite = Resources.Load <Sprite>("Sprites/inventoryBackdrop");
		backdrop.transform.SetParent (inventoryCanvas.transform);
		backdrop.transform.localScale = new Vector3 (1,1,1);

		//owningPlayer.GetComponent<Player>().playerCamera.GetComponent<Camera>().pixelWidth/2, 
		//owningPlayer.GetComponent<Player>().playerCamera.GetComponent<Camera>().pixelHeight/2);

		if (cameraType == "A") {

			inventoryCanvas.GetComponent<Canvas> ().renderMode = RenderMode.ScreenSpaceCamera;
			inventoryCanvas.GetComponent<Canvas> ().worldCamera = inventoryCamera.GetComponent<Camera> ();
			backdrop.GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, inventoryCamera.GetComponent<Camera> ().pixelWidth);
			backdrop.GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, inventoryCamera.GetComponent<Camera> ().pixelHeight);
			inventoryCanvas.transform.SetParent (owningPlayer.transform);

		} else if (cameraType == "B") {
			//backdrop.transform.SetParent (owningPlayer.transform);
			inventoryCanvas.GetComponent<Canvas> ().renderMode = RenderMode.ScreenSpaceCamera;
			inventoryCanvas.GetComponent<Canvas> ().worldCamera = inventoryCamera.GetComponent<Camera> ();
			backdrop.GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, setBSize.x);
			backdrop.GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, setBSize.y);
			inventoryCanvas.transform.SetParent (owningPlayer.transform);

			//backdrop.GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, inventoryCamera.GetComponent<Camera> ().pixelWidth/2);
			//backdrop.GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, inventoryCamera.GetComponent<Camera> ().pixelHeight / 2);
			//backdrop.transform.Translate(new Vector2(owningPlayer.GetComponent<BoxCollider2D> ().bounds.extents.x*1.2f, owningPlayer.GetComponent<BoxCollider2D> ().bounds.extents.y*1.2f));
			//backdrop.transform.Translate(new Vector2(backdrop.GetComponent<RectTransform>().rect.size.x, backdrop.GetComponent<RectTransform>().rect.size.y));

			backdrop.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (setBSize.x / 1.25f, setBSize.y / 2);
		} else if (cameraType == "C") {
			inventoryCanvas.GetComponent<Canvas> ().renderMode = RenderMode.ScreenSpaceCamera;
			inventoryCanvas.GetComponent<Canvas> ().worldCamera = gameManager.GetComponent<GameManager> ().sharedCamera.GetComponent<Camera> ();
			//backdrop.transform.SetParent (owningPlayer.transform);
			backdrop.GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, setBSize.x);
			backdrop.GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, setBSize.y);
			inventoryCanvas.transform.SetParent (gameManager.GetComponent<GameManager> ().coreCanvas.GetComponent<Canvas> ().transform);

			//backdrop.GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, inventoryCamera.GetComponent<Camera> ().pixelWidth/2);
			//backdrop.GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, inventoryCamera.GetComponent<Camera> ().pixelHeight / 2);
			//backdrop.transform.Translate(new Vector2(owningPlayer.GetComponent<BoxCollider2D> ().bounds.extents.x*1.2f, owningPlayer.GetComponent<BoxCollider2D> ().bounds.extents.y*1.2f));
			//backdrop.transform.Translate(new Vector2(backdrop.GetComponent<RectTransform>().rect.size.x, backdrop.GetComponent<RectTransform>().rect.size.y));

			backdrop.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (setBSize.x / 1.25f, setBSize.y / 2);
			//backdrop.transform.SetParent (owningPlayer.transform);
		} else if (cameraType == "D") {

			Destroy (backdrop);
			backdrop = new GameObject ();
			inventorySlot = new GameObject[6, 5];


			backdrop.AddComponent<SpriteRenderer> ();
			Sprite backdropSprite = new Sprite ();
			//backdropSprite = Resources.Load<Sprite> ("Sprites/inventoryBackdrop");
			backdrop.GetComponent<SpriteRenderer> ().sprite = backdropSprite;
			//backdrop.GetComponent<SpriteRenderer>().size = new Vector2 (inventorySlot.GetLength(0), inventorySlot.GetLength(1));
			backdrop.transform.SetParent (owningPlayer.transform);
			//backdrop.transform.Translate (backdrop.GetComponent<SpriteRenderer> ().sprite.bounds.size.x*(inventorySlot.GetLength(0)*.1f), backdrop.GetComponent<SpriteRenderer> ().sprite.bounds.size.y*(inventorySlot.GetLength(1)*.1f), 1);
			//backdrop.transform.localScale = new Vector2(inventorySlot.GetLength(0)*.12f, inventorySlot.GetLength(1)*.12f);
			backdrop.name = "Player Inventory Backdrop";
			//backdrop.SetActive (false);
			backdrop.GetComponent<SpriteRenderer> ().enabled = false;
			for (int j = 0; j < inventorySlot.GetLength(1); j++) {
				for (int i = 0; i < inventorySlot.GetLength(0); i++) {
					inventorySlot [i, j] = new GameObject ();
					inventorySlot [i, j].AddComponent<InventorySlot> ();
					inventorySlot [i, j].GetComponent<InventorySlot> ().x = i;
					inventorySlot [i, j].GetComponent<InventorySlot> ().y = j;
					inventorySlot [i, j].transform.SetParent (backdrop.transform);
					inventorySlot [i, j].AddComponent<SpriteRenderer> ();
					inventorySlot [i, j].GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/inventoryFree");
					inventorySlot [i, j].name = "Inventorybit";
					inventorySlot [i, j].transform.Translate (new Vector3 ((inventorySlot[i,j].GetComponent<SpriteRenderer>().sprite.bounds.size.x * i), (inventorySlot[i,j].GetComponent<SpriteRenderer>().sprite.bounds.size.y * j), -1));
					//inventorySlot [i, j].transform.Translate (new Vector2(backdrop.GetComponent<SpriteRenderer>().sprite.bounds.size.x*.28f, backdrop.GetComponent<SpriteRenderer>().sprite.bounds.size.y*.28f));
				}
			}

			activeInventorySlotX = 0;
			//activeInventorySlotY = 0;
			activeInventorySlotY = inventorySlot.GetLength(1)-1;
			activeInventorySlot = inventorySlot [activeInventorySlotX, activeInventorySlotY];


		}
//		backdrop.GetComponent<Image> ().preferredWidth = owningPlayer.GetComponent<Player> ().playerCamera.GetComponent<Camera> ().pixelWidth/2;
//		backdrop.GetComponent<Image> ().preferredHeight = owningPlayer.GetComponent<Player> ().playerCamera.GetComponent<Camera> ().pixelHeight;

//		backdrop.GetComponent<Image> ().SetNativeSize (owningPlayer.GetComponent<Player> ().playerCamera.GetComponent<Camera> ().pixelWidth / 2, owningPlayer.GetComponent<Player> ().playerCamera.GetComponent<Camera> ().pixelHeight);
		//backdrop.transform.localPosition = new Vector2 (inventoryCanvas.transform.position.x, inventoryCanvas.transform.position.y);

		/*newGameButton = new GameObject ();
		newGameButton.AddComponent<CanvasRenderer> ();
		newGameButton.AddComponent<Image>();
		newGameButton.GetComponent<Image> ().sprite = Resources.Load ("defaultimg.png") as Sprite;
		newGameButton.AddComponent<Button> ();
		newGameButton.GetComponent<Button> ().name = "NGButton";
		newGameButton.GetComponent<Button> ().targetGraphic = newGameButton.GetComponent<Image> ();
		newGameButton.transform.SetParent (mainCanvas.transform);
		newGameButton.transform.localPosition = new Vector3 (0, 0, 0);
		newGameButton.transform.localScale = new Vector2 (1, 1);*/

		/*newGametext = new GameObject ();
		newGametext.AddComponent<CanvasRenderer> ();
		newGametext.AddComponent<Text> ();
		newGametext.GetComponent<Text> ().text = "NGText";
		newGametext.GetComponent<Text> ().name = "NGText";
		newGametext.GetComponent<Text> ().color = Color.blue;
		newGametext.transform.SetParent (newGameButton.transform);
		newGametext.transform.localPosition = new Vector3 (0, 0, 0);
		newGametext.transform.localScale = new Vector2 (1, 1);
		newGametext.GetComponent<Text> ().font = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;*/
		//backdrop.SetActive (false);
		backdrop.GetComponent<SpriteRenderer> ().enabled = false;
	}

	// Update is called once per frame
	void Update () {

		/*
		for (int j = 0; j < inventorySlot.GetLength (1); j++) {
			for (int i = 0; i < inventorySlot.GetLength (0); i++) {
			}
		}

		inventorySlot [activeInventorySlotX, activeInventorySlotY].GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/inventorySelect");
*/
		//print (selectingShop + " - [" + activeInventorySlotX + ", " + activeInventorySlotY + "]");
//		print (this.inventoryOpen ());
		if (this.inventoryOpen ()) {
			for (int j = 0; j < inventorySlot.GetLength (1); j++) {
				for (int i = 0; i < inventorySlot.GetLength (0); i++) {
					inventorySlot [i, j].GetComponent<SpriteRenderer> ().enabled = true;
					if (inventorySlot [i, j].GetComponent<InventorySlot> ().occupyingItem != null) {
						inventorySlot [i, j].GetComponent<InventorySlot> ().occupyingItem.GetComponent<SpriteRenderer> ().enabled = true;
					}
					if (inventorySlot [i, j].GetComponent<InventorySlot> ().occupyingItem == null) {
						inventorySlot [i, j].GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/inventoryFree");
					} else {
						inventorySlot [i, j].GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/inventoryOccupied");
					} 
					//if (activeInventorySlot == inventorySlot [i, j]) {
					//	inventorySlot [activeInventorySlotX, activeInventorySlotY].GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/inventorySelect");
					//}
				}
			}
			if (cursor == null) {
				inventorySlot [activeInventorySlotX, activeInventorySlotY].GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/inventorySelect");
			} else {
				for (int i = 0; i < cursor.GetComponent<Item>().hSize; i++){
					for (int j = 0; j < cursor.GetComponent<Item>().vSize; j++){
//						print ((i+activeInventorySlotX).ToString () + ", " + (j+activeInventorySlotY).ToString () + "by item");
						inventorySlot[activeInventorySlotX+i, activeInventorySlotY-j] .GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/inventorySelect");
					}
				}
			}
		} else {
			for (int j = 0; j < inventorySlot.GetLength (1); j++) {
				for (int i = 0; i < inventorySlot.GetLength (0); i++) {
					if (inventorySlot [i, j].GetComponent<InventorySlot> ().occupyingItem != null) {
						inventorySlot [i, j].GetComponent<InventorySlot> ().occupyingItem.GetComponent<SpriteRenderer> ().enabled = false;
					}
					inventorySlot [i, j].GetComponent<SpriteRenderer> ().enabled = false;
				}
			}
		}
	}

	public void toggleInventory() {


		if (cameraType == "A") {
			if (inventoryCamera.activeInHierarchy) {
				inventoryCamera.SetActive (false);
				this.owningPlayer.GetComponent<Player> ().playerCamera.GetComponent<Camera> ().rect = new Rect (0.0f, 0.0f, 1.0f, 1.0f);
			} else {
				inventoryCamera.SetActive (true);
				this.owningPlayer.GetComponent<Player> ().playerCamera.GetComponent<Camera> ().rect = new Rect (0.0f, 0.0f, 0.5f, 1.0f);
			}
		} else if (cameraType == "B") {
			//Inventory Set Type B
			if (inventoryCamera.activeInHierarchy) {
				inventoryCamera.SetActive (false);
				//this.owningPlayer.GetComponent<Player> ().playerCamera.GetComponent<Camera> ().rect = new Rect (0.0f, 0.0f, 1.0f, 1.0f);
			} else {
				inventoryCamera.SetActive (true);
				//this.owningPlayer.GetComponent<Player> ().playerCamera.GetComponent<Camera> ().rect = new Rect (0.0f, 0.0f, 0.5f, 1.0f);
			}
		} else if (cameraType == "C") {
			//Inventory Set Type C - No camera stuff, handled by GameManager camera
			if (backdrop.activeInHierarchy) {
				//backdrop.SetActive (false);
				backdrop.GetComponent<SpriteRenderer> ().enabled = false;

			} else {
				//backdrop.SetActive (true);
				backdrop.GetComponent<SpriteRenderer> ().enabled = true;
				backdrop.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (owningPlayer.transform.position.x, owningPlayer.transform.position.y);
			}
		} else if (cameraType == "D") {


			//for testing purposes
			//inventorySlot [0, inventorySlot.GetLength(1)-2].GetComponent<InventorySlot> ().isEmpty = false;

		//Inventory Set Type C - No camera stuff, handled by GameManager camera
			if (backdrop.GetComponent<SpriteRenderer> ().enabled == true) {
				backdrop.GetComponent<SpriteRenderer> ().enabled = false;
			} else {
				backdrop.GetComponent<SpriteRenderer> ().enabled = true;
				//owningPlayer.GetComponent<Player> ().inventoryManager.GetComponent<PlayerInventory> ().activeInventorySlotX = 0;
				//owningPlayer.GetComponent<Player> ().inventoryManager.GetComponent<PlayerInventory> ().activeInventorySlotY = 0;
				//owningPlayer.GetComponent<Player> ().inventoryManager.GetComponent<PlayerInventory> ().activeInventorySlot = owningPlayer.GetComponent<Player> ().inventoryManager.GetComponent<PlayerInventory> ().inventorySlot [0, 0];

				//backdrop.GetComponent<Plane> ().SetNormalAndPosition (new Vector3 (0, 0, 0), owningPlayer.transform.position);
			}
		}
	}

	public void hideInventory() {
		inventoryCamera.SetActive (false);
		backdrop.GetComponent<SpriteRenderer> ().enabled = false;
	}

	public bool inventoryOpen() {
		return backdrop.GetComponent<SpriteRenderer> ().enabled;
	}

	public void moveInventoryCursorX(int x) {
		/*activeInventorySlotX += x;
		if (activeInventorySlotX >= inventorySlot.GetLength (0))
			activeInventorySlotX = 0;
		else if (activeInventorySlotX < 0) {
			if (owningPlayer.GetComponent<Player> ().shoppingAt == null) { 
				activeInventorySlotX = inventorySlot.GetLength (0) - 1;
			} else {
				activeInventorySlot = owningPlayer.GetComponent<Player> ().shoppingAt.GetComponent<Shop> ().inventorySlot [0, 0];
			}
		}
		activeInventorySlot = inventorySlot [activeInventorySlotX, activeInventorySlotY];*/

		if (!selectingShop) {
			activeInventorySlotX += x;
			if (activeInventorySlotX >= inventorySlot.GetLength (0))
				activeInventorySlotX = 0;
			else if (activeInventorySlotX < 0) {
				print("Shop: " + owningPlayer.GetComponent<Player>().shoppingAt);
				if (owningPlayer.GetComponent<Player> ().shoppingAt == null) {
					activeInventorySlotX = inventorySlot.GetLength (0) - 1;
				} else {
					if (activeInventorySlotY >= owningPlayer.GetComponent<Player> ().shoppingAt.GetComponent<Shop> ().inventorySlot.GetLength (1))
						activeInventorySlotY = owningPlayer.GetComponent<Player> ().shoppingAt.GetComponent<Shop> ().inventorySlot.GetLength (1)-1;
					activeInventorySlot = owningPlayer.GetComponent<Player> ().shoppingAt.GetComponent<Shop> ().inventorySlot [0, activeInventorySlotY];
					selectingShop = true;
					activeInventorySlotX = 0;
				}
			}
		} else {
			activeInventorySlotX -= x;
			if (activeInventorySlotX >= owningPlayer.GetComponent<Player> ().shoppingAt.GetComponent<Shop> ().inventorySlot.GetLength (0)) {
				activeInventorySlotX = 0;
			}
			else if (activeInventorySlotX < 0) {
				if (activeInventorySlotY >= inventorySlot.GetLength (1))
					activeInventorySlotY = inventorySlot.GetLength (1)-1;
				activeInventorySlot = inventorySlot [0, activeInventorySlotY];
				selectingShop = false;
				activeInventorySlotX = 0;
			}
		}

		if (selectingShop) {
			activeInventorySlot = owningPlayer.GetComponent<Player> ().shoppingAt.GetComponent<Shop> ().inventorySlot [activeInventorySlotX, activeInventorySlotY];
		} else {
			activeInventorySlot = inventorySlot [activeInventorySlotX, activeInventorySlotY];
		}

		if (cursor != null) {
			cursor.GetComponent<Item> ().relocateItemToInventory (this.gameObject, activeInventorySlotX, activeInventorySlotY);
		}
	}

	public void moveInventoryCursorY(int y) {

		if (selectingShop) {
			activeInventorySlotY += y;
			if (activeInventorySlotY >= owningPlayer.GetComponent<Player> ().shoppingAt.GetComponent<Shop> ().inventorySlot.GetLength (1))
				activeInventorySlotY = 0;
			else if (activeInventorySlotY < 0) {
				activeInventorySlotY = owningPlayer.GetComponent<Player> ().shoppingAt.GetComponent<Shop> ().inventorySlot.GetLength (1) - 1;
			}
			activeInventorySlot = owningPlayer.GetComponent<Player> ().shoppingAt.GetComponent<Shop> ().inventorySlot [activeInventorySlotX, activeInventorySlotY];
		} else {
			activeInventorySlotY += y;
			if (activeInventorySlotY >= inventorySlot.GetLength (1))
				activeInventorySlotY = 0;
			else if (activeInventorySlotY < 0) {
				activeInventorySlotY = inventorySlot.GetLength (1) - 1;
			}
			activeInventorySlot = inventorySlot [activeInventorySlotX, activeInventorySlotY];

		}

		if (cursor != null) {
			cursor.GetComponent<Item> ().relocateItemToInventory (this.gameObject, activeInventorySlotX, activeInventorySlotY);
		}
	}

	public void action() {
		if (cursor == null) {
			if (this.activeInventorySlot.GetComponent<InventorySlot> ().occupyingItem == null) {
				print ("Empty Slot");
			} else {
				//activeInventorySlot = activeInventorySlot.GetComponent<InventorySlot> ().occupyingItem.GetComponent<Item> ().inventorySlots [0];
				cursor = activeInventorySlot.GetComponent<InventorySlot> ().occupyingItem;
				//print ("Active at: " + activeInventorySlotX.ToString () + ", " + activeInventorySlotY.ToString () + " with size of " + cursor.GetComponent<Item>().hSize.ToString() +", " + cursor.GetComponent<Item>().vSize.ToString());
				//foreach (GameObject slot in activeInventorySlot.GetComponent<InventorySlot>().occupyingItem.GetComponent<Item>().inventorySlots) {
				//	slot.GetComponent<InventorySlot> ().occupyingItem = null;
				//}
				cursor.GetComponent<Item> ().relocateItemToInventory (this.gameObject, activeInventorySlotX, activeInventorySlotY);
				for (int i = 0; i < cursor.GetComponent<Item> ().hSize; i++) {
					for (int j = 0; j < cursor.GetComponent<Item> ().vSize; j++) {
						//print ("pickup loc: " + (activeInventorySlotX).ToString ()+" + "+ i.ToString() + ", " + (activeInventorySlotY).ToString () +" - "+ j.ToString());
						//print ("pickup: " + inventorySlot[activeInventorySlotX+i, activeInventorySlotY-j].GetComponent<InventorySlot>().occupyingItem.name);
						inventorySlot [activeInventorySlotX + i, activeInventorySlotY - j].GetComponent<InventorySlot> ().occupyingItem = null;
					}
				}
				//for (int i = 0; i < activeInventorySlot.GetComponent<InventorySlot>().occupyingItem.GetComponent<Item>().inventorySlots.GetLength(0); i++){
				//	activeInventorySlot.GetComponent<InventorySlot>().occupyingItem.GetComponent<Item>().inventorySlots
				//}
			}
		} else {
			bool toomany = false;
			GameObject underneathCursor = null;
			for (int i = 0; i < cursor.GetComponent<Item>().hSize; i++){
				for (int j = 0; j < cursor.GetComponent<Item> ().vSize; j++) {
					if (inventorySlot [activeInventorySlotX + i, activeInventorySlotY - j].GetComponent<InventorySlot> ().occupyingItem != null) {
						print (inventorySlot[activeInventorySlotX+i, activeInventorySlotY-j].GetComponent<InventorySlot>().occupyingItem.name + " at " + (i+activeInventorySlotX).ToString() + ", " + (activeInventorySlotY-j).ToString());
						if (underneathCursor == null) {
							underneathCursor = inventorySlot [activeInventorySlotX + i, activeInventorySlotY - j].GetComponent<InventorySlot>().occupyingItem;
						} else if (underneathCursor == inventorySlot[activeInventorySlotX+i, activeInventorySlotY-j].GetComponent<InventorySlot>().occupyingItem) {
							print ("Same item");
						} else {
							print ("WARNING: TRYING TO OVERLAP TWO DIFFERENT ITEMS");
							toomany = true;
						}
					} else {
						print ("No item at " + (i + activeInventorySlotX).ToString () + ", " + (activeInventorySlotY-j).ToString ());
					}
				}
			}
			if (!toomany) {


				cursor.GetComponent<Item> ().relocateItemToInventory (this.gameObject, activeInventorySlotX, activeInventorySlotY);

				//remove item currently in inventory
				if (activeInventorySlot.GetComponent<InventorySlot> ().occupyingItem != null) {
					//foreach (GameObject slot in activeInventorySlot.GetComponent<InventorySlot>().occupyingItem.GetComponent<Item>().inventorySlots) {
					//	print ("slot: " + slot.GetComponent<InventorySlot> ().occupyingItem);
					//	slot.GetComponent<InventorySlot> ().occupyingItem = null;
					//}

					int hSize = activeInventorySlot.GetComponent<InventorySlot> ().occupyingItem.GetComponent<Item> ().hSize;
					int vSize = activeInventorySlot.GetComponent<InventorySlot> ().occupyingItem.GetComponent<Item> ().vSize;
					/*for (int i = 0; i < hSize; i++) {
						for (int j = 0; j < vSize; j++) {
							//print ("pickup loc: " + (activeInventorySlotX).ToString ()+" + "+ i.ToString() + ", " + (activeInventorySlotY).ToString () +" - "+ j.ToString());
							//print ("pickup: " + inventorySlot[activeInventorySlotX+i, activeInventorySlotY-j].GetComponent<InventorySlot>().occupyingItem.name);
							inventorySlot [activeInventorySlotX + i, activeInventorySlotY - j].GetComponent<InventorySlot> ().occupyingItem = null;
						}
					}*/
					foreach (GameObject slot in activeInventorySlot.GetComponent<InventorySlot>().occupyingItem.GetComponent<Item>().inventorySlots) {
						print ("active " + activeInventorySlotX + ", " + activeInventorySlotY);
						print ("clear: " + slot.GetComponent<InventorySlot> ().x);
						print ("clear: " + slot.GetComponent<InventorySlot> ().y);
						inventorySlot [slot.GetComponent<InventorySlot>().x, slot.GetComponent<InventorySlot>().y].GetComponent<InventorySlot> ().occupyingItem = null;
						//slot.GetComponent<InventorySlot> ().occupyingItem = null;
					}
				}

				//place item down
				for (int i = 0; i < cursor.GetComponent<Item> ().hSize; i++) {
					for (int j = 0; j < cursor.GetComponent<Item> ().vSize; j++) {
						inventorySlot [activeInventorySlotX + i, activeInventorySlotY - j].GetComponent<InventorySlot> ().occupyingItem = cursor;
					}
				}

				if (underneathCursor!=null) {
					cursor = underneathCursor;
				} else {
					cursor = null;
				}
				//Destroy(underneathCursor)
			}
		}
	}

	public void addItem(GameObject item) {

		bool added = false;

		for(int j = inventorySlot.GetLength(1)-1; j>=0 && added == false; j--) {
			for (int i = 0; i < inventorySlot.GetLength (0) && added == false; i++) {
				if (inventorySlot [i, j].GetComponent<InventorySlot> ().occupyingItem==null) {
					added = placeItemInInventory(item, i, j);
				}
				if (added) {
					item.GetComponent<Item>().relocateItemToInventory(this.gameObject, i, j);
				}
			}
		}
	}

	public bool placeItemInInventory(GameObject item, int rooti, int rootj) {

		bool addable = true;
		for (int x = 0; x < item.GetComponent<Item> ().hSize; x++) {
			for (int y = 0; y < item.GetComponent<Item> ().vSize; y++) {

				//print ("adding to: " + (i + x) + ", " + (j - y));
				//inventorySlot [i + x, j - y].GetComponent<InventorySlot> ().isEmpty = false;
				if (inventorySlot [rooti + x, rootj - y].GetComponent<InventorySlot> ().occupyingItem != null) {
					addable = false;
				}
			}
		}
		if (addable) {
			for (int x = 0; x < item.GetComponent<Item> ().hSize; x++) {
				for (int y = 0; y < item.GetComponent<Item> ().vSize; y++) {
					inventorySlot [rooti + x, rootj - y].GetComponent<InventorySlot> ().occupyingItem = item;
					//print ("::" + (item.GetComponent<Item>().hSize*y+x) + "::");
					item.GetComponent<Item> ().inventorySlots [item.GetComponent<Item>().hSize*y+x] = inventorySlot [rooti + x, rootj - y];
				}
			}
		}
		return addable;
	}
}