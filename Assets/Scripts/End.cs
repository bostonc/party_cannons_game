using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class End : MonoBehaviour {

    public GameObject p1winner;
    public GameObject p2winner;
    public GameObject p3winner;
    public GameObject p4winner;

    public Text p1score;
    public Text p2score;
    public Text p3score;
    public Text p4score;

    // Use this for initialization
    void Start () {
        int p1Score = RoundTracker.R.p1_score;
        int p2Score = RoundTracker.R.p2_score;
        int p3Score = RoundTracker.R.p3_score;
        int p4Score = RoundTracker.R.p4_score;

        //int winner = 0;

        if (p1Score > p2Score &&
             p1Score > p3Score &&
             p1Score > p4Score)
        {
            p1winner.SetActive(true);
            //winner = 1;
            //winScore = p1Score;
        }
        if (p2Score > p1Score &&
            p2Score > p3Score &&
            p2Score > p4Score)
        {
            p2winner.SetActive(true);
            //winner = 2;
            //winScore = p2Score;
        }
        if (p3Score > p1Score &&
            p3Score > p2Score &&
            p3Score > p4Score)
        {
            p3winner.SetActive(true);
            //winner = 3;
            //winScore = p3Score;
        }
        if (p4Score > p1Score &&
            p4Score > p2Score &&
            p4Score > p3Score)
        {
            p4winner.SetActive(true);
            //winner = 4;
            //winScore = p4Score;
        }

        p1score.text = (RoundTracker.R.p1_score).ToString();
        p2score.text = (RoundTracker.R.p2_score).ToString();
        p3score.text = (RoundTracker.R.p3_score).ToString();
        p4score.text = (RoundTracker.R.p4_score).ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
