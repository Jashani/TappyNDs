using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour {

	private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator> ();//Should be on Start() so it's only called once, and not every single time the circle re-appears.
    }

	void OnEnable () {
		//GetComponent<SpriteRenderer> ().color = colours [Random.Range (0, colours.Length)];
        //Moved to CircleSpawner
	}

	void OnTriggerStay2D () {
		// TODO: Remove this nonsense and add something that keeps them from spawning on each other.
		//gameObject.transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (Random.Range (0, Screen.width), Random.Range (0, Screen.height), Camera.main.farClipPlane / 2));
		//Debug.Log ("Collision.");
	}

	void OnMouseDown () {
		anim.Play ("Pop");
	}

	void Tapped () { // Called from 'Pop' animation.
		FindObjectOfType<Score> ().addOnePoint ();
		gameObject.SetActive(false);
	}
}
