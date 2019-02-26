using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour {

	public GameObject occupyingItem;
	public int x, y;
	//public bool isEmpty;

	// Use this for initialization
	void Start () {
		this.occupyingItem = null;
		//this.isEmpty = true;
		//true==null
	}


}
