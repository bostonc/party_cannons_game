using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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
    public GameObject runner; //4

    public Text pausedText;

    public bool paused = false;

    public bool _________________;

    public enum AIMode {
		None, Off, On // Can add more here (like AI with different difficulties)
    }

	public enum PlayerID {
		None, Player1, Player2, Player3, Player4, Debug
	};

	public enum ControlID {
		None, Cannon1, Cannon2, Cannon3, Runner
	};

	public class PlayerInfo {
		public PlayerID playerID;
		public ControlID controlID;
    	public AIMode aiMode;

		public PlayerInfo(PlayerID pID, ControlID cID,
                          AIMode _aiMode) {
			playerID = pID;
			controlID = cID;
     		aiMode = _aiMode;
		}
	};

	public List<PlayerInfo> playerInfoList = new List<PlayerInfo>() {
		new PlayerInfo(PlayerID.Player1, ControlID.Cannon1, AIMode.On),
		new PlayerInfo(PlayerID.Player2, ControlID.Cannon2, AIMode.On),
		new PlayerInfo(PlayerID.Player3, ControlID.Cannon3, AIMode.On),
		new PlayerInfo(PlayerID.Player4, ControlID.Runner , AIMode.On),
	};
	// public int debugPlayerNum = -1; //who is the keyboard controlling?
	public bool debugAllow = true;

    CannonControl cc1;
    CannonControl cc2;
    CannonControl cc3;
	PlayerControl pc1;

    private void Awake()
    {
        S = this;
    }

	// get the JoyNum controlling a given cannon (1 - 3) or runner (4)
//	public int getPlayerJoyNumForController(int controller) {
//		if(player1JoyNum == controller)
//			return 1;
//		else if(player2JoyNum == controller)
//			return 2;
//		else if(player3JoyNum == controller)
//			return 3;
//		else
//			return 4;
//	}

	public PlayerInfo getPlayerInfoWithControlID(ControlID cID) {
		foreach (PlayerInfo pcm in playerInfoList) {
			if (pcm.controlID == cID)
				return new PlayerInfo (pcm.playerID, pcm.controlID, pcm.aiMode);
		}
		Debug.Log (cID);
		Debug.Assert (false); // Controller does not exist! !!WARNING!!
		return new PlayerInfo(PlayerID.None, ControlID.None, AIMode.None);
	}

	public PlayerInfo getPlayerInfoWithPlayerID(PlayerID pID) {
		PlayerID correctedPlayerID = (pID == PlayerID.Debug) ? PlayerID.Player1 : pID;
		foreach (PlayerInfo pcm in playerInfoList) {
			if (pcm.playerID == correctedPlayerID)
				return new PlayerInfo (pcm.playerID, pcm.controlID, pcm.aiMode);
		}

		return new PlayerInfo(PlayerID.None, ControlID.None, AIMode.None); // Player does not exist! Not controlling anything.
	}

    // Use this for initialization
    void Start ()
    {
        cc1 = cannon1.GetComponent<CannonControl>();
        cc2 = cannon2.GetComponent<CannonControl>();
        cc3 = cannon3.GetComponent<CannonControl>();
		pc1 = runner.GetComponent<PlayerControl>();

        pausedText.enabled = false;

        string[] Joysticks = Input.GetJoystickNames();
		if (Joysticks.Length > 0) { // At least 1 controller attached. No need for Keyboard (Debug) Controller.
			debugAllow = false;
			randomizePlayers(Joysticks.Length);
		} else { // No controllers attached!
			debugAllow = true;
			randomizePlayers (1);
			// debugPlayerNum = getPlayerJoyNumForController(1); // whatever player 1 is controlling, control that.
		}
    }

	float OsBasedGetAxis(string axis) {
		if ((Application.platform == RuntimePlatform.OSXEditor) ||
		    (Application.platform == RuntimePlatform.OSXPlayer))
			return CalibratedGetAxis (axis + "_OSX");
		else
			return CalibratedGetAxis (axis);
	}

	float CalibratedGetAxis(string axis) {
		float f = Input.GetAxis (axis);
		return (Mathf.Abs (f) > 0.1f) ? f : 0.0f; // Safety mechanism to stop leakage of values
												  // (dead range could be incorrectly set.)
	}

	void UpdateHelper(PlayerID pID) {
		float yaw = 0.0f;
		float pitch = 0.0f;
		float firing = 0.0f;

		if (getPlayerInfoWithPlayerID (pID).controlID == ControlID.None)
			return;

		if (pID == PlayerID.Debug) {
			yaw = CalibratedGetAxis ("Rotate_D"); // Debug same on OSX and Windows.
			pitch = CalibratedGetAxis ("Pitch_D"); // Debug same on OSX and Windows.
			firing = CalibratedGetAxis ("Fire_D"); // Debug same on OSX and Windows.
		} else if (pID == PlayerID.Player1) {
			yaw = CalibratedGetAxis ("Rotate_1"); // Rotation axis same on OSX and Windows.
			pitch = OsBasedGetAxis ("Pitch_1");
			firing = OsBasedGetAxis ("Fire_1");
		} else if (pID == PlayerID.Player2) {
			yaw = CalibratedGetAxis ("Rotate_2"); // Rotation axis same on OSX and Windows.
			pitch = OsBasedGetAxis ("Pitch_2");
			firing = OsBasedGetAxis ("Fire_2");
		} else if (pID == PlayerID.Player3) {
			yaw = CalibratedGetAxis ("Rotate_3"); // Rotation axis same on OSX and Windows.
			pitch = OsBasedGetAxis ("Pitch_3");
			firing = OsBasedGetAxis ("Fire_3");
		} else if (pID == PlayerID.Player4) {
			yaw = CalibratedGetAxis ("Rotate_4"); // Rotation axis same on OSX and Windows.
			pitch = OsBasedGetAxis ("Pitch_4");
			firing = OsBasedGetAxis ("Fire_4");
		} else {
			return; // PlayerID.AI;
		}

		switch(getPlayerInfoWithPlayerID(pID).controlID) {
		case ControlID.Cannon1:
			if (yaw != 0)
				cc1.rotate (yaw);
			if (pitch != 0)
				cc1.pitch (pitch);
			if (firing != 0)
				cc1.fire (firing);
			break;
		case ControlID.Cannon2:
			if (yaw != 0)
				cc2.rotate (yaw);
			if (pitch != 0)
				cc2.pitch (pitch);
			if (firing != 0)
				cc2.fire (firing);
			break;
		case ControlID.Cannon3:
			if (yaw != 0)
				cc3.rotate (yaw);
			if (pitch != 0)
				cc3.pitch (pitch);
			if (firing != 0)
				cc3.fire (firing);
			break;
		case ControlID.Runner:
			if (yaw != 0)
				pc1.move (yaw);
			if (firing != 0)
				pc1.jump (firing);
			break;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if (Scorekeeper.S.gameOver)
			return;

		UpdateHelper (PlayerID.Player1);
		UpdateHelper (PlayerID.Player2);
		UpdateHelper (PlayerID.Player3);
		UpdateHelper (PlayerID.Player4);

// Previous Stop Firing Code. TODO: Incorporate Above if Needed.
//			if (Input.GetKeyDown (KeyCode.Joystick1Button5)) {
//				switch (player1JoyNum) {
//				case 1:
//					cc1.stopFire ();
//					break;
//				case 2:
//					cc2.stopFire ();
//					break;
//				case 3:
//					cc3.stopFire ();
//					break;
//				}
//			}

		// Special Case! No controllers attached. Keyboard axes used.
		// Debug player (keyboard). Keyboard emulates Player 1 (PlayerID.Player1)!

		if (debugAllow) {
			UpdateHelper (PlayerID.Debug);
			// else {
//				//RB (right click)
//				if (Input.GetKeyDown (KeyCode.Mouse1)) {
//					switch (debugPlayerNum) {
//					case 1:
//						cc1.stopFire ();
//						break;
//					case 2:
//						cc2.stopFire ();
//						break;
//					case 3:
//						cc3.stopFire ();
//						break;
//					}
			//}end debugger else

			//debugger player switch
			if (Input.GetKeyDown (KeyCode.Alpha1) || Input.GetKeyDown (KeyCode.Keypad1)) {
				print ("debugger is controlling cannon 1");
				debugSwap (ControlID.Cannon1);
			}
			if (Input.GetKeyDown (KeyCode.Alpha2) || Input.GetKeyDown (KeyCode.Keypad2)) {
				print ("debugger is controlling cannon 2");
				debugSwap (ControlID.Cannon2);
			}
			if (Input.GetKeyDown (KeyCode.Alpha3) || Input.GetKeyDown (KeyCode.Keypad3)) {
				print ("debugger is controlling cannon 3");
				debugSwap (ControlID.Cannon3);
			}
			if (Input.GetKeyDown (KeyCode.Alpha4) || Input.GetKeyDown (KeyCode.Keypad4)) {
				print ("debugger is controlling runner");
				debugSwap (ControlID.Runner);
			}
		}

        //pause
		if (Input.GetButtonDown("Pause_D") ||
			Input.GetButtonDown("Pause_J") || 
			((Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer) && 
			  Input.GetButtonDown("Pause_J_OSX"))) 	
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


	public void debugSwap(ControlID desiredCID) {
		ControlID currentCID = getPlayerInfoWithPlayerID (PlayerID.Debug).controlID;

		float f = Random.value;

		if (f <= 1.0f / 3.0f) {
			if(getPlayerInfoWithPlayerID(PlayerID.Player2).controlID == ControlID.None)
				setPlayerInfoForControlID (currentCID, PlayerID.Player2, AIMode.On);
			else
				setPlayerInfoForControlID (currentCID, PlayerID.Player2, AIMode.Off);
		} else if (f > 1.0 / 3.0f && f <= 2.0f / 3.0f) {
			if(getPlayerInfoWithPlayerID(PlayerID.Player3).controlID == ControlID.None)
				setPlayerInfoForControlID (currentCID, PlayerID.Player3, AIMode.On);
			else
				setPlayerInfoForControlID (currentCID, PlayerID.Player3, AIMode.Off);
		} else if (f > 2.0 / 3.0f && f <= 3.0f / 3.0f) {
			if(getPlayerInfoWithPlayerID(PlayerID.Player4).controlID == ControlID.None)
				setPlayerInfoForControlID (currentCID, PlayerID.Player4, AIMode.On);
			else
				setPlayerInfoForControlID (currentCID, PlayerID.Player4, AIMode.On);
		}

		setPlayerInfoForControlID (desiredCID, PlayerID.Player1, AIMode.Off);
	}

	public void swapPlayer(ControlID cID)//, int otherController)
    {
        //        int temp = a;
        //        a = b;
        //        b = temp;
        // Debug.Log(debugPlayerNum);
        AudioDriver.S.play(SoundType.swap);

		Debug.Log (cID);
		// NOTE: controllerThatFired will be a number from (1 - 3)
		Debug.Assert(cID != ControlID.None && cID != ControlID.Runner);

		// get the player currently controlling the runner
		PlayerInfo playerControllingRunnerInfo = getPlayerInfoWithControlID(ControlID.Runner);
		PlayerInfo playerControllingCannonThatFiredInfo = getPlayerInfoWithControlID (cID);

		setPlayerInfoForControlID (cID, playerControllingRunnerInfo.playerID, playerControllingRunnerInfo.aiMode);

		// player who shot the cannon that hit the runner cannot be the player controlling the runner!
		Debug.Assert (playerControllingRunnerInfo.playerID != playerControllingCannonThatFiredInfo.playerID);

		// Get the player currently controlling the cannon that fired.

		setPlayerInfoForControlID (ControlID.Runner, playerControllingCannonThatFiredInfo.playerID, 
								   playerControllingCannonThatFiredInfo.aiMode);
//		if (debugPlayerNum == 4)
//			debugPlayerNum = cannonThatFired;
//		else
//			debugPlayerNum = 4;
    }

    public void gameStop()
    {
        Time.timeScale = 0;
        paused = true;
    }

	public void setPlayerInfoForControlID (ControlID cID, PlayerID pID, AIMode aiMode) {
//		if(playerControllerMappings.Where  (pcm => pcm.playerID == pID).ToList().Count == 0)
//			playerControllerMappings.Where (pcm => pcm.controlID == cID).First ().playerID = PlayerID.AI;
//		else
		PlayerInfo pInfo = playerInfoList.Where (pcm => pcm.controlID == cID).First ();
		pInfo.playerID = pID;
		pInfo.aiMode = aiMode;
	}

	private void randomizePlayers(int numOfPlayers)
    {
		List<ControlID> cIDList = new List<ControlID> () {
			ControlID.Cannon1,
			ControlID.Cannon2,
			ControlID.Cannon3,
			ControlID.Runner
		};

		List<PlayerID> pIDList = new List<PlayerID> () {
			PlayerID.Player1,
			PlayerID.Player2,
			PlayerID.Player3,
			PlayerID.Player4
		};

		int i;
		for(i = 0; i < numOfPlayers; i++) {
			int idx = Random.Range (0, cIDList.Count);
			ControlID cID = cIDList[idx];
			cIDList.RemoveAt (idx);
			setPlayerInfoForControlID (cID, pIDList[i], AIMode.Off);
		}

		foreach (ControlID cID in cIDList) {
			setPlayerInfoForControlID (cID, pIDList[i++], AIMode.On);
		}
    }

//	public int getDebugPlayerNum() {
//		return debugPlayerNum;
//	}
}
