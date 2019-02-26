using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	public GameObject[] inventorySlots;
	public int hSize, vSize;

	// Use this for initialization
	void Start () {
		switch (this.gameObject.name) {
		case ("recoveryPotion"):
			hSize = vSize = 1;
			//this.gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprite/recoveryPotion");
			break;
		case ("stick"):
			hSize = 1;
			vSize = 2;
			//this.gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprite/stick");
			break;
		case ("axe"):
			hSize = 2;
			vSize = 3;
			break;
		default: 
			hSize = vSize = 1;
			break;
		}

		inventorySlots = new GameObject[hSize*vSize];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D col) {
		print ("Stick collided with " + col.gameObject.name);
		if(col.gameObject.layer == LayerMask.NameToLayer("Terrain")) {
			Destroy(this.gameObject.GetComponent<Rigidbody2D> ());
			this.gameObject.GetComponent<Collider2D> ().isTrigger = true;
		}
	}

	public void relocateItemToInventory(GameObject inventoryManager, int rooti, int rootj) {

		//Relocate the item to the inventory slot it will start at and move it into place
		this.gameObject.transform.SetPositionAndRotation (
			inventoryManager.GetComponent<PlayerInventory> ().inventorySlot [rooti, rootj].transform.position, 
			inventoryManager.GetComponent<PlayerInventory> ().inventorySlot [rooti, rootj].transform.rotation);
		this.gameObject.transform.Translate(new Vector2(((hSize-1)*inventoryManager.GetComponent<PlayerInventory>().inventorySlot[rooti, rootj].GetComponent<SpriteRenderer>().size.x), 
			(-((vSize-1)*inventoryManager.GetComponent<PlayerInventory>().inventorySlot[rooti, rootj].GetComponent<SpriteRenderer>().size.y)))/2);
		this.gameObject.transform.Translate(new Vector3(0, 0, -1));

		//rescale so the item fits in the inventory
		this.gameObject.transform.localScale = new Vector2 (1, 1);

		//parent item to the inventory
		this.gameObject.transform.SetParent (inventoryManager.GetComponent<PlayerInventory>().backdrop.transform);

		//make sure the collider doesn't exist (so it doesn't randomly trigger with other objects outside the inventory)
		Destroy (gameObject.GetComponent<BoxCollider2D> ());
	}

	public void freeInventorySlots() {
		for (int i = 0; i < hSize; i++) {
			for (int j = 0; j < hSize; j++) {
				inventorySlots [i*j].GetComponent<InventorySlot> ().occupyingItem = null;
			}
		}
	}
}
