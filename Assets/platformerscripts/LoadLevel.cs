using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class LoadLevel : MonoBehaviour {

	private string gameSetup = Application.dataPath + "/Resources/Levels/gameSetup.txt";
	private string tutorial = Application.dataPath + "/Resources/Levels/tutorial.txt";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadGameSetup () {

		string[] map = new string [File.ReadAllLines(gameSetup).Length];

		map = readMapFile(gameSetup);
//		row = getRows(map);

		for (int i = 0; i<map.Length; i++) {
			print(map[i]);
			string[] row = map[i].Split(' ');
			for (int j = 0; j<row.Length; j++) {
				if(row[j] == "block") {
					createBlock(i, j /*texture*/);
				}
			}
		}
	}

	public void LoadTutorial () {
		readMapFile(tutorial);
	}

	public string[] readMapFile(string file) {

		int i = File.ReadAllLines(file).Length-1;
		StreamReader input = new StreamReader(file);

		string[] map = new string[File.ReadAllLines(file).Length];

		while(!input.EndOfStream) {
			string line = input.ReadLine();
			map[i] = line;
			i--;
		}
		return map;
	}


	public void createBlock(int i, int j) {
	
		GameObject tile = Instantiate(Resources.Load("Prefabs/level/block", typeof(GameObject))) as GameObject;
		Transform gDef = tile.transform.FindChild("group_Default");
		GameObject child = gDef.gameObject;

		Mesh mesh = gDef.GetComponent<MeshFilter>().mesh;
		Bounds bounds = mesh.bounds;

		tile.transform.Translate(j*bounds.size.x, i*bounds.size.y, 0*bounds.size.z);

		BoxCollider collider = child.AddComponent<BoxCollider>() as BoxCollider;
	}
}