using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour {

	private Animator anim;
	private CircleSpawner spawner;

    void Start()
    {
        anim = GetComponent<Animator> ();
		spawner = FindObjectOfType<CircleSpawner>();
    }

	void OnMouseDown () {
		anim.Play ("Pop");
	}

	void Tapped () { // Called from 'Pop' animation.
		if (spawner.getMain() == GetComponent<SpriteRenderer> ().color)
			FindObjectOfType<Score> ().addOnePoint ();
		gameObject.SetActive(false);
	}

	void Deactivate () {
		gameObject.SetActive(false);
	}
}
