using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioClip[] vulgarClips;
	public AudioClip[] regularClips;

	private AudioSource audioSource;
	private bool vulgar;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		SetVolume (PlayerPrefsManager.GetMasterVolume ()); 
		vulgar = PlayerPrefsManager.GetVulgarity ();
	}
	
	public void PopSound(){
		if (vulgar && vulgarClips != null && vulgarClips.Length != 0)
			audioSource.clip = vulgarClips [Random.Range (0, vulgarClips.Length)];
		else if (regularClips != null && regularClips.Length != 0)
			audioSource.clip = regularClips [Random.Range (0, vulgarClips.Length)];
		else
			return;
		audioSource.Play ();
	}

	public void SetVolume(float volume){ audioSource.volume = (volume + 0.4f) % 1; }
}
