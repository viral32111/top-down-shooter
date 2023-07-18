using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	// Hero gameobject
	public GameObject Hero;

	// Runs every frame
	void Update() {
		// Set camera position to player's X and Z coordonates, but don't change the Y (height)
		transform.position = new Vector3(Hero.transform.position.x, 13.5f, Hero.transform.position.z);
	}
}
