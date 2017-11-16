using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawner : MonoBehaviour {

	public GameObject circle;
	private float radius;

	private int maxChildren = 10;

	// Use this for initialization
	void Start () {
		radius = circle.GetComponent<CircleCollider2D> ().radius;
	}
	
	// Update is called once per frame
	void Update () {
		if (isTimeToSpawn () && gameObject.transform.childCount < maxChildren) {
			Spawn ();
		}
	}

	bool isTimeToSpawn() {
		float spawnDelay = 10;
		//float spawnsPerSecond = spawnDelay;
		//Debug.Log (spawnsPerSecond);

		if (Time.deltaTime > spawnDelay) 
			Debug.LogWarning ("Spawn rate capped by frame rate.");

		float threshold = spawnDelay * Time.deltaTime / 10f ;

		return (Random.value < threshold);
	}

	void Spawn() {
		Vector3 screenPosition = Camera.main.ScreenToWorldPoint (new Vector3 (Random.Range (radius, Screen.width-radius), Random.Range (radius, Screen.height-radius), Camera.main.farClipPlane / 2));
		GameObject newCircle = Instantiate (circle, screenPosition, Quaternion.identity);
		newCircle.transform.parent = gameObject.transform;
	}
}
