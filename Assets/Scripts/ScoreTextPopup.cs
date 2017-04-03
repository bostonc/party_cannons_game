using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTextPopup : MonoBehaviour
{
    public int minFont = 0;
    public int maxFont = 20;
    public float FontSizeTransitionSpeed = 5;
    public float riseSpeed = .2f; //speed of text rise
    public float fadeDelay = 2; //seconds before fade
    public float fadeSpeed = .01f;
    public string msg = "";
    Vector3 defaultSpawnLoc = new Vector3(0f, 10f, 24f);
    TextMesh tm;

    float spawnTime;

	// Use this for initialization
	void Start ()
    {
        tm = gameObject.GetComponent<TextMesh>();
        spawnTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {
        fade();
        rise();
        fontTransform();
	}

    void fade()
    {

    }

    void rise()
    {

    }

    void fontTransform()
    {

    }

    public void construct(string msg, Vector3 loc)
    {

    }

}
