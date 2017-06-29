using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private int moveSpeed = 10;
	public int direction = 1;

	public Rigidbody bullet;
	public Player player;
	public Enemy enemy;

	public string wall = "Wall";
	public string cube = "Cube";
	public string p = "Player";
	public string e = "Enemy";

	// Use this for initialization
	void Awake () {
		bullet = GetComponent<Rigidbody>();
		GameObject thePlayer = GameObject.Find("Player");
        Player playerScript = thePlayer.GetComponent<Player>();
		direction = playerScript.direction;
	}

	public void setDir(int dir) {
		direction = dir;
	}


	void OnCollisionEnter(Collision other)
	{
		if (other.transform.gameObject.name == wall || other.transform.gameObject.name == cube)
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

	}
	// Update is called once per frame
	void Update () {
		move();
	}

	void move() {
		bullet.velocity = new Vector3(moveSpeed * direction, 0, 0);

	}
}
