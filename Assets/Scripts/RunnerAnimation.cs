using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerAnimation : MonoBehaviour {
    RunnerAnimation R;

    public Sprite[] red_left; //1
    public Sprite[] red_right;

    public Sprite[] blue_left; //2
    public Sprite[] blue_right;

    public Sprite[] green_left; //3
    public Sprite[] green_right;

    public Sprite[] yellow_left; //4
    public Sprite[] yellow_right;

    enum ColorSprite {
        RED, BLUE, GREEN, YELLOW
    };

    ColorSprite currentColor;

    InputManager.PlayerInfo currentRunner;

    Vector3 prevPosition;

    // Use this for initialization
    void Start () {
        currentRunner = InputManager.S.getCurrentRunner();
        R = this;

        prevPosition = R.transform.position;

		switch (currentRunner.playerID) {
            case InputManager.PlayerID.Player1:
                //use red
                R.GetComponent<SpriteRenderer>().sprite = red_left[0];
                currentColor = ColorSprite.RED;
                break;
            case InputManager.PlayerID.Player2:
                //use blue
                R.GetComponent<SpriteRenderer>().sprite = blue_left[0];
                currentColor = ColorSprite.BLUE;
                break;
            case InputManager.PlayerID.Player3:
                R.GetComponent<SpriteRenderer>().sprite = green_left[0];
                currentColor = ColorSprite.GREEN;
                //use green
                break;
            case InputManager.PlayerID.Player4:
                R.GetComponent<SpriteRenderer>().sprite = yellow_left[0];
                currentColor = ColorSprite.YELLOW;
                //use yellow
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
        //direction of current movement
        Vector3 currentVelocity = R.transform.position - prevPosition;
        currentVelocity = currentVelocity.normalized;
        prevPosition = R.transform.position;

        if (currentVelocity == Vector3.left) {
            //change sprite
            switch (currentColor) {
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
        else if (currentVelocity == Vector3.right) {
            switch (currentColor) {
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
