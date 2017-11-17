using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleSpawner : MonoBehaviour {
    
	public GameObject circle;
    public Image mainCircleDisplay;
    public Image nextCircleDisplay;

	private float radius;
    private List<GameObject> circlePool;
	private Color mainCircleColour;
    private Color nextCircleColour;

	private int maxChildren = 10; // Maxmimum amount of circles
    public float timeToSpawn;
    private static Color[] colours = new Color[] { Color.cyan, Color.green, Color.magenta, Color.red, Color.yellow };

	void Start () {
		mainCircleColour = colours [Random.Range (0, colours.Length)];
        mainCircleDisplay.color = mainCircleColour;
        nextCircleColour = colours [Random.Range (0, colours.Length)];
        nextCircleDisplay.color = nextCircleColour;

		timeToSpawn = Random.Range (1, 2.5f);

        circlePool = new List<GameObject>();
        for(int i = 0; i < maxChildren; i++)
        {
			GameObject obj = (GameObject)Instantiate (circle);
			obj.SetActive (false);
			circlePool.Add (obj);
        }
		radius = circle.GetComponent<CircleCollider2D> ().radius * 100;
	}

	void Update () {
        timeToSpawn -= Time.deltaTime;
        if (timeToSpawn <= 0)
        {
            timeToSpawn = Random.Range(1, 2.5f);
            Spawn();
        }
	}

	//bool isTimeToSpawn() {
		// TODO: Make a more efficient function? This one's just kinda gay and I took it from a different project.

	//}

	void Spawn() {
		// TODO: Make circles not spawn on each other, either here or wherever comfortable.
		// Also try to figure out a way to make them spawn away from the top UI.
		// If creative enough do as you please, if not we'll just use distance constants or something, I don't know
		Vector3 screenPosition = Camera.main.ScreenToWorldPoint (new Vector3 (Random.Range (radius, Screen.width-radius), Random.Range (radius, Screen.height-radius), Camera.main.farClipPlane / 2));
        for(int i = 0; i < circlePool.Count; i++)
        {
			if (!circlePool [i].activeInHierarchy) 
			{
				circlePool [i].transform.position = screenPosition;
				circlePool [i].transform.rotation = Quaternion.identity;
				circlePool [i].transform.parent = gameObject.transform;

				circlePool [i].GetComponent<SpriteRenderer> ().color = mainCircleColour;
				mainCircleColour = nextCircleColour;
				mainCircleDisplay.color = mainCircleColour;
				nextCircleColour = colours [Random.Range (0, colours.Length)];
				nextCircleDisplay.color = nextCircleColour;

				circlePool [i].SetActive (true);
				break;
			}
        }
        //GameObject newCircle = Instantiate (circle, screenPosition, Quaternion.identity);
		//newCircle.transform.parent = gameObject.transform;
	}

	public Color getMain() { return mainCircleColour; }
}
