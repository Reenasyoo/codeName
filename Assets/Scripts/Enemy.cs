/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {

	public int hp = 100;
	public GameObject prefab;
	public bool huggingWall = false;
	private Animator anim;

	[Header("Movement")]
	public float moveSpeed;
	public float jumpHeight;
	private Rigidbody2D rb;
	private SpriteRenderer sprRend;
	public int direction;
	public bool jumping;

	[Header("Ground Check")]
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	private bool grounded;


	void Start() {
		The.enemy = this;
		rb = GetComponent<Rigidbody2D> ();
		prefab = Resources.Load<GameObject>("Prefabs/BulletPrefab");
		direction = -1;
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.transform.gameObject.tag == "Wall" && The.player.GetComponent<Charge>().rammed) {
			huggingWall = true;
		} else {
			huggingWall = false;
		}
	}

	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);
	}

	void Update()
	{
		
		EnemyAI ();
	}

	void EnemyAI()
	{
		//Debug.DrawRay (ePos.position, pPos.position - ePos.position, Color.red, 3, true);

	}
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {
	
	[Header("Movement")]
	public float moveSpeed;
	public float jumpHeight;
	private Rigidbody2D rb;
	private SpriteRenderer sprRend;
	public int direction;
	public bool jumping;

	[Header("Ground Check")]
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	private bool grounded;

	private Animator anim;

	[Header("Other Stuff")]
	public bool firing = false;
	public bool pressFire = false;
	public Vector3 pos;
	public bool activeSkill;
	// public bool dashing = false;
	public GameObject prefab;
	public int hp = 100;
	public string b = "BulletPrefabClone";

	[Header("Skills")]
	private bool skill1 = false;
	public bool huggingWall = false;
	private bool skill2 = false;


	void Start() {
		The.enemy = this;
		rb = GetComponent<Rigidbody2D> ();
		sprRend = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
		prefab = Resources.Load<GameObject>("Prefabs/BulletPrefab");
		direction = 1;
	}

	void FixedUpdate() {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);
		if (grounded && Input.GetAxisRaw ("HorizontalP2") == 0f) {
			anim.SetTrigger ("Idle"); 
		}
		if (grounded) {
			jumping = false;
		}
		if (!The.enemy.GetComponent<Charge> ().dashing) {
			if (Input.GetAxisRaw ("HorizontalP2") > 0.1f) {
				sprRend.flipX = false;
				direction = 1;
				rb.velocity = new Vector2 (moveSpeed, rb.velocity.y);
				anim.SetTrigger ("Walk");
			} else if (Input.GetAxisRaw ("HorizontalP2") < -0.1f) {
				sprRend.flipX = true;
				direction = -1;
				rb.velocity = new Vector2 (-moveSpeed, rb.velocity.y);
				anim.SetTrigger ("Walk");
			}
		}

		if (Config.SPEC12 || Input.GetKey (KeyCode.Q)) {
			skill1 = true;
			The.enemy.GetComponent<Charge> ().rdy = skill1;
			The.enemy.GetComponent<Charge> ().dashing = true;
		}
		if (The.enemy.GetComponent<Charge> ().duration <= 0 ) {
			The.enemy.GetComponent<Charge> ().dashing = false;
		}
		// if (Config.SPEC22 || Input.GetKey (KeyCode.E)) {
		// 	skill2 = true;
		// 	The.player.GetComponent<Shotgun> ().rdy = skill2;
		// }

		if (Config.JUMP2 && grounded && !jumping) {
			rb.velocity = new Vector2 (rb.velocity.x, jumpHeight);
			jumping = true;
			anim.SetTrigger ("Jump");
		}
		if (Config.SHOOT2 || Input.GetKey(KeyCode.LeftControl)) {
			firing = true;	
		} else {
			firing = false; 
			pressFire = false;
		}
		if (firing && !pressFire && !activeSkill) {
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
	}
	void OnCollisionEnter2D(Collision2D other) {
		if(other.transform.gameObject.name == b) {
			this.hp =  this.hp - 10;
		}
		if (other.transform.gameObject.tag == "Wall" && The.player.GetComponent<Charge>().rammed) {
			huggingWall = true;
		} else {
			huggingWall = false;
		}

	}
}
//
//	internal Rigidbody body;
//	private bool movingLeft = false;
//	private bool movingRight = false;
//	public bool jumping = false;
//	public bool firing = false;
//	public bool pressFire = false;
//	private int jumpHeight = 9;
//	public int direction = 0;
//	public int hp = 60;
//	public GameObject prefab;
//	public Vector3 pos;
//	public bool shotgun = false;
//	public string wall = "Cube";
//	public string p = "Player";
//	public string b = "BulletPrefabClone";
//
//	public float moveSpeed = 5f;
//	public bool activeSkill = false;
//	private Animator anim;
//	private SpriteRenderer sprRend;
//
//	// Use this for initialization
//	void Awake () {
//		body = this.GetComponent<Rigidbody> ();
//		anim = GetComponent<Animator> ();
//		sprRend = GetComponent<SpriteRenderer>();
//		The.player = this;
//		prefab = Resources.Load<GameObject>("Prefabs/BulletPrefab");
//		
//		direction = 1;
//
//
//
//	}
//
//	void changeJump() {
//		this.jumping = false;
//	}
//	void OnCollisionEnter(Collision other) {
//
//
//		if(other.transform.gameObject.name == b) {
//			this.hp =  this.hp - 10;
//		}
//
//	}
//
//	public void endRep() {
//			CancelInvoke();
//	}
//
//	// Update is called once per frame
//	void Update () {
//		
//		anim.SetTrigger("Idle");
//	
//		if (Input.GetAxis ("Horizontal") > 0.5f || Input.GetAxis ("Horizontal") < -0.5f)
//		{	
//			this.transform.Translate (Vector2.right * Input.GetAxis ("Horizontal") * moveSpeed * Time.deltaTime);
//			anim.SetTrigger("Walk");
//
//			if(Input.GetAxisRaw ("Horizontal") > 0.5f) {
//				sprRend.flipX = false;
//				direction = 1;
//			} 
//			else if(Input.GetAxisRaw ("Horizontal") < -0.5f) {
//				sprRend.flipX = true;
//				direction = -1;
//			}
//		}
//			
//		if ((Input.GetKey(KeyCode.JoystickButton0) || Input.GetKey(KeyCode.JoystickButton14)) && !jumping ) { 
//			body.AddForce (0, jumpHeight, 0, ForceMode.Impulse);
//			jumping = true;
//
//			
//		} 
//		if (jumping) {
//			anim.SetTrigger("Jump");
//		}
//
//		if (Input.GetKey (KeyCode.Joystick1Button1) || Input.GetKey (KeyCode.Joystick1Button17)) {
//			firing = true;
//
//		} else {
//			firing = false; 
//			pressFire = false;
//		}
//
//		if (firing && !pressFire && !activeSkill) {
//
//			if (direction < 0) {
//				pos = new Vector3 (transform.position.x - 1, transform.position.y, transform.position.z);
//			} else if (direction > 0) {
//				pos = new Vector3 (transform.position.x + 1, transform.position.y, transform.position.z);
//			}
//			
//			GameObject clone = Instantiate(prefab, pos, Quaternion.identity);
//			if(clone != null) {
//				clone.GetComponent<Bullet>().direction = direction;
//			}
//			pressFire = true;
//		} 
////
////		if (The.enemy.hp == 0)
////		{
////			SceneManager.LoadScene("End");
////		}
//	}
//
//
//
//}