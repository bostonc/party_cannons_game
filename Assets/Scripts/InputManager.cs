﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Debugger should be able to use PC keyboard/mouse to control each player (one at a time).
    Use number keys 1, 2, 3, 4 to switch control between the respective players.

    Left stick (horizontal) to adjust rotation
    Right stick (vertical) to adjust pitch
    right trigger (hold for power adjust) to fire 
    RB to cancel firing

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
    public int player1JoyNum = 1; //respective to cannon number, 4 = runner
    public int player2JoyNum = 2;
    public int player3JoyNum = 3;
    public int player4JoyNum = 4;
    public int debugPlayerNum = 1; //who is the keyboard controlling?

    CannonControl cc1;
    CannonControl cc2;
    CannonControl cc3;

    float f = 0f;

    private void Awake()
    {
        S = this;
    }

    // Use this for initialization
    void Start ()
    {
        cc1 = cannon1.GetComponent<CannonControl>();
        cc2 = cannon2.GetComponent<CannonControl>();
        cc3 = cannon3.GetComponent<CannonControl>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        //p1 input on CONTROLLER 1
        if (player1JoyNum == 4)
        {
            //control runner
        }
        else
        {
            f = Input.GetAxis("Rotate_1");
            if (f != 0)
            {
                switch (player1JoyNum)
                {
                    case 1:
                        cc1.rotate(f);
                        break;
                    case 2:
                        cc2.rotate(f);
                        break;
                    case 3:
                        cc3.rotate(f);
                        break;
                }
            }
            f = Input.GetAxis("Pitch_1");
            if (f != 0)
            {
                switch (player1JoyNum)
                {
                    case 1:
                        cc1.pitch(f);
                        break;
                    case 2:
                        cc2.pitch(f);
                        break;
                    case 3:
                        cc3.pitch(f);
                        break;
                }
            }
            f = Input.GetAxis("Fire_1");
            if (f != 0)
            {
                switch (player1JoyNum)
                {
                    case 1:
                        cc1.fire(f);
                        break;
                    case 2:
                        cc2.fire(f);
                        break;
                    case 3:
                        cc3.fire(f);
                        break;
                }
            }
            //RB
            if (Input.GetKeyDown(KeyCode.Joystick1Button5))
            {
                switch (player1JoyNum)
                {
                    case 1:
                        cc1.stopFire();
                        break;
                    case 2:
                        cc2.stopFire();
                        break;
                    case 3:
                        cc3.stopFire();
                        break;
                }
            }
        }//end p1 else


        //p2 input on CONTROLLER 2
        if (player2JoyNum == 4)
        {
            //control runner
        }
        else
        {
            f = Input.GetAxis("Rotate_2");
            if (f != 0)
            {
                switch (player2JoyNum)
                {
                    case 1:
                        cc1.rotate(f);
                        break;
                    case 2:
                        cc2.rotate(f);
                        break;
                    case 3:
                        cc3.rotate(f);
                        break;
                }
            }
            f = Input.GetAxis("Pitch_2");
            if (f != 0)
            {
                switch (player2JoyNum)
                {
                    case 1:
                        cc1.pitch(f);
                        break;
                    case 2:
                        cc2.pitch(f);
                        break;
                    case 3:
                        cc3.pitch(f);
                        break;
                }
            }
            f = Input.GetAxis("Fire_2");
            if (f != 0)
            {
                switch (player2JoyNum)
                {
                    case 1:
                        cc1.fire(f);
                        break;
                    case 2:
                        cc2.fire(f);
                        break;
                    case 3:
                        cc3.fire(f);
                        break;
                }
            }
            //RB
            if (Input.GetKeyDown(KeyCode.Joystick2Button5))
            {
                switch (player2JoyNum)
                {
                    case 1:
                        cc1.stopFire();
                        break;
                    case 2:
                        cc2.stopFire();
                        break;
                    case 3:
                        cc3.stopFire();
                        break;
                }
            }
        }//end p2 else


        //p3 input on CONTROLLER 3
        if (player3JoyNum == 4)
        {
            //control runner
        }
        else
        {
            f = Input.GetAxis("Rotate_3");
            if (f != 0)
            {
                switch (player3JoyNum)
                {
                    case 1:
                        cc1.rotate(f);
                        break;
                    case 2:
                        cc2.rotate(f);
                        break;
                    case 3:
                        cc3.rotate(f);
                        break;
                }
            }
            f = Input.GetAxis("Pitch_3");
            if (f != 0)
            {
                switch (player3JoyNum)
                {
                    case 1:
                        cc1.pitch(f);
                        break;
                    case 2:
                        cc2.pitch(f);
                        break;
                    case 3:
                        cc3.pitch(f);
                        break;
                }
            }
            f = Input.GetAxis("Fire_3");
            if (f != 0)
            {
                switch (player3JoyNum)
                {
                    case 1:
                        cc1.fire(f);
                        break;
                    case 2:
                        cc2.fire(f);
                        break;
                    case 3:
                        cc3.fire(f);
                        break;
                }
            }
            //RB
            if (Input.GetKeyDown(KeyCode.Joystick3Button5))
            {
                switch (player3JoyNum)
                {
                    case 1:
                        cc1.stopFire();
                        break;
                    case 2:
                        cc2.stopFire();
                        break;
                    case 3:
                        cc3.stopFire();
                        break;
                }
            }
        }//end p3 else


        //p4 input on CONTROLLER 4
        if (player4JoyNum == 4)
        {
            //control runner
        }
        else
        {
            f = Input.GetAxis("Rotate_4");
            if (f != 0)
            {
                switch (player4JoyNum)
                {
                    case 1:
                        cc1.rotate(f);
                        break;
                    case 2:
                        cc2.rotate(f);
                        break;
                    case 3:
                        cc3.rotate(f);
                        break;
                }
            }
            f = Input.GetAxis("Pitch_4");
            if (f != 0)
            {
                switch (player4JoyNum)
                {
                    case 1:
                        cc1.pitch(f);
                        break;
                    case 2:
                        cc2.pitch(f);
                        break;
                    case 3:
                        cc3.pitch(f);
                        break;
                }
            }
            f = Input.GetAxis("Fire_4");
            if (f != 0)
            {
                switch (player4JoyNum)
                {
                    case 1:
                        cc1.fire(f);
                        break;
                    case 2:
                        cc2.fire(f);
                        break;
                    case 3:
                        cc3.fire(f);
                        break;
                }
            }
            //RB
            if (Input.GetKeyDown(KeyCode.Joystick4Button5))
            {
                switch (player4JoyNum)
                {
                    case 1:
                        cc1.stopFire();
                        break;
                    case 2:
                        cc2.stopFire();
                        break;
                    case 3:
                        cc3.stopFire();
                        break;
                }
            }
        }//end p4 else

        //debug player (keyboard)
        if (debugPlayerNum == 4)
        {
            //control runner
        }
        else
        {
            f = Input.GetAxis("Rotate_D");
            if (f != 0)
            {
                switch (debugPlayerNum)
                {
                    case 1:
                        cc1.rotate(f);
                        break;
                    case 2:
                        cc2.rotate(f);
                        break;
                    case 3:
                        cc3.rotate(f);
                        break;
                }
            }
            f = Input.GetAxis("Pitch_D");
            if (f != 0)
            {
                switch (debugPlayerNum)
                {
                    case 1:
                        cc1.pitch(f);
                        break;
                    case 2:
                        cc2.pitch(f);
                        break;
                    case 3:
                        cc3.pitch(f);
                        break;
                }
            }
            f = Input.GetAxis("Fire_D"); //space/left click
            if (f != 0)
            {
                switch (debugPlayerNum)
                {
                    case 1:
                        cc1.fire(f);
                        break;
                    case 2:
                        cc2.fire(f);
                        break;
                    case 3:
                        cc3.fire(f);
                        break;
                }
            }
            //RB (right click)
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                switch (debugPlayerNum)
                {
                    case 1:
                        cc1.stopFire();
                        break;
                    case 2:
                        cc2.stopFire();
                        break;
                    case 3:
                        cc3.stopFire();
                        break;
                }
            }
        }//end debugger else


    }//end update

    public void swapPlayer(int a, int b)
    {
        int temp = a;
        a = b;
        b = temp;
    }

}
