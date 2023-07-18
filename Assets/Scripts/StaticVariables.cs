using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public static class StaticVariables {
	public static Text scoreText;
	public static int PlayerScore = 0;
	public static int PreviousScore = 0;

	public static void AddScore() {
		PlayerScore += 1;
		scoreText.text = "Score: " + PlayerScore.ToString();
	}
}
