using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	private static Text scoreText;
	private static int score;

	void Start() {
		scoreText = GetComponent<Text> ();
		score = 0;
	}
	
	public void addOnePoint() {
		score++;
		scoreText.text = score.ToString ();
	}
}
