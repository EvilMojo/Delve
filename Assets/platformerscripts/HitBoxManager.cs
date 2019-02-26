using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxManager : MonoBehaviour {

	public PolygonCollider2D frame2;
	public PolygonCollider2D frame3;

	private PolygonCollider2D[] colliders;

	private PolygonCollider2D localCollider;

	public enum hitBoxes {

		frame2Box,
		frame3Box,
		clear
	}
	// Use this for initialization
	void Start () {
		colliders = new PolygonCollider2D[]{ frame2, frame3 };

		localCollider = gameObject.AddComponent<PolygonCollider2D> ();
		localCollider.isTrigger = true;
		localCollider.pathCount = 0;
	}

	void OnTriggerEnter2D(Collider2D col) {
		Debug.Log ("Collider Hit Something");
	}

	public void setHitBox(hitBoxes val) {
		if (val != hitBoxes.clear) {
			localCollider.SetPath (0, colliders [(int)val].GetPath (0));
			return;
		}
		localCollider.pathCount = 0;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
