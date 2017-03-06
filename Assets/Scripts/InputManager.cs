using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject cannon1;
    public GameObject cannon2;
    public GameObject cannon3;
    public GameObject runner;//4

    public Text pausedText;

    public bool paused = false;

    public bool _________________;

    //these will be dynamically rearranged
    public int player1JoyNum = 1; //respective to cannon number, 4 = runner
    public int player2JoyNum = 2;
    public int player3JoyNum = 3;
    public int player4JoyNum = 4;
	public int debugPlayerNum = -1; //who is the keyboard controlling?
	public bool debugAllow = true;

    CannonControl cc1;
    CannonControl cc2;
    CannonControl cc3;
	PlayerControl pc1;

    float f = 0f;

    private void Awake()
    {
        S = this;
    }

	// get the JoyNum controlling a given cannon (1 - 3) or runner (4)
	public int getPlayerJoyNumForController(int controller) {
		if(player1JoyNum == controller) 
			return 1;
		else if(player2JoyNum == controller) 
			return 2;
		else if(player3JoyNum == controller) 
			return 3;
		else 
			return 4;
	}

    // Use this for initialization
    void Start ()
    {
        cc1 = cannon1.GetComponent<CannonControl>();
        cc2 = cannon2.GetComponent<CannonControl>();
        cc3 = cannon3.GetComponent<CannonControl>();
		pc1 = runner.GetComponent<PlayerControl>();

        pausedText.enabled = false;
        randomizePlayers();
		debugPlayerNum = getPlayerJoyNumForController(1); // whatever player 1 is controlling, control that.
    }
	
	// Update is called once per frame
	void Update ()
	{
		if (Scorekeeper.S.gameOver)
			return;
		
		//p1 input on CONTROLLER 1
		if (player1JoyNum == 4) {
			//runner control
		} else {
			f = Input.GetAxis ("Rotate_1");
			if (f != 0) {
				switch (player1JoyNum) {
				case 1:
					cc1.rotate (f);
					break;
				case 2:
					cc2.rotate (f);
					break;
				case 3:
					cc3.rotate (f);
					break;
				}
			}
			f = Input.GetAxis ("Pitch_1");
			if (f != 0) {
				switch (player1JoyNum) {
				case 1:
					cc1.pitch (f);
					break;
				case 2:
					cc2.pitch (f);
					break;
				case 3:
					cc3.pitch (f);
					break;
				}
			}
			f = Input.GetAxis ("Fire_1");
			if (f != 0) {
				switch (player1JoyNum) {
				case 1:
					cc1.fire (f);
					break;
				case 2:
					cc2.fire (f);
					break;
				case 3:
					cc3.fire (f);
					break;
				}
			}
			//RB
			if (Input.GetKeyDown (KeyCode.Joystick1Button5)) {
				switch (player1JoyNum) {
				case 1:
					cc1.stopFire ();
					break;
				case 2:
					cc2.stopFire ();
					break;
				case 3:
					cc3.stopFire ();
					break;
				}
			}
		}//end p1 else


		//p2 input on CONTROLLER 2
		if (player2JoyNum == 4) {
			//runner control
		} else {
			f = Input.GetAxis ("Rotate_2");
			if (f != 0) {
				switch (player2JoyNum) {
				case 1:
					cc1.rotate (f);
					break;
				case 2:
					cc2.rotate (f);
					break;
				case 3:
					cc3.rotate (f);
					break;
				}
			}
			f = Input.GetAxis ("Pitch_2");
			if (f != 0) {
				switch (player2JoyNum) {
				case 1:
					cc1.pitch (f);
					break;
				case 2:
					cc2.pitch (f);
					break;
				case 3:
					cc3.pitch (f);
					break;
				}
			}
			f = Input.GetAxis ("Fire_2");
			if (f != 0) {
				switch (player2JoyNum) {
				case 1:
					cc1.fire (f);
					break;
				case 2:
					cc2.fire (f);
					break;
				case 3:
					cc3.fire (f);
					break;
				}
			}
			//RB
			if (Input.GetKeyDown (KeyCode.Joystick2Button5)) {
				switch (player2JoyNum) {
				case 1:
					cc1.stopFire ();
					break;
				case 2:
					cc2.stopFire ();
					break;
				case 3:
					cc3.stopFire ();
					break;
				}
			}
		}//end p2 else


		//p3 input on CONTROLLER 3
		if (player3JoyNum == 4) {
			//runner control
		} else {
			f = Input.GetAxis ("Rotate_3");
			if (f != 0) {
				switch (player3JoyNum) {
				case 1:
					cc1.rotate (f);
					break;
				case 2:
					cc2.rotate (f);
					break;
				case 3:
					cc3.rotate (f);
					break;
				}
			}
			f = Input.GetAxis ("Pitch_3");
			if (f != 0) {
				switch (player3JoyNum) {
				case 1:
					cc1.pitch (f);
					break;
				case 2:
					cc2.pitch (f);
					break;
				case 3:
					cc3.pitch (f);
					break;
				}
			}
			f = Input.GetAxis ("Fire_3");
			if (f != 0) {
				switch (player3JoyNum) {
				case 1:
					cc1.fire (f);
					break;
				case 2:
					cc2.fire (f);
					break;
				case 3:
					cc3.fire (f);
					break;
				}
			}
			//RB
			if (Input.GetKeyDown (KeyCode.Joystick3Button5)) {
				switch (player3JoyNum) {
				case 1:
					cc1.stopFire ();
					break;
				case 2:
					cc2.stopFire ();
					break;
				case 3:
					cc3.stopFire ();
					break;
				}
			}
		}//end p3 else


		//p4 input on CONTROLLER 4
		if (player4JoyNum == 4) {
			//runner control
		} else {
			f = Input.GetAxis ("Rotate_4");
			if (f != 0) {
				switch (player4JoyNum) {
				case 1:
					cc1.rotate (f);
					break;
				case 2:
					cc2.rotate (f);
					break;
				case 3:
					cc3.rotate (f);
					break;
				}
			}
			f = Input.GetAxis ("Pitch_4");
			if (f != 0) {
				switch (player4JoyNum) {
				case 1:
					cc1.pitch (f);
					break;
				case 2:
					cc2.pitch (f);
					break;
				case 3:
					cc3.pitch (f);
					break;
				}
			}
			f = Input.GetAxis ("Fire_4");
			if (f != 0) {
				switch (player4JoyNum) {
				case 1:
					cc1.fire (f);
					break;
				case 2:
					cc2.fire (f);
					break;
				case 3:
					cc3.fire (f);
					break;
				}
			}
			//RB
			if (Input.GetKeyDown (KeyCode.Joystick4Button5)) {
				switch (player4JoyNum) {
				case 1:
					cc1.stopFire ();
					break;
				case 2:
					cc2.stopFire ();
					break;
				case 3:
					cc3.stopFire ();
					break;
				}
			}
		}//end p4 else


		//debug player (keyboard). Keyboard emulates some controller!
		if (debugPlayerNum == 4) {
			f = Input.GetAxis ("Horizontal");
			pc1.move (f);
			f = Input.GetAxis ("Jump");
			pc1.jump (f);
		} else {
			f = Input.GetAxis ("Rotate_D");
			if (f != 0) {
				switch (debugPlayerNum) {
				case 1:
					cc1.rotate (f);
					break;
				case 2:
					cc2.rotate (f);
					break;
				case 3:
					cc3.rotate (f);
					break;
				}
			}
			f = Input.GetAxis ("Pitch_D");
			if (f != 0) {
				switch (debugPlayerNum) {
				case 1:
					cc1.pitch (f);
					break;
				case 2:
					cc2.pitch (f);
					break;
				case 3:
					cc3.pitch (f);
					break;
				}
			}
			f = Input.GetAxis ("Fire_D"); //space/left click
			if (f != 0) {
				switch (debugPlayerNum) {
				case 1:
					cc1.fire (f);
					break;
				case 2:
					cc2.fire (f);
					break;
				case 3:
					cc3.fire (f);
					break;
				}
			}
			//RB (right click)
			if (Input.GetKeyDown (KeyCode.Mouse1)) {
				switch (debugPlayerNum) {
				case 1:
					cc1.stopFire ();
					break;
				case 2:
					cc2.stopFire ();
					break;
				case 3:
					cc3.stopFire ();
					break;
				}
			}
		}//end debugger else

		//debugger player switch
		if (Input.GetKeyDown (KeyCode.Alpha1) || Input.GetKeyDown (KeyCode.Keypad1)) {
			print ("debugger is controlling cannon 1");
			debugPlayerNum = 1;
			player1JoyNum = 1;
			debugSwap ();
		}
		if (Input.GetKeyDown (KeyCode.Alpha2) || Input.GetKeyDown (KeyCode.Keypad2)) {
			print ("debugger is controlling cannon 2");
			debugPlayerNum = 2;
			player1JoyNum = 2;
			debugSwap ();
		}
		if (Input.GetKeyDown (KeyCode.Alpha3) || Input.GetKeyDown (KeyCode.Keypad3)) {
			print ("debugger is controlling cannon 3");
			debugPlayerNum = 3;
			player1JoyNum = 3;
			debugSwap ();
		}
		if (Input.GetKeyDown (KeyCode.Alpha4) || Input.GetKeyDown (KeyCode.Keypad4)) {
			print ("debugger is controlling runner");
			debugPlayerNum = 4;
			player1JoyNum = 4;
			debugSwap ();
		}

        //pause
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) ||
            Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.Joystick2Button7) ||
            Input.GetKeyDown(KeyCode.Joystick3Button7) || Input.GetKeyDown(KeyCode.Joystick4Button7))
        {
            if (paused)
            {
                Time.timeScale = 1;
                paused = false;
                pausedText.enabled = false;
            }
            else
            {
                Time.timeScale = 0;
                paused = true;
                pausedText.enabled = true;
            }
        }



    }//end update


	public void debugSwap() {
		int controller = getPlayerJoyNumForController (1);

		float f = Random.value;

		if (f <= 1.0f / 3.0f) {
			player2JoyNum = controller;
		} else if (f > 1.0 / 3.0f && f <= 2.0f / 3.0f) {
			player3JoyNum = controller;
		} else if (f > 2.0 / 3.0f && f <= 3.0f / 3.0f) {
			player4JoyNum = controller;
		}
	}

	public void swapPlayer(int cannonThatFired)//, int otherController)
    {
//        int temp = a;
//        a = b;
//        b = temp;
		// Debug.Log(debugPlayerNum);

		Debug.Log (cannonThatFired);
		// NOTE: controllerThatFired will be a number from (1 - 3)
		Debug.Assert(cannonThatFired > 0 && cannonThatFired < 4);

		// get the player currently controlling the runner
		int playerJoyNum = getPlayerJoyNumForController (4);

		if(playerJoyNum == 1) 
			player1JoyNum = cannonThatFired;
		else if(playerJoyNum == 2) 
			player2JoyNum = cannonThatFired;
		else if(playerJoyNum == 3) 
			player3JoyNum = cannonThatFired;
		else 
			player4JoyNum = cannonThatFired;

		// player who shot the cannon that hit the runner cannot be the player controlling the runner!
		Debug.Assert (playerJoyNum != getPlayerJoyNumForController (cannonThatFired));

		// Get the player currently controlling the cannon that fired.
		playerJoyNum = getPlayerJoyNumForController (cannonThatFired);

		if (playerJoyNum == 1)
			player1JoyNum = 4;
		else if (playerJoyNum == 2)
			player2JoyNum = 4;
		else if (playerJoyNum == 3)
			player3JoyNum = 4;
		else 
			player4JoyNum = 4;

		if (debugPlayerNum == 4)
			debugPlayerNum = cannonThatFired;
		else
			debugPlayerNum = 4;
    }

    public void gameStop()
    {
        Time.timeScale = 0;
        paused = true;
    }

    private void randomizePlayers()
    {
        int num = Random.Range(1, 5);
        print("num: " + num);

        switch(num)
        {
            case 1:
                player1JoyNum = 1;
                player2JoyNum = 2;
                player3JoyNum = 3;
                player4JoyNum = 4;
                break;
            case 2:
                player1JoyNum = 2;
                player2JoyNum = 3;
                player3JoyNum = 4;
                player4JoyNum = 1;
                break;
            case 3:
                player1JoyNum = 3;
                player2JoyNum = 4;
                player3JoyNum = 1;
                player4JoyNum = 2;
                break;
            case 4:
                player1JoyNum = 4;
                player2JoyNum = 1;
                player3JoyNum = 2;
                player4JoyNum = 3;
                break;
            default:
                Debug.Assert(false);
                break;
        }
    }

	public int getDebugPlayerNum() {
		return debugPlayerNum;
	}
}
