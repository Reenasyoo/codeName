using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxScrooling : MonoBehaviour {

	public float speed;

	private Transform[] layers;

	// private int leftIndex;
	// private int rightIndex;

	// Use this for initialization
	void Start () {
		//renderer = GetComponent<Renderer>();
		//backgoundSize = 10f;
		// layers = new Transform[transform.childCount];
		// for(int i = 0; i < transform.childCount; i++) {
		// 	layers[i] = transform.GetChild(i);
		// }
		// leftIndex = 0;
		// rightIndex = layers.Length - 1;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 offset = new Vector2(Time.time * speed, 0);
		
		GetComponent<Renderer>().material.mainTextureOffset = offset;
		//scrollLeft();

	}

}
