using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

	public Slider volumeSlider, difficultySlider;
	public LevelManager levelManager;

	private MusicManager musicManager;

	// Use this for initialization
	void Start () {
		musicManager = GameObject.FindObjectOfType<MusicManager> ();
		volumeSlider.value = PlayerPrefsManager.GetMasterVolume ();
		difficultySlider.value = PlayerPrefsManager.GetDifficulty ();
	}

	// Update is called once per frame
	void Update () {
		try {
			musicManager.SetVolume (volumeSlider.value);
		} catch {
			Debug.Log ("Level likely loaded solitarily.");
		}
	}

	public void SaveAndExit(){
		PlayerPrefsManager.SetMasterVolume (volumeSlider.value);
		PlayerPrefsManager.SetDifficulty (difficultySlider.value);
		levelManager.LoadLevel ("01a MainMenu");
	}

	public void SetDefaults(){
		volumeSlider.value = 0.8f;
		difficultySlider.value = 0.5f;
	}
}
