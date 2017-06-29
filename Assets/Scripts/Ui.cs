using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui : MonoBehaviour {

	private GameObject gui;
	private GameObject guiImage;
	// Use this for initialization
	void Start () {
		gui = new GameObject();
		gui.name = "canvas";
		gui.AddComponent<Canvas>();
		gui.GetComponent<Canvas> ().renderMode = RenderMode.ScreenSpaceOverlay;
		gui.AddComponent<CanvasScaler> ();
		gui.AddComponent<GraphicRaycaster> ();
		//gui.AddComponent<Image> ();
		
		guiImage = new GameObject();
		guiImage.name = "image";
		guiImage.AddComponent<CanvasRenderer> ();
		//guiImage.AddComponent<Transform> ();
		guiImage.AddComponent<Image>();
		guiImage.GetComponent<Image>().color = Color.black;
		
		
		//Transform guiTrasform = guiImage.AddComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
