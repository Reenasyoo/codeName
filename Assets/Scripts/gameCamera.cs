using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameCamera : MonoBehaviour {

	//private Camera camera;
	private Vector3 position;
	private Vector3 center;


	// Use this for initialization
	void Start () {
		//camera = GetComponent<Camera> ();
		
		
		

		//camrea.orthographic = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (The.player != null) {
			Vector3 target = The.player.transform.position;
			Vector3 target2 = The.enemy.transform.position;

			center = ((target2 - target)/2.0f) + target;
          	transform.LookAt(center);

			
		}
	}
}
// 