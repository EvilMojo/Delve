using UnityEngine;
using System.Collections;

public class oldPlayer : MonoBehaviour {

	public float minZoom = 10.0f;

	public string playerName;
	//stats

	public GameObject playerModel;

	//Two potential cameras
	//one camera for whole stage
	//one camera per player for focus
	public GameObject playerCamera;


	private int playerIndex;




	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		playerCamera.GetComponent<Camera>().transform.position = new Vector3(playerModel.transform.position.x, 
														playerModel.transform.position.y,
														playerModel.transform.position.z - 20.0f);

	}

	public void createPlayer(string name, int playerIndex, int players) {
		//set name
		
		//set stats
		//create a player object
		print("creating" + playerName);
		this.playerName = playerName;
		this.playerIndex = playerIndex;
		playerModel = new GameObject();
		playerModel = Instantiate(Resources.Load("Prefabs/playerModel", typeof(GameObject))) as GameObject;
		playerModel.transform.position = new Vector2(0,0);
		playerController controlScript = playerModel.GetComponent<playerController>();
		controlScript.assignPlayer(playerIndex);

		

		//create a camera to be centered on that object
		playerCamera = new GameObject("Cam " + playerIndex.ToString());
		playerCamera.AddComponent(typeof(Camera));
		playerCamera.GetComponent<Camera>().orthographic = true;
		playerCamera.GetComponent<Camera>().transform.rotation = new Quaternion(0, 0, 0, 0);
		playerCamera.GetComponent<Camera>().gameObject.SetActive(false);
		playerCamera.GetComponent<Camera>().orthographicSize=minZoom;

		//Depending on number of players, create and assign appropriate number of cameras
		switch (players) {
		case 0:
			break;
		case 1: 
			break;
		case 2:
			//Split cameras into top and bottom screen halves
			switch (playerIndex) {
				case 0:
					playerCamera.GetComponent<Camera>().rect = new Rect(0f, 0.5f, 1f, 0.5f);
					break;
				case 1:
					playerCamera.GetComponent<Camera>().rect = new Rect(0f, 0f, 1f, 0.5f);
					break;
			}
			break;
		case 3:
			//Split cameras into top, bottom left and bottom right screen portions
			switch (playerIndex) {
				case 0:
					playerCamera.GetComponent<Camera>().rect = new Rect(0f, 0.5f, 1f, 0.5f);
				break;
				case 1:
					playerCamera.GetComponent<Camera>().rect = new Rect(0f, 0f, 0.5f, 0.5f);
				break;
				case 2:
					playerCamera.GetComponent<Camera>().rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
				break;
			}
			break;
		case 4:
			//Split cameras into top left, top right, bottom left and bottom right portions
			switch (playerIndex) {
				case 0:
					playerCamera.GetComponent<Camera>().rect = new Rect(0f, 0.5f, 0.5f, 0.5f);
					break;
				case 1:
					playerCamera.GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
					break;
				case 2:
					playerCamera.GetComponent<Camera>().rect = new Rect(0f, 0f, 0.5f, 0.5f);
					break;
				case 3:
					playerCamera.GetComponent<Camera>().rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
					break;

			}
			break;
		}	
	}

	public void toggleCamera(bool toggle) {
		playerCamera.GetComponent<Camera>().gameObject.SetActive(toggle);
	}

	public float getCameraPosX() {
		return playerCamera.transform.position.x;
	}

	public float getCameraPosY() {
		return playerCamera.transform.position.y;
	}

}
