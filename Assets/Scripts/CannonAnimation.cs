using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonAnimation : MonoBehaviour {
    public Sprite[] red;
    public Sprite[] blue;
    public Sprite[] green;
    public Sprite[] yellow;

    public InputManager.ControlID curr_CID;

    public RunnerAnimation.ColorSprite curr_color;

	// Use this for initialization
	void Start () {
        //GET COLOr of cannon
        InputManager.PlayerColor pclr = InputManager.S.getPlayerColorWithControlID(curr_CID);
        if (pclr == InputManager.PlayerColor.Red)
        {

        }
        else if (pclr == InputManager.PlayerColor.Blue)
        {

        }
        else if (pclr == InputManager.PlayerColor.Green)
        {

        }
        else if (pclr == InputManager.PlayerColor.Yellow)
        {

        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
