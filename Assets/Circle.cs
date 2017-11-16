using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D () {
		gameObject.transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (Random.Range (0, Screen.width), Random.Range (0, Screen.height), Camera.main.farClipPlane / 2));
		Debug.Log ("Collision.");
	}

	void OnMouseDown () {
		Destroy (gameObject);
	}
}
