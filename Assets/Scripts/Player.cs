using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
	// Bullet prefab related
	public GameObject bullet;
	public Transform bulletSpawn;

	// UI Text
	public Text scoreText;
	public Text healthText;
	public Text ammoText;
	public Text staminaText;

	int health = 100; // Health
	int ammo = 1000;
	float stamina = 100;

	bool staminaRecharging = false;
	int speed = 3;

	public int walkSpeed = 3;
	public int runSpeed = 8;

	float timer = 1; // Wait timer for bullet fire

	Plane plane = new Plane(Vector3.up, Vector3.zero); // Create a new plane
	float distance; // Distance between plane and camera

	void updateHealth(int newHealth) {
		if (newHealth <= 0) {
			StaticVariables.PreviousScore = StaticVariables.PlayerScore;
			SceneManager.LoadScene(2, LoadSceneMode.Single);
		} else {
			health = newHealth;
			healthText.text = "Health: " + health.ToString() + "%"; // Set to default
		}
	}

	void updateStamina(float newStamina) {
		if (newStamina >= 0) {
			stamina = newStamina;
			staminaText.text = "Stamina: " + Mathf.RoundToInt(stamina).ToString() + "%";
		}
	}

	void updateAmmo(int newAmmo) {
		if (newAmmo >= 0) {
			ammo = newAmmo;
			ammoText.text = "Ammo: " + ammo.ToString();
		}
	}

	// Object initalised
	void Start() {
		StaticVariables.scoreText = scoreText;
		updateHealth(health);
		updateAmmo(ammo);
		updateStamina(stamina);
	}

	// Every frame
	void Update() {
		// Shift (Sprint)
		if (Input.GetKey(KeyCode.LeftShift) && !staminaRecharging && stamina > 0) {
			updateStamina(stamina - 0.5f);
			if (stamina > 0) {
				speed = runSpeed;
			} else {
				speed = walkSpeed;
			}
		} else if (Input.GetKeyUp(KeyCode.LeftShift)) {
			staminaRecharging = true;
		}

		if (staminaRecharging == true) {
			updateStamina(stamina + 0.2f);
			if (stamina >= 100) {
				staminaRecharging = false;
			}
		}

		// W (Forward)
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
			transform.Translate(new Vector3(0, 0, speed) * Time.deltaTime, Space.World); // Move forward local to world
		}

		// S (Backwards)
		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
			transform.Translate(new Vector3(0, 0, -speed) * Time.deltaTime, Space.World); // Move backwards local to world
		}

		// A (Left)
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			transform.Translate(new Vector3(-speed, 0, 0) * Time.deltaTime, Space.World); // Move left local to world
		}

		// D (Right)
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			transform.Translate(new Vector3(speed, 0, 0) * Time.deltaTime, Space.World); // Move right local to world
		}

		// Space or Left Click (Shoot)
		timer += Time.deltaTime; // Timer for cooldown
		if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)) && ammo > 0) {
			if (timer >= 0.75) {
				GameObject clone = Instantiate(bullet, bulletSpawn.position, bulletSpawn.transform.rotation);
				clone.GetComponent<Rigidbody>().velocity = bulletSpawn.TransformDirection(Vector3.up * 20);
				clone.name = "Bullet";
				timer = 0;
				updateAmmo(ammo - 1);
			}
		}

		// Aim at mouse cursor
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (plane.Raycast(ray, out distance)) {
			Vector3 direction = ray.GetPoint(distance) - transform.position;
			transform.rotation = Quaternion.Euler(0, Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg, 0);
		}
	}

	// If something collides with the player
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.name.StartsWith("Enemy")) { // If it's an enemy
			updateHealth(health-1);
		} else if (collision.gameObject.name.StartsWith("Bullet")) {
			updateAmmo(ammo + 1);
			Destroy(collision.gameObject);
		}
	}
}
