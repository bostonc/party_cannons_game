using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTracker : MonoBehaviour {

    public int num_rounds;
    public int p1_score;
    public int p2_score;
    public int p3_score;
    public int p4_score;

    static public RoundTracker R;


    private void Awake()
    {
        
        R = this;

        DontDestroyOnLoad(R);


    }

    // Use this for initialization
    void Start () {
        num_rounds = 0;
        p1_score = 0;
        p2_score = 0;
        p3_score = 0;
        p4_score = 0;
    }
	
	// Update is called once per frame
	void Update () {
		if (num_rounds == 4)
        {
            //do something
            Debug.Log("4 rounds have passed");
        }
	}

    public void updateScore(int pnum, int score)
    {
        switch (pnum)
        {
            case 1:
                p1_score += score;
                break;
            case 2:
                p2_score += score;
                break;
            case 3:
                p3_score += score;
                break;
            case 4:
                p4_score += score;
                break;
        }
    }
    
    public void updateRound()
    {
        num_rounds += 1;
    }

    public void restart()
    {
        if (num_rounds > 0)
        {
            num_rounds -= 1;
        }
    }
}
