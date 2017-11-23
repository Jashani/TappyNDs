using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour {

	private Animator anim;
	private CircleSpawner spawner;
	private SoundManager soundManager;

    void Start()
    {
        anim = GetComponent<Animator> ();
		spawner = FindObjectOfType<CircleSpawner>();
		soundManager = FindObjectOfType<SoundManager> ();
    }

	void OnMouseDown () {
		soundManager.PopSound ();
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
