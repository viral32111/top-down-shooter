using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	// Hero to target
	GameObject hero;

	private Rigidbody myRigidbody;

	//private Vector3 movementForce = new Vector3(0, 0, 15);
	//private int maxVelocity = 2;
	private Vector3 targetVelocity = new Vector3(0, 0, 12);
	private float maxAccel = 12.0f;

	// Runs when the object is initalised
	void Start() {
		hero = GameObject.Find("/Hero"); // Set the hero to the "Hero" object in the hierarchy
		myRigidbody = GetComponent<Rigidbody>();
	}

	void FixedUpdate() { // Update()
		// Make the enemy look at the hero's X and Z position, but not the Y. (to avoid them looking up and down)
		transform.LookAt(new Vector3(hero.transform.position.x, 0.5f, hero.transform.position.z));

		// Constantly move the enemy forwards local to themselves
		//if (myRigidbody.velocity.magnitude<maxVelocity) myRigidbody.AddRelativeForce(movementForce, ForceMode.Force);
		Vector3 deltaV = targetVelocity - myRigidbody.velocity;
		Vector3 accel = deltaV / Time.deltaTime;
		if (accel.sqrMagnitude > maxAccel * maxAccel) accel = accel.normalized * maxAccel;
		myRigidbody.AddRelativeForce(accel, ForceMode.Acceleration);

	}

	// If something collides with the enemy
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.name.StartsWith("Bullet")) { // If it is a bullet
			Destroy(gameObject); // Destroy the enemy
			Destroy(collision.gameObject); // Destroy the bullet
			StaticVariables.AddScore(); // Add one to the score
		}
	}
}
