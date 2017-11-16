using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawner : MonoBehaviour {

	public GameObject circle;
	private float radius;

	private int maxChildren = 10; // Maxmimum amount of circles

	void Start () {
		radius = circle.GetComponent<CircleCollider2D> ().radius;
	}

	void Update () {
		if (isTimeToSpawn () && gameObject.transform.childCount < maxChildren) {
			Spawn ();
		}
	}

	bool isTimeToSpawn() {
		// TODO: Make a more efficient function? This one's just kinda gay and I took it from a different project.
		float spawnDelay = 10;

		if (Time.deltaTime > spawnDelay) 
			Debug.LogWarning ("Spawn rate capped by frame rate.");

		float threshold = spawnDelay * Time.deltaTime / 10f ;

		return (Random.value < threshold);
	}

	void Spawn() {
		// TODO: Make circles not spawn on each other, either here or wherever comfortable.
		// Also try to figure out a way to make them spawn away from the top UI.
		// If creative enough do as you please, if not we'll just use distance constants or something, I don't know
		Vector3 screenPosition = Camera.main.ScreenToWorldPoint (new Vector3 (Random.Range (radius, Screen.width-radius), Random.Range (radius, Screen.height-radius), Camera.main.farClipPlane / 2));
		GameObject newCircle = Instantiate (circle, screenPosition, Quaternion.identity);
		newCircle.transform.parent = gameObject.transform;
	}
}
