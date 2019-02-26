using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*void OnCollisionEnter2D (Collision2D collision) {
		print ("Enter" + collision);
	}

	void OnCollisionStay2D (Collision2D collision) {
		print ("stay" + collision);
	}

	void OnCollisionExit2D (Collision2D collision) {
		print ("leave" + collision);
	}

	void OnTriggerEnter2D (Collider2D collision) {
		print ("TEnter" + collision);
		print (collision.attachedRigidbody.gameObject);
		collision.attachedRigidbody.gameObject.GetComponent<Player> ().ground (true);
	}

	void OnTriggerStay2D (Collider2D collision) {
		print ("Tstay" + collision);
	}

	void OnTriggerExit2D (Collider2D collision) {
		print ("Tleave" + collision);
		collision.attachedRigidbody.gameObject.GetComponent<Player> ().ground (false);
	}*/
}
