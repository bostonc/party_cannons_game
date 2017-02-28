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
    public GameObject runner;//4

    public bool _________________;

    //these will be dynamically rearranged
    int player1JoyNum = 1;
    int player2JoyNum = 2;
    int player3JoyNum = 3;
    int player4JoyNum = 4;
    int runnerPlayer = 4; //player number of runner

    private void Awake()
    {
        S = this;
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //cannon1.GetComponent<CannonControl>().rotate(0f);
        //p1 input
        if (runnerPlayer == 1)
        {

        }
        else
        {

        }


        //p2 input
        if (runnerPlayer == 2)
        {

        }
        else
        {

        }


        //p3 input
        if (runnerPlayer == 3)
        {

        }
        else
        {

        }


        //p4 input
        if (runnerPlayer == 4)
        {

        }
        else
        {

        }


    }
}
