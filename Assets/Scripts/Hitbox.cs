using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour {

	public int framesleft;
	public Vector2 launchVelocity;
	public float lethality;
	public float multiplier = 0;
	public int damage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		framesleft--;
		if (framesleft <= 0) {
			terminate();
		}
		damage = 0;
	}

	public void init (int framesleft, Vector2 dimensions, Transform centre, Vector3 translate, bool flip, Vector2 launchVelocity, float lethality, int damage) {

		if (!flip) {
			multiplier = 1.0f;
		} else {
			multiplier = -1.0f;
			launchVelocity.x = -launchVelocity.x;
		}

		translate = new Vector3 (translate.x * multiplier, translate.y, translate.z);

		this.lethality = lethality;
		this.launchVelocity = launchVelocity;
		this.damage = damage;

		print ("ATTACK");
		this.framesleft = framesleft;
		this.gameObject.AddComponent<BoxCollider2D> ();
		this.gameObject.GetComponent<BoxCollider2D> ().transform.localScale = dimensions;
		this.gameObject.GetComponent<BoxCollider2D> ().isTrigger = true;
		this.gameObject.GetComponent<BoxCollider2D> ().transform.position = new Vector2 (centre.position.x, centre.position.y);
		this.gameObject.AddComponent<Rigidbody2D> ();
		this.gameObject.GetComponent<Rigidbody2D> ().gravityScale = 0;
		this.gameObject.GetComponent<Rigidbody2D> ().WakeUp ();
		this.gameObject.AddComponent<SpriteRenderer> ();
		this.gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/red");
		this.gameObject.GetComponent<BoxCollider2D> ().transform.Translate (translate);
		this.gameObject.GetComponent<Rigidbody2D> ().MovePosition (new Vector2 (this.gameObject.GetComponent<Rigidbody2D> ().transform.position.x, this.gameObject.GetComponent<Rigidbody2D> ().transform.position.y));
	
					/*hitbox = new GameObject ();
					hitbox.AddComponent<BoxCollider2D> ();
					hitbox.GetComponent<BoxCollider2D> ().transform.localScale = new Vector2 (.2f, .4f);
					hitbox.GetComponent<BoxCollider2D> ().isTrigger = true;
					hitbox.GetComponent<BoxCollider2D> ().transform.position = new Vector2 (this.gameObject.transform.position.x, this.gameObject.transform.position.y);
					hitbox.AddComponent<Rigidbody2D> ();
					hitbox.GetComponent<Rigidbody2D> ().gravityScale = 0;
					hitbox.GetComponent<Rigidbody2D> ().WakeUp ();
					hitbox.AddComponent<SpriteRenderer> ();
					hitbox.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/red");

					if (facing == "right") {
						hitbox.GetComponent<BoxCollider2D> ().transform.Translate (.5f, 0, 0);
						hitbox.GetComponent<Rigidbody2D> ().MovePosition (new Vector2 (hitbox.GetComponent<Rigidbody2D> ().transform.position.x, hitbox.GetComponent<Rigidbody2D> ().transform.position.y));
					} else if (facing == "left") {
						hitbox.GetComponent<BoxCollider2D> ().transform.Translate (-.5f, 0, 0);
						hitbox.GetComponent<Rigidbody2D> ().MovePosition (new Vector2 (hitbox.GetComponent<Rigidbody2D> ().transform.position.x, hitbox.GetComponent<Rigidbody2D> ().transform.position.y));
					}*/

	}

	public void terminate() {
		Destroy (this.gameObject);
	}
}
