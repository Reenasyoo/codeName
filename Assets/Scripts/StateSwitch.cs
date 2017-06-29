using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StateSwitch : MonoBehaviour {
	

	public void changedScene(string sName) {
		SceneManager.LoadScene(sName);
	}
}
