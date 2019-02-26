using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour {

	public GameObject mainCamera;
	public GameObject mainCanvas;
	public GameObject newGameButton;
	public GameObject newGametext;
	//public EventSystem eventSystem;

	// Use this for initialization
	void Start () {

//		eventSystem = new GameObject ();
//		eventSystem.name = "EventSystem";
//		eventSystem.AddComponent<Stan

		mainCamera = new GameObject ();
		mainCamera.AddComponent<Camera> ();
		mainCamera.AddComponent<GUILayer> ();
		mainCamera.name = "UICamera";



		mainCanvas = new GameObject ();
		mainCanvas.AddComponent<Canvas> ();
		mainCanvas.GetComponent<Canvas> ().renderMode = RenderMode.ScreenSpaceCamera;
		mainCanvas.GetComponent<Canvas> ().worldCamera = mainCamera.GetComponent<Camera>();
		mainCanvas.GetComponent<Canvas> ().name = "NGCanvas";
		mainCanvas.AddComponent<CanvasScaler> ();
		mainCanvas.AddComponent<GraphicRaycaster> ();

		newGameButton = new GameObject ();
		newGameButton.AddComponent<CanvasRenderer> ();
		newGameButton.AddComponent<Image>();
		newGameButton.GetComponent<Image> ().sprite = Resources.Load ("defaultimg.png") as Sprite;
		newGameButton.AddComponent<Button> ();
		newGameButton.GetComponent<Button> ().name = "NGButton";
		newGameButton.GetComponent<Button> ().targetGraphic = newGameButton.GetComponent<Image> ();
		newGameButton.GetComponent<Button> ().onClick.AddListener(StartGame);
		newGameButton.transform.SetParent (mainCanvas.transform);
		newGameButton.transform.localPosition = new Vector3 (0, 0, 0);
		newGameButton.transform.localScale = new Vector2 (1, 1);

		newGametext = new GameObject ();
		newGametext.AddComponent<CanvasRenderer> ();
		newGametext.AddComponent<Text> ();
		newGametext.GetComponent<Text> ().text = "NGText";
		newGametext.GetComponent<Text> ().name = "NGText";
		newGametext.GetComponent<Text> ().color = Color.blue;
		newGametext.transform.SetParent (newGameButton.transform);
		newGametext.transform.localPosition = new Vector3 (0, 0, 0);
		newGametext.transform.localScale = new Vector2 (1, 1);
		newGametext.GetComponent<Text> ().font = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;

	}

	public void StartGame() {
		print ("Pressed");
		SceneManager.LoadScene ("gamescene", LoadSceneMode.Single);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
