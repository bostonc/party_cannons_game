using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class End : MonoBehaviour {

    public GameObject p1winner;
    public GameObject p2winner;
    public GameObject p3winner;
    public GameObject p4winner;
    public GameObject allTied;

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
        if (p1Score == p2Score && 
            p2Score == p3Score && 
            p3Score == p4Score)
        {
            allTied.SetActive(true);
            p1winner.SetActive(true);
            p2winner.SetActive(true);
            p3winner.SetActive(true);
            p4winner.SetActive(true);
        }
        if (p1Score == p2Score &&
            p1Score > p3Score &&
            p1Score > p4Score) //p1 and p2 tied
        {
            p1winner.SetActive(true);
            p2winner.SetActive(true);
            allTied.SetActive(true);
        }
        if (p2Score == p3Score &&
            p2Score > p1Score &&
            p2Score > p4Score) //p2 and p3 tied
        {
            p2winner.SetActive(true);
            p3winner.SetActive(true);
            allTied.SetActive(true);
        }
        if (p3Score == p1Score &&
            p3Score > p2Score &&
            p3Score > p4Score) //p3 and p1 tied
        {
            p3winner.SetActive(true);
            p1winner.SetActive(true);
            allTied.SetActive(true);
        }
        if (p4Score == p1Score &&
            p4Score > p2Score &&
            p4Score > p3Score) //p4 and p1 tied
        {
            p4winner.SetActive(true);
            p1winner.SetActive(true);
            allTied.SetActive(true);
        }
        if (p4Score == p3Score &&
            p4Score > p2Score &&
            p4Score > p1Score) //p4 and p3 tied
        {
            p4winner.SetActive(true);
            p3winner.SetActive(true);
            allTied.SetActive(true);
        }
        if (p4Score == p2Score &&
            p4Score > p3Score &&
            p4Score > p1Score) //p4 and p2 tied
        {
            p4winner.SetActive(true);
            p2winner.SetActive(true);
            allTied.SetActive(true);
        }
        if (p4Score == p2Score &&
            p4Score == p3Score &&
            p4Score > p1Score) //p4 and p2 and p3 tied
        {
            p4winner.SetActive(true);
            p2winner.SetActive(true);
            p3winner.SetActive(true);
            allTied.SetActive(true);
        }
        if (p4Score == p2Score &&
            p4Score == p1Score &&
            p4Score > p3Score) //p4 and p2 and p1 tied
        {
            p4winner.SetActive(true);
            p2winner.SetActive(true);
            p1winner.SetActive(true);
            allTied.SetActive(true);
        }
        if (p4Score == p3Score &&
            p4Score == p1Score &&
            p4Score > p2Score) //p4 and p3 and p1 tied
        {
            p4winner.SetActive(true);
            p3winner.SetActive(true);
            p1winner.SetActive(true);
            allTied.SetActive(true);
        }
        if (p1Score == p2Score &&
            p1Score == p3Score &&
            p1Score > p4Score) //p2 and p3 and p1 tied
        {
            p2winner.SetActive(true);
            p3winner.SetActive(true);
            p1winner.SetActive(true);
            allTied.SetActive(true);
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
