using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : MonoBehaviour {

	const string MASTER_VOLUME_KEY = "master_volume";
	const string DIFFICULTY_KEY = "difficulty";
	const string LEVEL_KEY = "level_unlocked_"; // level_unlocked_1,2,3...

	public static void SetMasterVolume (float volume){
		Debug.Log ("Volume: " + volume);
		if (volume >= 0f && volume <= 1f)
			PlayerPrefs.SetFloat (MASTER_VOLUME_KEY, volume);
		else
			Debug.LogError ("Master volume out of range.");
	}

	public static float GetMasterVolume(){ return PlayerPrefs.GetFloat (MASTER_VOLUME_KEY); }

	public static void UnlockLevel (int level){
		if (level <= SceneManager.sceneCount - 1)
			PlayerPrefs.SetInt (LEVEL_KEY + level.ToString (), 1); //1 for true.
		else
			Debug.LogError ("Trying to access non existing scene at UnlockLevel().");
	}

	public static bool IsLevelUnlocked (int level){
		if (level <= SceneManager.sceneCount - 1) {
			if (PlayerPrefs.GetInt (LEVEL_KEY + level.ToString ()) == 1)
				return true;
			else
				return false;
		} else
			Debug.LogError ("Trying to access non existing scene at IsLevelUnlocked().");
		return false;
	}

	public static void SetDifficulty (float difficulty){
		if (difficulty >= 1f && difficulty <= 3f)
			PlayerPrefs.SetFloat (DIFFICULTY_KEY, difficulty);
		else
			PlayerPrefs.SetFloat (DIFFICULTY_KEY, 1f);
	}

	public static float GetDifficulty (){ 
		if (PlayerPrefs.GetFloat (DIFFICULTY_KEY) != null && PlayerPrefs.GetFloat (DIFFICULTY_KEY) >= 1f)
			return PlayerPrefs.GetFloat (DIFFICULTY_KEY);
		else
			SetDifficulty (1f);
		return 1f;
	}
}
