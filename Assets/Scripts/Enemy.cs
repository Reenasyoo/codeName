using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEditor;
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
		
	}
}
