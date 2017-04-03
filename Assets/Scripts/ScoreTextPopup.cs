using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTextPopup : MonoBehaviour
{
    public int minFont = 0;
    public int maxFont = 20;
    public float fontTransitionSpeed = 1;
    public float riseSpeed = .1f; //speed of text rise
    public float fadeDelay = 2; //seconds before fade
    public float fadeSpeed = .01f;
    public string msg = "";
    Vector3 defaultSpawnLoc = new Vector3(0f, 10f, 24f);
    TextMesh tm;
    float currentFontSize = 0;

    float spawnTime;

	// Use this for initialization
	void Start ()
    {
        tm = gameObject.GetComponent<TextMesh>();
        spawnTime = Time.time;
        tm.fontSize = minFont;
        currentFontSize = minFont;
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
        if (!(Time.time - spawnTime > fadeDelay)) return;
        Color c = tm.color;
        c.a = c.a - fadeSpeed;
        tm.color = c;
        if (tm.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }

    void rise()
    {
        gameObject.transform.position = new Vector3(
            gameObject.transform.position.x,
            gameObject.transform.position.y + riseSpeed,
            gameObject.transform.position.z);
    }

    void fontTransform()
    {
        if (currentFontSize < maxFont)
        {
            currentFontSize += fontTransitionSpeed;
            tm.fontSize = (int)currentFontSize;
        }
    }

    public void construct(string msg, Vector3 loc)
    {
        gameObject.transform.position = loc;
        tm.text = msg;
    }

}
