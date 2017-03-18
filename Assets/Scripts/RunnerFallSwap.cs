using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerFallSwap : MonoBehaviour
{
    public int fallenDetectionDistance = -10;

    public GameObject runner;
    public GameObject cannon_1;
    public GameObject cannon_2;
    public GameObject cannon_3;

	// Prefer use of PlayerID/ControlID enums wherever possible (over integers or other numeric types).
	InputManager.PlayerID playerWithLowestScore = 0;

    // Update is called once per frame
    void Update ()
    {
        positionCheck();
	}

    void positionCheck()
    {
        List<int> scoreList = new List<int>();
        scoreList.Add(Scorekeeper.S.p1Score);
        scoreList.Add(Scorekeeper.S.p2Score);
        scoreList.Add(Scorekeeper.S.p3Score);
        scoreList.Add(Scorekeeper.S.p4Score);
        scoreList.Sort();

        //if runner has fallen
        if (runner.transform.position.y < fallenDetectionDistance)
        {
            //who has the lowest score?
            if (Scorekeeper.S.p1Score < Scorekeeper.S.p2Score &&
                Scorekeeper.S.p1Score < Scorekeeper.S.p3Score &&
                Scorekeeper.S.p1Score < Scorekeeper.S.p4Score)
            {
				playerWithLowestScore = InputManager.PlayerID.Player1;
            }
            else if (Scorekeeper.S.p2Score < Scorekeeper.S.p1Score &&
                Scorekeeper.S.p2Score < Scorekeeper.S.p3Score &&
                Scorekeeper.S.p2Score < Scorekeeper.S.p4Score)
            {
				playerWithLowestScore = InputManager.PlayerID.Player2;
            }
            else if (Scorekeeper.S.p3Score < Scorekeeper.S.p1Score &&
                Scorekeeper.S.p3Score < Scorekeeper.S.p2Score &&
                Scorekeeper.S.p3Score < Scorekeeper.S.p4Score)
            {
				playerWithLowestScore = InputManager.PlayerID.Player4;
            }
            else if (Scorekeeper.S.p4Score < Scorekeeper.S.p1Score &&
                Scorekeeper.S.p4Score < Scorekeeper.S.p2Score &&
                Scorekeeper.S.p4Score < Scorekeeper.S.p3Score)
            {
				playerWithLowestScore = InputManager.PlayerID.Player4;
            }

			// All necessary information for a player can be got through a control ID or a player ID by 
			// using the getPlayerInfoWith*ID (where * = Control or Player). ControlID is one of 
			// {Cannon1, Cannon2, Cannon3, Runner}. PlayerID is one of {Player1, Player2, Player3, Player4}.
			// A PlayerInfo object is returned on a call to both of these functions. Objects of this class 
			// contain a PlayerID, ControlID and an AIMode.
			if (InputManager.S.getPlayerInfoWithControlID(InputManager.ControlID.Runner).playerID == 
				playerWithLowestScore)
            {
                //find second lowest score player
                int secondLowestScore = scoreList[1];
                if (secondLowestScore == Scorekeeper.S.p1Score)
                {
					playerWithLowestScore = InputManager.PlayerID.Player1;
                }
                else if (secondLowestScore == Scorekeeper.S.p2Score)
                {
					playerWithLowestScore = InputManager.PlayerID.Player2;
                }
                else if (secondLowestScore == Scorekeeper.S.p3Score)
                {
					playerWithLowestScore = InputManager.PlayerID.Player3;
                }
                else if (secondLowestScore == Scorekeeper.S.p4Score)
                {
					playerWithLowestScore = InputManager.PlayerID.Player4;
                }
            }

            //swap (Get Control ID of player with lowest score. The runner will then take on the cannon 
			// specified, and the player with the lowest score will now take on the runner role.)
			InputManager.S.swapPlayer(InputManager.S.getPlayerInfoWithPlayerID(playerWithLowestScore).controlID);

            runner.GetComponent<PlayerControl>().enabled = false;

            //put runner back on map
            runner.transform.position = new Vector3(0f, 10f, 20f);

            runner.GetComponent<PlayerControl>().enabled = true;
        }
    }

}
