using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private int moveSpeed = 20;
	public int direction = 1;

	public Rigidbody2D bullet;
	public Player player;
	public Enemy enemy;

	public string wall = "Wall";
	public string cube = "Cube";
	public string p = "Player";
	public string e = "Enemy";


	void Awake () {
		bullet = GetComponent<Rigidbody2D>();
		GameObject thePlayer = GameObject.Find("Player");
		Player playerScript = The.player.GetComponent<Player>();
		direction = playerScript.direction;
	}

	public void setDir(int dir) {
		direction = dir;
	}


	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.transform.gameObject.tag == "Wall" || other.transform.gameObject.name == cube)
		{
			Destroy(this.gameObject);	
		}

		if(other.transform.gameObject.name == e) {
			Destroy(this.gameObject);
			The.enemy.hp = The.enemy.hp - 10;
		}
		if(other.transform.gameObject.name == p) {
			Destroy(this.gameObject);
			The.player.hp = The.player.hp - 10;
		}
		if(other.transform.gameObject.name == this.gameObject.name) {
			Destroy(this.gameObject);
		}

	}

	void Update () {
		move();
	}

	void move() {
		bullet.velocity = new Vector2 (moveSpeed * direction, 0);
	}
}
