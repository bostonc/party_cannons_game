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

    int RunnerPlayerNumber = 0; //1 - 4
    int lowestScorePlayerNumber = 0; //1 - 4
	
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
            //look through player list for runner
            for (int i = 0; i < 4; i++)
            {
                if (InputManager.S.playerInfoList[i].controlID == InputManager.ControlID.Runner)
                {
                    RunnerPlayerNumber = i;
                }
            }
            //who has the lowest score?
            if (Scorekeeper.S.p1Score < Scorekeeper.S.p2Score &&
                Scorekeeper.S.p1Score < Scorekeeper.S.p3Score &&
                Scorekeeper.S.p1Score < Scorekeeper.S.p4Score)
            {
                lowestScorePlayerNumber = 1;
            }
            else if (Scorekeeper.S.p2Score < Scorekeeper.S.p1Score &&
                Scorekeeper.S.p2Score < Scorekeeper.S.p3Score &&
                Scorekeeper.S.p2Score < Scorekeeper.S.p4Score)
            {
                lowestScorePlayerNumber = 2;
            }
            else if (Scorekeeper.S.p3Score < Scorekeeper.S.p1Score &&
                Scorekeeper.S.p3Score < Scorekeeper.S.p2Score &&
                Scorekeeper.S.p3Score < Scorekeeper.S.p4Score)
            {
                lowestScorePlayerNumber = 3;
            }
            else if (Scorekeeper.S.p4Score < Scorekeeper.S.p1Score &&
                Scorekeeper.S.p4Score < Scorekeeper.S.p2Score &&
                Scorekeeper.S.p4Score < Scorekeeper.S.p3Score)
            {
                lowestScorePlayerNumber = 4;
            }

            if (RunnerPlayerNumber == lowestScorePlayerNumber)
            {
                //find second lowest score player
                int secondLowestScore = scoreList[1];
                if (secondLowestScore == Scorekeeper.S.p1Score)
                {
                    lowestScorePlayerNumber = 1;
                }
                else if (secondLowestScore == Scorekeeper.S.p2Score)
                {
                    lowestScorePlayerNumber = 2;
                }
                else if (secondLowestScore == Scorekeeper.S.p3Score)
                {
                    lowestScorePlayerNumber = 3;
                }
                else if (secondLowestScore == Scorekeeper.S.p4Score)
                {
                    lowestScorePlayerNumber = 4;
                }
            }

            //swap





        }
    }

}
