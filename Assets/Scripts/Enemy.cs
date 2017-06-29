using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEditor;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {


	internal Rigidbody body;
	private bool movingLeft = false;
	private bool movingRight = false;
	private bool jumping = false;
	private bool firing = false;
	private bool pressFire = false;
	private int jumpHeight = 9;
	public int direction = 0;
	public int hp = 100;
	public GameObject prefab;
	Vector3 pos;

	public string wall = "Cube";
	public string p = "Player";
	public string b = "BulletPrefabClone";

	float xAxis;
	//float yAxis = 0f;

	private Animator anim;
	private SpriteRenderer sprRend;

	// Use this for initialization
	void Awake () {
		body = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		sprRend = GetComponent<SpriteRenderer>();
		The.enemy = this;
		prefab = Resources.Load<GameObject>("Prefabs/BulletPrefab");
		direction = 1;

	}

	void OnCollisionEnter(Collision other) {

		if(other.transform.gameObject.name == wall || other.transform.gameObject.name == p)
		{
			jumping = false;
		}

	}
		// Update is called once per frame
	void Update () {
		anim.SetTrigger("Idle");
	

		if (Input.GetKey(KeyCode.A)) {
			transform.Translate(Vector3.left * 5 * Time.deltaTime);
			direction = -1;
			anim.SetTrigger("Walk");
			sprRend.flipX = true;

		}
		if (Input.GetKey (KeyCode.D)) {
			transform.Translate(Vector3.right * 5 * Time.deltaTime);
			direction = 1;
			anim.SetTrigger("Walk");
			sprRend.flipX = false;
		}

		if (Input.GetKeyDown(KeyCode.Space)&& !jumping ) {
			body.AddForce (0, jumpHeight, 0, ForceMode.Impulse);
			jumping = true;

			
		} 
		if (jumping) {
			anim.SetTrigger("Jump");
		}

		if (Input.GetKey (KeyCode.LeftControl)) {
			firing = true;

		} else {
			firing = false; 
			pressFire = false;
		}

		if (firing && !pressFire) {

			if (direction < 0) {
				pos = new Vector3 (transform.position.x - 1, transform.position.y, transform.position.z);
			} else if (direction > 0) {
				pos = new Vector3 (transform.position.x + 1, transform.position.y, transform.position.z);
			}

			GameObject clone = Instantiate(prefab, pos, Quaternion.identity);
			if(clone != null) {
				clone.GetComponent<Bullet>().direction = direction;
			}
			pressFire = true;
		} 

		if (The.player.hp == 0)
		{
			SceneManager.LoadScene("End");
		}
	}


}

/*
		xAxis = Input.GetAxis ("Horizontal");

		// yAxis = Input.GetAxis ("Vertical");


		if (xAxis <= -0.1f) {
			movingLeft = true;

		} else {
			movingLeft = false;
		} 

		if (xAxis > 0) {
			movingRight = true;

		} else {
			movingRight = false;
		}

		if (Input.GetKey(KeyCode.A) || movingLeft) {
			transform.Translate(Vector3.left * 5 * Time.deltaTime);
			direction = -1;
			anim.SetTrigger("Walk");
			sprRend.flipX = true;

		}
		if (Input.GetKey (KeyCode.D) || movingRight) {
			transform.Translate(Vector3.right * 5 * Time.deltaTime);
			direction = 1;
			anim.SetTrigger("Walk");
			sprRend.flipX = false;
		}

		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.JoystickButton0)) && !jumping ) {
			body.AddForce (0, jumpHeight, 0, ForceMode.Impulse);
			jumping = true;

			
		} 
		if (jumping) {
			anim.SetTrigger("Jump");
		}

		if (Input.GetKey (KeyCode.LeftControl) || Input.GetKey (KeyCode.Joystick1Button1)) {
			firing = true;

		} else {
			firing = false; 
			pressFire = false;
		}
 */