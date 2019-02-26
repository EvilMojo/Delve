using UnityEngine;
using System.Collections;

public class oldgameManager : MonoBehaviour {


	public GameObject[] player;
	
	// Use this for initialization
	void Start () {
		//Create world
		LoadLevel levelGenerator = new LoadLevel();
		levelGenerator.LoadGameSetup();
		//Create players
		player = new GameObject[4];
		player[0]= new GameObject("Player " + 0);
		player[1]= new GameObject("Player " + 1);
		player[2]= new GameObject("Player " + 2);
		player[3]= new GameObject("Player " + 3);

/*		player[0].AddComponent<Player>();
		player[0].GetComponent<Player>().createPlayer("Bob", 0, player.Length);
		player[1].AddComponent<Player>();
		player[1].GetComponent<Player>().createPlayer("Dennis", 1,  player.Length);
		player[2].AddComponent<Player>();
		player[2].GetComponent<Player>().createPlayer("Blake", 2,  player.Length);
		player[3].AddComponent<Player>();
		player[3].GetComponent<Player>().createPlayer("Syd", 3,  player.Length);*/
		
		GameObject genCam = new GameObject();
		genCam.AddComponent<CameraPointer>();



		//Create Enemies
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI() {
		GUI.Label(new Rect(0,0,50,20), Input.GetAxis("LeftJoystickX").ToString());
		GUI.Label(new Rect(0,25,50,20), Input.GetAxis("LeftJoystickY").ToString());

		//Joystick has a value of -1 to 1
		//-1 left, 1 right
		//-1 up, 1 down

	}
}
