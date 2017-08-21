using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour {

	public string name;
	public float cooldown;
	public float maxCooldown = 2f;
	public float ready;
	public float duration;
	public bool activated;
	public string e = "Enemy";
	public bool rdy = false;
	public int power;
	public bool rammed = false;
	public Vector3 originalCameraPosition;
	public float shakeAmount = 0;
	public Camera mainCam;
	public bool dashing;
	public bool activeSkill;
	public bool skill1;


	void Start () {
		activated = false;
		originalCameraPosition = mainCam.transform.position;
		duration = 0.001f;
	}

	void Update () {
		
		if (rdy && !activated && cooldown == 0f) {
			activated = true;
			The.player.activeSkill = true;
			cooldown = maxCooldown;
			ready = 0;
			duration = 0.2f;
		}
		if (rdy && activated) {
			duration -= Time.deltaTime;
			power = 40;
			Dash ();
		}

		if (duration <= 0f) {
			cooldown -= 1f * Time.deltaTime;
			ready += 1f * Time.deltaTime;
			activated = false;
			activeSkill = false;
			rdy = false;
			dashing = false;
			skill1 = false;
			The.player.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f,The.player.GetComponent<Rigidbody2D>().velocity.y);
		}
		if (cooldown <= 0f) {
			cooldown = 0f;
			ready = maxCooldown;
		}
		CheckEnemy ();
		rammed = rammed;
	}
		
	void Dash()
	{
		The.player.GetComponent<Rigidbody2D>().velocity = new Vector2 (power * The.player.direction, The.player.GetComponent<Rigidbody2D>().velocity.y);

	}


	void OnCollisionEnter2D(Collision2D other)
	{
		if (activated) {
			if (other.transform.gameObject.name == e) {
				The.enemy.hp = The.enemy.hp - 20;
				shakeAmount = 0.1f;
				InvokeRepeating ("CameraShake", 0, 0.01f);
				Invoke ("StopShaking", 0.3f);
				rammed = true;
			}
		}
	}

	void CameraShake(){
		if (shakeAmount > 0) {
			float quakeAmount = Random.value * shakeAmount * 2 - shakeAmount;
			Vector3 pp = mainCam.transform.position;
			pp.y += quakeAmount;
			pp.x += quakeAmount;
			mainCam.transform.position = pp;
		}
	}

	void StopShaking() {
		CancelInvoke ("CameraShake");
		mainCam.transform.position = originalCameraPosition;
	}

	void CheckEnemy()
	{
		if (The.enemy.huggingWall) {
			shakeAmount = 0.3f;
			InvokeRepeating ("CameraShake", 0, 0.01f);
			Invoke ("StopShaking", 0.3f);
			The.enemy.hp = The.enemy.hp - 30;
			rammed = false;
			The.enemy.huggingWall = false;
		}
	}
 
}
