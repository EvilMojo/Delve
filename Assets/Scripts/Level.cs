using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

	public static List<List<GameObject>> levelStorage = new List<List<GameObject>>();
	public static List<GameObject> currentLevel = new List<GameObject>();
	public GameObject[] shop = new GameObject[3];

	public GameObject mgrObj;
	public GameManager mgr;
	public int wallCount, floorCount, ceilingCount, spikesCount, 
	backdropCount, illusionCount, rampCount, 
	deathboxCount, levelTransitionCount;
	public int levelIndex;

	// Use this for initialization
	void Start () {
		/*for (int i = 0; i <= 5; i++) {
			List<GameObject> list = new List<GameObject> ();
			list.Add (new GameObject ());
			list.Add (new GameObject ());
			levelStorage.Add (list);
		}
		print (levelStorage.GetType() + " - Storage size: " + levelStorage.Count);
		print (levelStorage[0].GetType() + " - List 0 size: " + levelStorage [0].Count);
		print (levelStorage[0][0].GetType());*/
		//currentLevel = new List<GameObject>();
		//levelStorage = new List<List<GameObject>>(2);
		wallCount = floorCount = ceilingCount = spikesCount = backdropCount = illusionCount = rampCount = 0;

		mgrObj = GameObject.Find ("GameManager");
		mgr = mgrObj.GetComponent<GameManager> ();

	}
	
	// Update is called once per frame
	void Update () {
		//print (currentLevel);
		//print (levelStorage);
	}

	//Generates the level
	public void generateLevel(int level){
		/*if (currentLevel == null) {
			currentLevel = new List<GameObject> ();
		}*/


		//Will need to rehash how Terrain is generated, this is too messy.
		//Use Inheritance for different terrain types? Functions+Override?
		//Will need to devise an algorithm that can intelligently generate levels, rooms, and end with level transition 

		print ("Loading Level: " + levelIndex);
		switch (level) {
		case (2):
			currentLevel = new List<GameObject> ();
			currentLevel.Add (createSpawnPoint (0, new Vector3(-3.0f, 10.0f)));
			currentLevel.Add (createTerrain (Terrain.TerrainType.FLOOR, 10.0f, 1.0f, new Vector2 (0, -8.0f), "defaultimg", new Vector3 (0, 0, 0), -1, -1));
			currentLevel.Add (createTerrain (Terrain.TerrainType.FLOOR, 50.0f, 1.0f, new Vector2 (-10.0f, -16.0f), "defaultimg", new Vector3 (0, 0, 0), -1, -1));
			currentLevel.Add (createTerrain (Terrain.TerrainType.LEVELTRANSITION, 1.0f, 1.0f, new Vector2 (0.0f, -5.0f), "arrow", new Vector3 (0, 0, 0), 2, 1));
			levelStorage.Add (currentLevel);
			break;
		case (1):
			currentLevel = new List<GameObject> ();
			currentLevel.Add (createSpawnPoint (0, new Vector3 (-3.0f, 10.0f)));
			List <GameObject> blockList = new List<GameObject> ();

			//generateCathedral (blockList);

			foreach (GameObject block in blockList) {
				block.GetComponent<LevelBlock> ().generateBlock (currentLevel);
			}

			currentLevel.Add (createTerrain (Terrain.TerrainType.FLOOR, 5.0f, 1.0f, new Vector2 (0, -8.0f), "defaultimg", new Vector3 (0, 0, 0), -1, -1));
			currentLevel.Add (createTerrain (Terrain.TerrainType.WALL, 1.0f, 5.0f, new Vector2 (-5.0f, 0), "defaultimg", new Vector3 (0, 0, 0), -1, -1));
			currentLevel.Add (createTerrain (Terrain.TerrainType.RAMP, 3.0f, 8.0f, new Vector2 (5.0f, -5.0f), "defaultimg", new Vector3 (75, 75, 0), -1, -1));
			currentLevel.Add (createTerrain (Terrain.TerrainType.DEATHBOX, 30.0f, 1.0f, new Vector2 (0.0f, -15.0f), "Sprites/red", new Vector3 (0, 0, 0), -1, -1));
			currentLevel.Add (createTerrain (Terrain.TerrainType.LEVELTRANSITION, 1.0f, 1.0f, new Vector2 (1.0f, -2.0f), "arrow", new Vector3 (0, 0, 0), 1, 0));
			currentLevel.Add (createTerrain (Terrain.TerrainType.LEVELTRANSITION, 1.0f, 1.0f, new Vector2 (-5.0f, -10.0f), "arrow", new Vector3 (0, 0, 0), 1, 2));
			levelStorage.Add (currentLevel);
			//currentLevel.Clear();
			break;
		case (0):
			currentLevel = new List<GameObject> ();
			currentLevel.Add (createSpawnPoint (0, new Vector3 (-1.0f, 3.0f)));
			currentLevel.Add (createTerrain (Terrain.TerrainType.FLOOR, 12.0f, 1.0f, new Vector2 (0.0f, -2.0f), "defaultimg", new Vector3 (0, 0, 0), -1, -1));
			currentLevel.Add (createTerrain (Terrain.TerrainType.LEVELTRANSITION, 1.0f, 1.0f, new Vector2 (12.0f, 0.0f), "arrow", new Vector3 (0, 0, 0), 0, 1));
			List<GameObject> lvl = new List<GameObject> (currentLevel);
			levelStorage.Add (lvl);
			//currentLevel.Clear();

			//Add shops (Town will always be static/the same
			shop[0] = new GameObject ();

			shop[0].AddComponent<Shop> ();
			shop[0].GetComponent<Shop> ().init ("shopkeep");

			break;
		}
	}

	public void storeLevel(int index) {
		//levelStorage [index] = currentLevel;
	}

	public void loadLevel(int index) {
		//Level is new, has not been generated before
		//print ("Next Level is: " + levelStorage[index]);
		print (index + " >= " + levelStorage.Count);
		if(index>=levelStorage.Count) {
			print ("new level");
			generateLevel(index);
		} else {
			print ("not new level");
			retrieveLevel(index);
		}

		if (index != 0) {
			foreach (GameObject s in shop) {
				if (s != null) {
					s.SetActive (false);
				}
			}
		}

		//Level has been generated before, retrieve existing level geometry
		//foreach (GameObject level in levelStorage) {
		//	
		//}
	}

	public void retrieveLevel(int index) {
		print ("Retrieving level: " + index);
		foreach (GameObject terrain in levelStorage[index]) {
			terrain.SetActive (true);
			//terrain.GetComponent<Terrain> ().createTerrainMesh();
		}
		if (index == 0) {
			foreach (GameObject s in shop) {
				s.SetActive (true);
			}
		} else {
			foreach (GameObject s in shop) {
				s.SetActive (false);
			}
		}
	}

	public void clearLevelMeshes(int index) {
		//print ("Destroying Meshes");
		//print ("Storage size: " + levelStorage.Count);
		//print ("List 0 size: " + levelStorage [0].Count);
		//print (levelStorage[0][0].GetType());
		print ("Removing meshes from level: " + index);
		foreach (GameObject terrain in levelStorage[index]) {
			terrain.SetActive (false);
			//terrain.GetComponent<Terrain> ().removeTerrainMesh ();
		}
	}

	public void clearLevelData() {
		//foreach (GameObject terrain in currentLevel) {
		//}
	}

	public GameObject createTerrain(Terrain.TerrainType type, float width, float height, Vector2 move, 
		string imagename, Vector3 rotation, int from, int to) {
		GameObject terrain = new GameObject ();

		terrain.AddComponent<Terrain> ();
		terrain.layer = LayerMask.NameToLayer ("Terrain");;
		terrain.GetComponent<Terrain> ().initialiseData (type, width, height, move, imagename, rotation, from, to);
		terrain.GetComponent<Terrain> ().createTerrainMesh ();

		switch (type) {
		case (Terrain.TerrainType.WALL):
			terrain.GetComponent<Terrain>().name = ("WALL " + wallCount.ToString ());
			terrain.AddComponent<Wall> ();
			wallCount++;
			break;
		case (Terrain.TerrainType.FLOOR):
			terrain.name = ("FLOOR " + floorCount.ToString ());
			floorCount++;
			break;
		case (Terrain.TerrainType.RAMP):
			terrain.name = ("RAMP " + rampCount.ToString ());
			terrain.transform.Rotate (rotation);
			rampCount++;
			break;
		case (Terrain.TerrainType.DEATHBOX):
			terrain.name = ("DEATHBOX");
			terrain.AddComponent<DeathBox> ();
			deathboxCount++;
			break;
		case (Terrain.TerrainType.LEVELTRANSITION):
			levelTransitionCount++; //increment before as Town is Level 0
			terrain.name = ("LEVELTRANSITION");
			//terrain.gameObject.GetComponent<BoxCollider2D> ().isTrigger = true;
			break;
		}
		return terrain;
	}

	public void generateCathedral(List<GameObject> blockList) {

		GameObject levelBlock = new GameObject ();
		levelBlock.AddComponent<LevelBlock> ();
		levelBlock.GetComponent<LevelBlock> ().init (true, true, true, true, 5.0f, 2.0f, this.gameObject);
		blockList.Add (levelBlock);

		levelBlock = new GameObject ();
		levelBlock.AddComponent<LevelBlock> ();
		levelBlock.GetComponent<LevelBlock> ().init (true, true, true, true, 30.0f, 10.0f, this.gameObject);
		blockList.Add (levelBlock);
	}

	public GameObject createSpawnPoint(int playerIndex, Vector3 position) {
		GameObject point = new GameObject ();
		point.transform.Translate (position);
		point.name = "Player " + playerIndex + " spawn";
		return point;
	}

	public void spawnPlayer(GameObject player, int destination) {
		//player.transform.SetPositionAndRotation(levelStorage[destination][0].transform.position, )
		player.transform.position = levelStorage[destination][0].transform.position;
	}
}