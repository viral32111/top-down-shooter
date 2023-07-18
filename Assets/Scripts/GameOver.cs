using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
	// Score text gameobject
	public Text scoreText;
	public Text scoreDataLabel;
	public InputField nameEntry;

	private string scoreFilePath;
	private Dictionary<string, int> scores;

	// Runs whenever the respawn button is clicked
	public void respawnButtonClicked() {
		StaticVariables.PreviousScore = StaticVariables.PlayerScore; // Set the previous score
		StaticVariables.PlayerScore = 0; // Reset the player's score
		SceneManager.LoadScene(1, LoadSceneMode.Single); // Load the main game scene
	}

	public void saveScoreButtonClicked() {
		if (nameEntry.text != "") {
			scores[nameEntry.text] = StaticVariables.PlayerScore;
			saveScores();
			updateScoreboard();
		}
	}

	public void nameEntryChanged() {
		if (nameEntry.text.Length > 20) nameEntry.text = nameEntry.text.Substring(0, 20);
	}

	private void saveScores() {
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream file = File.Create(scoreFilePath);
		formatter.Serialize(file, scores);
		file.Close();
	}

	private void loadScores() {
		if (File.Exists(scoreFilePath)) {
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream file = File.OpenRead(scoreFilePath);
			scores = (Dictionary<string, int>)formatter.Deserialize(file);
			file.Close();
		} else {
			scores = new Dictionary<string, int>();
		}
	}

	private void updateScoreboard() {
		scoreDataLabel.text = "";
		foreach (KeyValuePair<string, int> score in scores) {
			scoreDataLabel.text += score.Key + ": " + score.Value.ToString() + "\n";
		}
	}

	// Runs when the canvas initalises
	void Start() {
		StaticVariables.PlayerScore = Random.Range(5, 100);

		scoreFilePath = Application.persistentDataPath + "/scores.dat";
		loadScores();

		scoreText.text = "You scored: " + StaticVariables.PlayerScore.ToString() + "!";

		updateScoreboard();
	}
}
