using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class LevelManager : MonoBehaviour {

	public float autoLoad = 0f;

	void Start(){
		if (autoLoad <= 0) {
			Debug.Log ("No autoload");
		} else {
			Invoke ("LoadNextLevel", autoLoad);
		}
	}

	public void LoadLevel(string name){
		Debug.Log ("New Level load: " + name);
		SceneManager.LoadScene (name);
	}

	public void LoadNextLevel(){
		Debug.Log ("New Level load: " + name);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

	public void EndGame(){
		StartCoroutine(DelayedLoad());
	}

	public IEnumerator DelayedLoad(){
		Debug.Log ("Waiting");
		yield return new WaitForSecondsRealtime (2);
		Debug.Log ("Waited");
		LoadLevel ("End Screen");
	}

}
