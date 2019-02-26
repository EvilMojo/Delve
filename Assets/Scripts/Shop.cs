using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

	GameObject backdrop;
	GameObject servingPlayer;

	public GameObject[,] inventorySlot;
	//public GameObject activeInventorySlot;
	private int activeInventorySlotX;
	private int activeInventorySlotY;

	// Use this for initialization
	void Start () {

		backdrop = new GameObject ();
		inventorySlot = new GameObject[3, 3];
		//activeInventorySlot = null;


		this.gameObject.transform.Translate (new Vector3 (-10, -.4f, 0));
		this.gameObject.AddComponent<BoxCollider2D> ();
		this.gameObject.GetComponent<BoxCollider2D> ().isTrigger = true;

		for (int j = 0; j < inventorySlot.GetLength(1); j++) {
			for (int i = 0; i < inventorySlot.GetLength(0); i++) {
				inventorySlot [i, j] = new GameObject ();
				inventorySlot [i, j].transform.SetParent (backdrop.transform);
				inventorySlot [i, j].AddComponent<SpriteRenderer> ();
				inventorySlot [i, j].GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/inventoryFree");
				inventorySlot [i, j].name = "Inventorybit";
				inventorySlot [i, j].transform.Translate (new Vector3 ((inventorySlot[i,j].GetComponent<SpriteRenderer>().sprite.bounds.size.x * i), (inventorySlot[i,j].GetComponent<SpriteRenderer>().sprite.bounds.size.y * j), -1));
				//inventorySlot [i, j].transform.Translate (new Vector2(backdrop.GetComponent<SpriteRenderer>().sprite.bounds.size.x*.28f, backdrop.GetComponent<SpriteRenderer>().sprite.bounds.size.y*.28f));
			}
		}

		backdrop.AddComponent<SpriteRenderer> ();
		backdrop.name = "Shop Backdrop";
		//backdrop.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite>("Sprites/inventoryBackdrop");
		backdrop.transform.SetParent (this.gameObject.transform);
		//backdrop.transform.localScale = new Vector3 (1,1,1);
		backdrop.transform.Translate (this.gameObject.transform.position);
		backdrop.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
		for (int j = 0; j < inventorySlot.GetLength (1); j++) {
			for (int i = 0; i < inventorySlot.GetLength (0); i++) {
				inventorySlot [i, j].GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/inventoryFree");
				if(servingPlayer!=null)
				if (servingPlayer.GetComponent<Player> ().inventoryManager.GetComponent<PlayerInventory> ().activeInventorySlot == inventorySlot [i, j]) {
					inventorySlot [servingPlayer.GetComponent<Player> ().
						inventoryManager.GetComponent<PlayerInventory> ().activeInventorySlotX, 
						servingPlayer.GetComponent<Player> ().inventoryManager.GetComponent<PlayerInventory> ().activeInventorySlotY].
						GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/inventorySelect");
				}
			}
		}
	}

	public void init(string name) {

		this.gameObject.AddComponent<SpriteRenderer> ();
		this.gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/"+name);
		this.gameObject.name = name;
	}

	public void displayShopInventory(GameObject player) {
		if (backdrop.activeInHierarchy) {
			backdrop.SetActive (false);
		} else {

			servingPlayer = player;
			//player.GetComponent<Player> ().setShop (this.gameObject);

			backdrop.SetActive (true);

			for (int j = 0; j < inventorySlot.GetLength (1); j++) {
				for (int i = 0; i < inventorySlot.GetLength (0); i++) {
					inventorySlot [i, j].transform.SetPositionAndRotation (servingPlayer.GetComponent<Player> ().inventoryManager.GetComponent<PlayerInventory> ().inventorySlot [0, j].transform.position,
						servingPlayer.GetComponent<Player> ().inventoryManager.GetComponent<PlayerInventory> ().inventorySlot [0, j].transform.rotation);
					inventorySlot [i, j].transform.Translate(new Vector2(-inventorySlot[i,j].GetComponent<SpriteRenderer>().sprite.bounds.size.x*i-inventorySlot[i,j].GetComponent<SpriteRenderer>().sprite.bounds.size.x, 0));
				}
			}

			/*backdrop.transform.SetPositionAndRotation (player.GetComponent<Player>().inventoryManager.GetComponent<PlayerInventory> ().backdrop.transform.position, 
				player.GetComponent<Player>().inventoryManager.GetComponent<PlayerInventory> ().backdrop.transform.rotation);
			backdrop.transform.Translate(new Vector2(-backdrop.GetComponent<SpriteRenderer>().sprite.bounds.size.x, 0));
			*/

		}
	}

	public void hideShopInventory() {
		backdrop.SetActive (false);

		if (servingPlayer.GetComponent<Player> ().inventoryManager.GetComponent<PlayerInventory> ().selectingShop) {
			servingPlayer.GetComponent<Player> ().inventoryManager.GetComponent<PlayerInventory> ().activeInventorySlot = servingPlayer.GetComponent<Player> ().inventoryManager.GetComponent<PlayerInventory> ().inventorySlot [0, 0];
			servingPlayer.GetComponent<Player> ().inventoryManager.GetComponent<PlayerInventory> ().activeInventorySlotX = 0;
			servingPlayer.GetComponent<Player> ().inventoryManager.GetComponent<PlayerInventory> ().activeInventorySlotY = 0;
			servingPlayer.GetComponent<Player> ().inventoryManager.GetComponent<PlayerInventory> ().selectingShop = false;
		}
		servingPlayer = null;
	}

	public bool shopOpen() {
		return backdrop.activeInHierarchy;
	}
}
