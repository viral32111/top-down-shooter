using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnEnemy : MonoBehaviour {
	// Spawn distance from hero/player
	public int spawnDistance = 16;

	// Enemy prefab
	public GameObject enemy;

	// Hero to target
	public GameObject hero;

	public Text enemyCountLabel;

	// Generate a random vector around the hero object
	Vector3 randomSpawnPos() {
		Vector3 vec = new Vector3(hero.transform.position.x, hero.transform.position.y, hero.transform.position.z);
		vec.x += Random.Range(-spawnDistance, spawnDistance);
		vec.z += Random.Range(-spawnDistance, spawnDistance);
		return vec;
	}

	// Runs every frame
	void Update() {
		// Spawn new enemies if the amount of enemies currently spawned is less than 5
		int enemyNeeded = (8 + StaticVariables.PlayerScore);
		if (gameObject.transform.childCount < enemyNeeded) {
			GameObject clone = Instantiate(enemy, randomSpawnPos(), enemy.transform.rotation);
			clone.transform.parent = gameObject.transform;
			clone.name = "Enemy";
			enemyCountLabel.text = "Enemies: " + enemyNeeded.ToString();
		}
	}
}
