using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour {

	public Sprite playerIdle;

	// Use this for initialization
	void Start () {
		playerIdle = Resources.Load ("bigtestpng") as Sprite;
	}
	
	// Update is called once per frame
	void Update () {

		//playerIdle = Resources.Load ("bigtestpng") as Sprite;
	}
}
