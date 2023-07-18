using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	// Amount of time a bullet stays around for after its been fired
	public float timeToLive = 60;

	// Total time the bullets been alive
	float timeAlive = 0;

	void Update() {
		timeAlive += Time.deltaTime;
		if (timeAlive >= timeToLive) {
			Destroy(gameObject);
		}
	}
}
