using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RainbowText : MonoBehaviour {

    public Text text;

    public Color color;

    // Use this for initialization
    void Start () {
        color = text.color;
	}
	
	// Update is called once per frame
	void Update () {
        color = text.color;
        Color newColor;
		if (color == Color.white)
        {
            newColor = Color.red;
        }
        else if (color == Color.red)
        {
            newColor = Color.yellow;
        }
        else if (color == Color.yellow)
        {
            newColor = Color.green;
        }
        else if (color == Color.green)
        {
            newColor = Color.blue;
        }
        else if (color == Color.blue)
        {
            newColor = Color.red;
        }
        else
        {
            newColor = Color.white;
        }
        text.color = Color.Lerp(color, newColor, Mathf.PingPong(Time.time, 1));
	}
}
