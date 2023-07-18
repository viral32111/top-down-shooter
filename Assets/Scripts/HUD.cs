using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
	public Text previousScoreLabel;

	void Start() {
		if (StaticVariables.PreviousScore > 0) {
			previousScoreLabel.text = "Previous: " + StaticVariables.PreviousScore.ToString();
		} else {
			previousScoreLabel.text = "";
		}
	}
}
