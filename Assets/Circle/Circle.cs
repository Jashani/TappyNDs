using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour {

	private static Color[] colours = new Color[] { Color.blue, Color.cyan, Color.green, Color.magenta, Color.red, Color.yellow };

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().color = colours [Random.Range (0, colours.Length)];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D () {
		gameObject.transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (Random.Range (0, Screen.width), Random.Range (0, Screen.height), Camera.main.farClipPlane / 2));
		Debug.Log ("Collision.");
	}

	void OnMouseDown () {
		FindObjectOfType<Score> ().addOnePoint ();
		Destroy (gameObject);
	}
}
