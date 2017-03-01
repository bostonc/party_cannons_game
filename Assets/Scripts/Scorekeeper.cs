using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scorekeeper : MonoBehaviour
{
    static public Scorekeeper S;

    public Text highScoreText;

    public bool ___________________;

    int p1Score = 0;
    int p2Score = 0;
    int p3Score = 0;
    int p4Score = 0;



    private void Awake()
    {
        S = this;
        highScoreText.enabled = false;
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        int highScore = PlayerPrefs.GetInt("highScore");
        if (p1Score > highScore)
        {
            recordHighScore(1, p1Score);   
        }
        if (p2Score > highScore)
        {
            recordHighScore(2, p2Score);
        }
        if (p3Score > highScore)
        {
            recordHighScore(3, p3Score);
        }
        if (p4Score > highScore)
        {
            recordHighScore(4, p4Score);
        }
    }

    //player is 1-4
    void Score(int player, int amt)
    {
        switch(player)
        {
            case 1:
                p1Score += amt;
                break;
            case 2:
                p2Score += amt;
                break;
            case 3:
                p3Score += amt;
                break;
            case 4:
                p4Score += amt;
                break;
            default:
                Debug.Assert(false);
                break;
        }
    }

    void recordHighScore(int player, int amt)
    {
        PlayerPrefs.SetInt("highScore", p4Score);
        highScoreText.enabled = true;

        //need to get player name and record that too


    }


}
