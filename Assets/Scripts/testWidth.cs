using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testWidth : MonoBehaviour {

	public GameObject chP;

	// Use this for initialization
	void Start () {
		RectTransform chPRT = GetComponent<RectTransform>();
		Debug.Log(chPRT.rect.width);
		Debug.Log(chPRT.rect.height);
		chP.GetComponent<GridLayoutGroup> ().cellSize = new Vector2(chPRT.rect.width/4, chPRT.rect.height/3);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
