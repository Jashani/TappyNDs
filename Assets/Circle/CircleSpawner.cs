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
    public RectTransform topBar;

	private float radius;
    private float topBarHeight;
    private List<GameObject> circlePool;
	private Color mainCircleColour;
    private Color nextCircleColour;


	private int maxChildren = 10; // Maxmimum amount of circles
    private float minSpawnDelay = 1;
    private float maxSpawnDelay = 2.5f;
    private float mainColourChangeDelay = 5;
    private float gracePeriodDuration = 1;
    private bool gracePeriodOn = false;

    public float spawnNormalisationFactor;
    public static float timeToSpawn;
    public static float timeToChange;
    public static float timeToEndGrace;
    public List<Color> nextColourOptions;
    public List<float> colourProbabilities;
    public float[] spawnedSoFar;
    public float[] spawnedSoFarReal;

	private static Color[] colours = Colours.smallSet;

	void Start () {
		nextCircleColour = colours [Random.Range (0, colours.Length)];
        nextCircleDisplay.color = nextCircleColour;
        GetNextColour();

        colourProbabilities = new List<float>();
        for(int i = 0; i < colours.Length; i++)
            colourProbabilities.Add(1.0f/colours.Length);

        spawnedSoFar = new float[colours.Length];
        for(int i = 0; i < spawnedSoFar.Length; i++)
            spawnedSoFar[i] = 1f;
        spawnedSoFarReal = new float[colours.Length];
        for(int i = 0; i < spawnedSoFarReal.Length; i++)
            spawnedSoFarReal[i] = 0f;

        timeToSpawn = Random.Range (minSpawnDelay, maxSpawnDelay);

        circlePool = new List<GameObject>();
        for(int i = 0; i < maxChildren; i++)
        {
			GameObject obj = (GameObject)Instantiate (circle);
			obj.SetActive (false);
			circlePool.Add (obj);
        }
		radius = circle.GetComponent<CircleCollider2D> ().radius;
        topBarHeight = topBar.rect.height;
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

				//circlePool [i].GetComponent<SpriteRenderer> ().color = colours [Random.Range (0, colours.Length)];
                circlePool [i].GetComponent<SpriteRenderer> ().color = randomColour();
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

    Vector3 FindValidSpawn()
    {
        bool valid;
        int counter = 20;
        Vector3 spawnPoint = new Vector3(0, 0, 0);
		do {
			counter--;
			spawnPoint = Camera.main.ScreenToWorldPoint (new Vector3 (Random.Range (radius * 100, Screen.width - radius * 100), Random.Range (radius * 100, Screen.height - radius * 100 - topBarHeight), Camera.main.farClipPlane / 2));
            valid = true;
			foreach (GameObject g in circlePool) {
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
     
    Color randomColour()
    {
        //colourProbabilities.Sort();
        float[] copy = colourProbabilities.ToArray();
        float value = Random.Range(0f, 1f);
        for(int i = 1; i < copy.Length; i++)
        {
            copy[i] = copy[i]+copy[i-1];
            if(value > copy[i-1] && value <= copy[i])
            {
                updateProbabilities(i);
                return colours[i];
            }     
        }
        updateProbabilities(0);
        return colours[0];
    }

    void updateProbabilities(int i)
    {
        //Assign probabilities
        spawnedSoFar[i] += spawnNormalisationFactor;
        spawnedSoFarReal[i]++;
        for(int j = 0; j < spawnedSoFar.Length; j++)
        {
            spawnedSoFar[j] *= spawnedSoFar.Length / (spawnedSoFar.Length+spawnNormalisationFactor);
            colourProbabilities[j] = (spawnedSoFar.Length-spawnedSoFar[j])/12;
        }

        //Take primary colour into account
        int mainColourIndex = 0;
        for(int j = 0; j < colours.Length; j++)
        {
            if(colours[j] == mainCircleColour)
            {
                mainColourIndex = j;
                break;
            }
        }
        float dif = colourProbabilities[mainColourIndex];
        float sum = 1 - colourProbabilities[mainColourIndex];
        colourProbabilities[mainColourIndex] *= 2;
        for(int j = 0; j < colourProbabilities.Count; j++)
            if(j != mainColourIndex)
                colourProbabilities[j] -= colourProbabilities[j]/sum*dif;

        //Increase the chance of the colour which has been seen the least recently
        int smallestI = 0;
        float smallest = spawnedSoFar[0];
        for(int j = 1; j < spawnedSoFar.Length; j++)
            if (spawnedSoFar[j] < smallest)
                smallestI = j;
        Debug.Log(smallestI);
        dif = colourProbabilities[smallestI]/2;
        sum = 1 - colourProbabilities[smallestI];
        colourProbabilities[smallestI] *= 1.5f;
        for(int j = 0; j < colourProbabilities.Count; j++)
            if(j != smallestI)
                colourProbabilities[j] -= colourProbabilities[j]/sum*dif;
    }
}
