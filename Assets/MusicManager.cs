using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

	public AudioClip[] levelMusicChangeArray;

	private AudioSource audioSource;

	void Awake () {
		DontDestroyOnLoad (gameObject);
	}

	void Start() {
		audioSource = GetComponent<AudioSource> ();
		SetVolume (PlayerPrefsManager.GetMasterVolume ());
	}

	void OnLevelWasLoaded(int level) {
		AudioClip currentClip = levelMusicChangeArray [level];
		Debug.Log ("Playing: " + currentClip);

		if (currentClip) {
			audioSource.clip = currentClip;
			audioSource.loop = true;
			audioSource.Play ();
		}
	}

	public void SetVolume(float volume){ audioSource.volume = volume; }
}
