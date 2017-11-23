using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleSpawner : MonoBehaviour {
    
	//TODO: Add lives concept.
	//TODO: Program timer.
	//TODO: Add difficulty concept (should be discussed obvs but this is just a reminder).

	public GameObject circle;
    public Image mainCircleDisplay;
    public Image nextCircleDisplay;
    public Image progressBar;

	private float radius;
    private List<GameObject> circlePool;
	private Color mainCircleColour;
    private Color nextCircleColour;

	private int maxChildren = 10; // Maxmimum amount of circles
    private float minSpawnDelay = 1;
    private float maxSpawnDelay = 2.5f;
    private float mainColourChangeDelay = 5;
    private float gracePeriodDuration = 1;
    private bool gracePeriodOn = false;

    public static float timeToSpawn;
    public static float timeToChange;
    public static float timeToEndGrace;
    public List<Color> nextColourOptions;

	private static Color[] colours = Colours.smallSet;

	void Start () {
		nextCircleColour = colours [Random.Range (0, colours.Length)];
        nextCircleDisplay.color = nextCircleColour;
        GetNextColour();


        timeToSpawn = Random.Range (minSpawnDelay, maxSpawnDelay);

        circlePool = new List<GameObject>();
        for(int i = 0; i < maxChildren; i++)
        {
			GameObject obj = (GameObject)Instantiate (circle);
			obj.SetActive (false);
			circlePool.Add (obj);
        }
		radius = circle.GetComponent<CircleCollider2D> ().radius;
	}

	void Update () {
        UpdateSpawnTimer();
        if(gracePeriodOn)
            UpdateGracePeriodTimer();
        else
            UpdateChangeTimer();
	}

    void UpdateSpawnTimer(){
        timeToSpawn -= Time.deltaTime;
        if (timeToSpawn <= 0)
        {
            timeToSpawn = Random.Range(minSpawnDelay, maxSpawnDelay);
            Spawn();
        }
    }

    void UpdateChangeTimer(){
        timeToChange -= Time.deltaTime;
        SetProgressBar(timeToChange/mainColourChangeDelay);
        if (timeToChange <= 0)
        {
            timeToChange = mainColourChangeDelay;
            ChangeColours();
        }
    }

    void UpdateGracePeriodTimer(){
        timeToEndGrace -= Time.deltaTime;
        SetProgressBar(1- timeToEndGrace/gracePeriodDuration);
        if (timeToEndGrace <= 0)
        {
            timeToEndGrace = gracePeriodDuration;
            gracePeriodOn = false;
        }
    }

	void Spawn() {
		for (int i = 0; i < circlePool.Count; i++) {
			if (!circlePool [i].activeInHierarchy) {
				circlePool [i].transform.position = FindValidSpawn ();
				circlePool [i].transform.rotation = Quaternion.identity;
				circlePool [i].transform.parent = gameObject.transform;

				circlePool [i].GetComponent<SpriteRenderer> ().color = colours [Random.Range (0, colours.Length)];
				circlePool [i].SetActive (true);
				break;
			}
		}
	}

    void ChangeColours(){
        mainCircleColour = nextCircleColour;
        mainCircleDisplay.color = mainCircleColour;
        nextColourOptions = new List<Color>();
        foreach (Color c in colours)
        {
            if(c != mainCircleColour)
            {
                nextColourOptions.Add(c);
            }
        }
        nextCircleColour = nextColourOptions [Random.Range (0, nextColourOptions.Count)];
        nextCircleDisplay.color = nextCircleColour;
        gracePeriodOn = true;
        progressBar.color = mainCircleColour;
    }

    private void GetNextColour()
    {
        mainCircleColour = nextCircleColour;
        mainCircleDisplay.color = mainCircleColour;
        nextColourOptions = new List<Color>();
        foreach (Color c in colours)
        {
            if(c != mainCircleColour)
            {
                nextColourOptions.Add(c);
            }
        }
        nextCircleColour = nextColourOptions [Random.Range (0, nextColourOptions.Count)];
        nextCircleDisplay.color = nextCircleColour;
    }

	public Color getMain() { return mainCircleColour; }

	//TODO: Make circles avoid the top bar.

    Vector3 FindValidSpawn()
    {
        bool valid;
        int counter = 20;
        Vector3 spawnPoint = new Vector3(0, 0, 0);
		do {
			counter--;
			spawnPoint = Camera.main.ScreenToWorldPoint (new Vector3 (Random.Range (radius * 100, Screen.width - radius * 100), Random.Range (radius * 100, Screen.height - radius * 100), Camera.main.farClipPlane / 2));
			valid = true;
			foreach (GameObject g in circlePool) {
				if (g.activeInHierarchy) {
					Debug.Log ("distance: " + (spawnPoint - g.transform.position).magnitude.ToString ());
					Debug.Log ("radius*2: " + radius);
				}

				if (g.activeInHierarchy && (spawnPoint - g.transform.position).magnitude < radius) {
					valid = false;
					break;
				}
			}
		} while(!valid && counter >= 0);
        return spawnPoint;
    }

    void SetProgressBar(float percentage)
    {
        progressBar.fillAmount = percentage * 0.42f + 0.041f;
    }
}
