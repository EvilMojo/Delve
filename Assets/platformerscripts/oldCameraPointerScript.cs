/*using UnityEngine;
using System.Collections;

public class oldCameraPointerScript : MonoBehaviour {

	public GameObject manager;
	GameObject[] players;

	GameObject genCam;

	GameObject cameraPointer;
	// Use this for initialization

	float minZoom = 10.0f; 				//Camera will not zoom in past this point
	float upperSplitThreshold = 40.0f;	//Camera will split into 4 cameras above this zoom
	float lowerSplitThreshold = 38.5f;	//4 cameras will merge into 1 camera below this zoom
	float midx = 0, midy = 0; 

	float[] x = new float[4];			//debug
	float[] y = new float[4];			//debug

	void Start () {
		//Get the Game Manager so we can access Player Data
		manager = GameObject.Find("GameManager");
		gameManager managerScript = (gameManager) manager.GetComponent(typeof(gameManager));

		//Assign players into a local array
		players = managerScript.player;


		//Create a main Camera to view all players onscreen in one view
		genCam = new GameObject("genCam");
		genCam.AddComponent(typeof(Camera));
		genCam.GetComponent<Camera>().orthographic = true;
		genCam.GetComponent<Camera>().transform.rotation = new Quaternion(0, 0, 0, 0);
		genCam.GetComponent<Camera>().orthographicSize = minZoom;

		//create a parent to control the position of the main camera and set default position
		cameraPointer = new GameObject("cameraPointer");
		genCam.transform.parent = cameraPointer.transform;
		genCam.transform.position = new Vector3(0,0, cameraPointer.transform.position.z-20.0f);
	}

	void Update () {
		
		int index = 0;									//index for 'for each' loops
		float[,] camPos = new float[players.Length, 2];	//array to track all player cameras
		float highestDistance = 0;						//distance between two furthest cameras

		foreach (GameObject player in players) {
			//For each player on screen, get the x and y co-ordinate of camera
			Player playerScript = (Player) player.GetComponent(typeof(Player));

			camPos[index, 0] = playerScript.getCameraPosX();
			camPos[index, 1] = playerScript.getCameraPosY();

			x[index] = playerScript.getCameraPosX();	//debug
			y[index] = playerScript.getCameraPosY();	//debug

			index++;
			
		}

		//increment through all player cameras and calculate the distance between each
		for(int i=0; i<players.Length; i++) {
			for (int j = i+1; j< players.Length; j++) {
				float x = (camPos[i,0] - camPos[j, 0]);
				float y = (camPos[i,1] - camPos[j, 1]);

				//calculate distance
				float d = Mathf.Sqrt((x*x) + (y*y));

				//if this distance is highest among cameras, set new highest and determine midpoint
				if(highestDistance < d) {
					highestDistance = d;
					midx = (camPos[i,0] + camPos[j, 0])/2;
					//print("x: " + x);
					midy = (camPos[i,1] + camPos[j, 1])/2;
					//print("y: " + y);
				}

				//print("mid: " + midx + ", " + midy);
				//print(i + " " + j + " = " + d + " vs " + highestDistance);
			}
		}

		//Place Main Camera at midpoint
		cameraPointer.transform.position = new Vector3(midx, midy);

		//Set the main camera zoom
		genCam.GetComponent<Camera>().orthographicSize = (highestDistance);

		//If the main camera zoom is too high, (zoomed in) then maintain a set zoom
		if(genCam.GetComponent<Camera>().orthographicSize < minZoom) {
			genCam.GetComponent<Camera>().orthographicSize = minZoom;
		}
		
		//If the camera zooms out too far, deactivate it and activate individual player cameras
		if (genCam.GetComponent<Camera>().orthographicSize > upperSplitThreshold) {
			genCam.GetComponent<Camera>().gameObject.SetActive(false);
			
			foreach (GameObject player in players) {
				Player p = player.GetComponent<Player>();

				p.toggleCamera(true);
			}
		
		//If the camera zooms in to a reasonable level, activate it and deactivate individual player cameras
		} else if (genCam.GetComponent<Camera>().orthographicSize < lowerSplitThreshold) {
			genCam.GetComponent<Camera>().gameObject.SetActive(true);
			foreach (GameObject player in players) {
				Player p = player.GetComponent<Player>();

				p.toggleCamera(false);
			}
		}
		//Adjust Zoom to fit characters
	}

	void OnGUI() {
		GUI.Label(new Rect(0,300,50,200), "1: " + x[0] + ", " + y[0]);
		GUI.Label(new Rect(0,50,50,200), "2: " + x[1] + ", " + y[1]);
		//GUI.Label(new Rect(0,75,50,20), y.ToString());
		GUI.Label(new Rect(0,100,50,200), "3: " + x[2] + ", " + y[2]);
		GUI.Label(new Rect(0,125,50,200), "4: " + x[3] + ", " + y[3]);

		GUI.Label(new Rect(0,150,50,200), "mid: " + midx + ", " + midy);

		//Joystick has a value of -1 to 1
		//-1 left, 1 right
		//-1 up, 1 down

	}
}
*/