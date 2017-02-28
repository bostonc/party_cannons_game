using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Debugger should be able to use PC keyboard/mouse to control each player (one at a time).
    Use number keys 1, 2, 3, 4 to switch control between the respective players.

    Left stick (horizontal) to adjust rotation
    Right stick (vertical) to adjust pitch
    right trigger (hold for power adjust) to fire 

    joy1-3 are launchers
    joy 4 is runner
 */

public class InputManager : MonoBehaviour
{
    static public InputManager S;

    public int maxRotation = 45;
    public int minRotation = 45;

    public GameObject cannon1;
    public GameObject cannon2;
    public GameObject cannon3;
    public GameObject runner;

    public bool _________________;

    int player1JoyNum = 1;
    int player2JoyNum = 2;
    int player3JoyNum = 3;
    int player4JoyNum = 4;

    private void Awake()
    {
        S = this;
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
