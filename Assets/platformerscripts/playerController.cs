using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	public int playerIndex;

	private Vector3 movementVector;
	private CharacterController control;

	//Stats
	private float move = 4;
	private float jump = 10;
	private int jumpsmax = 1;
	private int jumps;

	//Data
	private string direction = "right";
	private float gravity = 40;


	// Use this for initialization
	void Start () {
		control = GetComponent<CharacterController>();
		control.center = new Vector3 (0.1f, 0, 0);
		control.radius = .35f;
		control.height = 1.5f;
	}
	
	// Update is called once per frame
	void Update () {
		if (playerIndex == 0) {

			//movementVector.x = Input.GetAxis("LeftJoystickX") * move;
			//movementVector.z = Input.GetAxis("LeftJoystickY") * move;

			if (Input.GetAxis ("LeftJoystickX") > 0.2) {
				movementVector.x = 1;
			} else if (Input.GetAxis ("LeftJoystickX") < -0.2) {
				movementVector.x = -1;
			} else {
				movementVector.x = 0;
			}

			if(control.isGrounded) {
				movementVector.y = 0;
				jumps = jumpsmax;
			}

	
	
			if (Input.GetButtonDown("A")) {
				if(jumps!=0) { 
					jumps--;
					movementVector.y = jump;
				}
			}
	
			movementVector.y -= gravity * Time.deltaTime;
			control.Move(movementVector*Time.deltaTime);

		}
	}

	public void assignPlayer(int num) {
		playerIndex = num;
	}
}
