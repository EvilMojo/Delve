using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public int knock;
	public int health;
	public bool vibrate;
	public bool pauseVibrate;
	public Vector2 force;


	// Use this for initialization
	void Start () {
		health = 12;
		knock = 0;
		vibrate = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.GetComponent<Rigidbody2D> () != null && this.gameObject.GetComponent<Rigidbody2D> ().IsSleeping() ) {
			this.gameObject.GetComponent<Rigidbody2D> ().WakeUp ();
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		//print ("Collided with: " + col.gameObject.name);
		if (health > 0) {
			if (health >= col.gameObject.GetComponent<Hitbox> ().damage) {
				health -= col.gameObject.GetComponent<Hitbox> ().damage;
			} else {
				knock += col.gameObject.GetComponent<Hitbox> ().damage - health;
				health = 0;
			}
		} else {
			knock += col.gameObject.GetComponent<Hitbox> ().damage;
		}
	}

	void OnTriggerStay2D(Collider2D col) {
		if (vibrate) {
			this.gameObject.transform.Translate (new Vector2 (-.1f, 0));
		} else if (!vibrate) {
			this.gameObject.transform.Translate (new Vector2 (.1f, 0));
		}
		vibrate = !vibrate;
		
		//print ("Stayed with: " + col.gameObject);
	}

	void OnTriggerExit2D(Collider2D col) {
		if (health <= 0) {
			this.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2(
				col.gameObject.GetComponent<Hitbox> ().launchVelocity.x * knock/100,  
				col.gameObject.GetComponent<Hitbox> ().launchVelocity.y * knock/100));

			force = new Vector2 (
				col.gameObject.GetComponent<Hitbox> ().launchVelocity.x * knock / 100,  
				col.gameObject.GetComponent<Hitbox> ().launchVelocity.y * knock / 100);

		}
		//print ("Exited with: " + col.gameObject);
	}

	void OnCollisionEnter2D(Collision2D col) {
		//print ("Collided with: " + col.gameObject);
		if (col.gameObject.name == ("DEATHBOX"))
			Destroy (this.gameObject);
	}

	void OnCollisionStay2D(Collision2D col) {
		//print ("Stayed with: " + col.gameObject);
	}

	void OnCollisionExit2D(Collision2D col) {
		//print ("Exited with: " + col.gameObject);
	}
}
