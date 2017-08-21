using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour {

	public string name;
	public float cooldown;
	public float maxCooldown = 20f;
	public float ready;
	public float duration = 2f;
	public bool activated;
	public GameObject prefab;
	private bool firing = false;
	private bool pressFire = false;
	public bool rdy = false;

	void Start () {
		
		activated = false;
		prefab = Resources.Load<GameObject>("Prefabs/BulletPrefabSkill");
	}
	void Update () {
		if (rdy && !activated && cooldown == 0f) {
			Debug.Log ("boii");
			activated = true;
			The.player.activeSkill = true;
			cooldown = maxCooldown;
			ready = 0;
			duration = 2f;
		}
		if (activated) {
			duration -= 1f * Time.deltaTime;
			if (Input.GetKey (KeyCode.JoystickButton1) || Input.GetKey (KeyCode.LeftControl)) {
				firing = true;

			} else {
				firing = false; 
				pressFire = false;
			}
		}
		if (firing && !pressFire) {
			Spray ();
			pressFire = true;
		}
		if (duration <= 0f) {
			activated = false;
			
			The.player.activeSkill = false;
			The.player.skill2 = false;
			rdy = false;
			duration = 0f;
			cooldown -= 1f * Time.deltaTime;
			ready += 1f * Time.deltaTime;

		}
		if (cooldown <= 0f) {
			cooldown = 0f;
			ready = maxCooldown;
		}
	}
	void Spray()
	{	
		if (The.player.direction < 0) {
			The.player.pos = new Vector3 (transform.position.x - 1, transform.position.y, transform.position.z);
		} else if (The.player.direction > 0) {
			The.player.pos = new Vector3 (transform.position.x + 1, transform.position.y, transform.position.z);
		}
		GameObject bullet1 = Instantiate(prefab, The.player.pos, Quaternion.Euler(new Vector3(0,0,25)));
		bullet1.GetComponent<Rigidbody2D>().velocity = new Vector3 (20 * The.player.direction, 15, 0);
		GameObject bullet2 = Instantiate(prefab, The.player.pos, Quaternion.Euler(new Vector3(0,0,25)));
		bullet2.GetComponent<Rigidbody2D>().velocity = new Vector3 (20 * The.player.direction, 0, 0);
		GameObject bullet3 = Instantiate(prefab, The.player.pos, Quaternion.Euler(new Vector3(0,0,25)));
		bullet3.GetComponent<Rigidbody2D>().velocity = new Vector3 (20 * The.player.direction, -15, 0);

		Physics2D.IgnoreCollision (bullet1.GetComponent<Collider2D> (), bullet2.GetComponent<Collider2D> ());
		Physics2D.IgnoreCollision (bullet1.GetComponent<Collider2D> (), bullet3.GetComponent<Collider2D	> ());



	}
}
