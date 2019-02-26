using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour {

	/****************************************\
	 * Class: Terrain						*
	 * Purpose: Holds data for Terrain and 	*
	 * functionality for generation/removal	*
	\****************************************/ 

	public enum TerrainType
	{
		WALL,
		FLOOR,
		CEILING,
		SPIKES,
		BACKDROP,
		ILLUSION,
		RAMP,
		DEATHBOX,
		LEVELTRANSITION
	}

	public string name;
	public TerrainType type;
	public float width;
	public float height; 
	public Vector2 move;
	public string imagename; 
	public Vector3 rotation;
	public int transitionFrom;
	public int transitionTo;

	public void initialiseData(Terrain.TerrainType type, float width, float height, Vector2 move, 
		string imagename, Vector3 rotation, int from, int to) {
		this.type = type;
		this.width = width;
		this.height = height;
		this.move = move;
		this.imagename = imagename;
		this.rotation = rotation;
		this.transitionFrom = from;
		this.transitionTo = to;
	}

	public void createTerrainMesh() {
		this.gameObject.AddComponent<MeshFilter> ();
		this.gameObject.GetComponent<MeshFilter> ().mesh = createMesh (width, height);
		this.gameObject.GetComponent<MeshFilter> ().transform.Translate (move);

		this.gameObject.AddComponent<MeshRenderer> ();
		this.gameObject.GetComponent<MeshRenderer> ().material.mainTexture = Resources.Load (imagename) as Texture2D;

		this.gameObject.AddComponent<BoxCollider2D> ();
		this.gameObject.SetActive (true);
	}

	public void removeTerrainMesh() {
		/*Destroy(this.gameObject.GetComponent<MeshFilter>().mesh);
		Destroy(this.gameObject.GetComponent<MeshFilter>());
		Destroy(this.gameObject.GetComponent<MeshRenderer>());
		Destroy(this.gameObject.GetComponent<BoxCollider2D>());*/
		this.gameObject.SetActive (false);
	}

	public Mesh createMesh(float width, float height) {
		Mesh m = new Mesh ();
		m.vertices = new Vector3[] {
			new Vector3 (-width, -height, 0.01f),
			new Vector3 (-width, height, 0.01f),
			new Vector3 (width, height, 0.01f),
			new Vector3 (width, -height, 0.01f)
		};
		m.uv = new Vector2[] {
			new Vector2 (0, 0),
			new Vector2 (0, 1),
			new Vector2 (1, 1),
			new Vector2 (1, 0)
		};
		m.triangles = new int[] { 0, 1, 2, 0, 2, 3 };
		m.RecalculateNormals ();

		return m;
	}

}
