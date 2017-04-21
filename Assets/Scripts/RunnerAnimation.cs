using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerAnimation : MonoBehaviour {
    static public RunnerAnimation R;

    public Sprite[] red_left; //1
    public Sprite[] red_right;

    public Sprite[] blue_left; //2
    public Sprite[] blue_right;

    public Sprite[] green_left; //3
    public Sprite[] green_right;

    public Sprite[] yellow_left; //4
    public Sprite[] yellow_right;

    float curr_time;

    public enum ColorSprite {
        RED, BLUE, GREEN, YELLOW
    };

    public ColorSprite currentColor;

    InputManager.PlayerID currentRunner;

    Vector3 prevPosition;

    Vector3 currentVelocity;

    public Sprite getSprite(InputManager.PlayerID pID, Vector3 dir) {
        InputManager.PlayerColor clr = InputManager.S.getPlayerColorWithPlayerID(pID);
        switch (clr) {
            case InputManager.PlayerColor.Red:
                //use red
                return red_left[0];
            case InputManager.PlayerColor.Blue:
                //use blue
                return blue_left[0];
                //currentColor = ColorSprite.BLUE;
            case InputManager.PlayerColor.Green:
                return green_left[0];
                //R.GetComponent<SpriteRenderer>().sprite = green_left[0];
                //currentColor = ColorSprite.GREEN;
                //use green
            case InputManager.PlayerColor.Yellow:
                return yellow_left[0];
                //R.GetComponent<SpriteRenderer>().sprite = yellow_left[0];
                //currentColor = ColorSprite.YELLOW;
                //use yellow
        }
        return null;
    }

    void Awake() {
        R = this;
    }

    // Use this for initialization
    void Start () {
        curr_time = Time.time;
        currentVelocity = Vector3.zero;
        currentRunner = InputManager.S.getCurrentRunnerID();

        prevPosition = R.transform.position;

        InputManager.PlayerColor curr_color = InputManager.S.getPlayerColorWithPlayerID(currentRunner);

		switch (curr_color) {
            case InputManager.PlayerColor.Red:
                //use red
                R.GetComponent<SpriteRenderer>().sprite = red_left[0];
                currentColor = ColorSprite.RED;
                break;
            case InputManager.PlayerColor.Blue:
                //use blue
                R.GetComponent<SpriteRenderer>().sprite = blue_left[0];
                currentColor = ColorSprite.BLUE;
                break;
            case InputManager.PlayerColor.Green:
                R.GetComponent<SpriteRenderer>().sprite = green_left[0];
                currentColor = ColorSprite.GREEN;
                //use green
                break;
            case InputManager.PlayerColor.Yellow:
                R.GetComponent<SpriteRenderer>().sprite = yellow_left[0];
                currentColor = ColorSprite.YELLOW;
                //use yellow
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
        //direction of current movement
        currentVelocity = R.transform.position - prevPosition;
        currentVelocity = currentVelocity.normalized;
        prevPosition = R.transform.position;
        if (Time.time - curr_time > 0.1f)
        {
            curr_time = Time.time;
            

            if (currentVelocity == Vector3.left)
            {
                //change sprite
                switch (currentColor)
                {
                    case ColorSprite.RED:
                        if (R.GetComponent<SpriteRenderer>().sprite == red_left[0])
                            R.GetComponent<SpriteRenderer>().sprite = red_left[1];
                        else
                            R.GetComponent<SpriteRenderer>().sprite = red_left[0];
                        break;
                    case ColorSprite.BLUE:
                        if (R.GetComponent<SpriteRenderer>().sprite == blue_left[0])
                            R.GetComponent<SpriteRenderer>().sprite = blue_left[1];
                        else
                            R.GetComponent<SpriteRenderer>().sprite = blue_left[0];
                        break;
                    case ColorSprite.GREEN:
                        if (R.GetComponent<SpriteRenderer>().sprite == green_left[0])
                            R.GetComponent<SpriteRenderer>().sprite = green_left[1];
                        else
                            R.GetComponent<SpriteRenderer>().sprite = green_left[0];
                        break;
                    case ColorSprite.YELLOW:
                        if (R.GetComponent<SpriteRenderer>().sprite == yellow_left[0])
                            R.GetComponent<SpriteRenderer>().sprite = yellow_left[1];
                        else
                            R.GetComponent<SpriteRenderer>().sprite = yellow_left[0];
                        break;
                }
            }
            else if (currentVelocity == Vector3.right)
            {
                switch (currentColor)
                {
                    case ColorSprite.RED:
                        if (R.GetComponent<SpriteRenderer>().sprite == red_right[0])
                            R.GetComponent<SpriteRenderer>().sprite = red_right[1];
                        else
                            R.GetComponent<SpriteRenderer>().sprite = red_right[0];
                        break;
                    case ColorSprite.BLUE:
                        if (R.GetComponent<SpriteRenderer>().sprite == blue_right[0])
                            R.GetComponent<SpriteRenderer>().sprite = blue_right[1];
                        else
                            R.GetComponent<SpriteRenderer>().sprite = blue_right[0];
                        break;
                    case ColorSprite.GREEN:
                        if (R.GetComponent<SpriteRenderer>().sprite == green_right[0])
                            R.GetComponent<SpriteRenderer>().sprite = green_right[1];
                        else
                            R.GetComponent<SpriteRenderer>().sprite = green_right[0];
                        break;
                    case ColorSprite.YELLOW:
                        if (R.GetComponent<SpriteRenderer>().sprite == yellow_right[0])
                            R.GetComponent<SpriteRenderer>().sprite = yellow_right[1];
                        else
                            R.GetComponent<SpriteRenderer>().sprite = yellow_right[0];
                        break;
                }
            }
        }
	}
}
