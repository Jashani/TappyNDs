using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {
	
	public AudioClip[] levelMusicChangeArray;

	private static AudioSource audioSource;

	private static MusicManager instance = null;
	public static MusicManager Instance { 
		get { 
			if (instance == null)
				instance = (MusicManager)FindObjectOfType (typeof(MusicManager)); 
			return instance; 
		} 
	}

	void Awake () {
		if (Instance != this) {
			Destroy (gameObject);
		} else {
			DontDestroyOnLoad (gameObject);
		}
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
