using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour {
	void Update() {
		// Load main game scene when any key is pressed
		if (Input.anyKey) {
			SceneManager.LoadScene(1, LoadSceneMode.Single);
		}
	}
}
