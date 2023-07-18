using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Void : MonoBehaviour {
	// Runs whenever another collider hits it
	void OnCollisionEnter(Collision collision) {
		// Reset the collided object's position back to the middle of the world
		collision.gameObject.transform.position = new Vector3(0, 0.5f, 0); 
	}
}
