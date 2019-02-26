using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBlock : MonoBehaviour {

	public float minBlockSize = 1.0f;
	public bool top, right, bottom, left;
	public float width, height;
	public GameObject levelManager;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void init (bool top, bool right, bool bottom, bool left, float width, float height, GameObject levelManager) {
		this.top = top;
		this.right = right;
		this.bottom = bottom;
		this.left = left;
		this.width = width;
		this.height = height;
		this.levelManager = levelManager;
	}

	public void generateBlock(List<GameObject> currentLevel) {

		List<GameObject> levelComponents = new List<GameObject> ();
		if (top) {
			currentLevel.Add (levelManager.GetComponent<Level>().createTerrain (Terrain.TerrainType.FLOOR, width+minBlockSize, minBlockSize, new Vector2 (0, height), "defaultimg", new Vector3 (0, 0, 0), -1, -1));
		}if (right) {
			currentLevel.Add (levelManager.GetComponent<Level>().createTerrain (Terrain.TerrainType.FLOOR, minBlockSize, height-(minBlockSize), new Vector2 (width, 0), "defaultimg", new Vector3 (0, 0, 0), -1, -1));
		}if (bottom) {
			currentLevel.Add (levelManager.GetComponent<Level>().createTerrain (Terrain.TerrainType.FLOOR, width+minBlockSize, minBlockSize, new Vector2 (0, -height), "defaultimg", new Vector3 (0, 0, 0), -1, -1));
		}if (left) {
			currentLevel.Add (levelManager.GetComponent<Level>().createTerrain (Terrain.TerrainType.FLOOR, minBlockSize, height-(minBlockSize), new Vector2 (-width, 0), "defaultimg", new Vector3 (0, 0, 0), -1, -1));
		}
	}
}
