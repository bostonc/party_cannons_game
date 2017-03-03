using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//at end of game, need to get high score name from player

public class Scorekeeper : MonoBehaviour
{
    static public Scorekeeper S;

    public Text highScoreText;

    public Text p1ScoreText;
    public Text p2ScoreText;
    public Text p3ScoreText;
    public Text p4ScoreText;

    public GameObject cannon1;
    public GameObject cannon2;
    public GameObject cannon3;
    public GameObject runner;

    public float highscoreFadeDelay = 3f;
    public float highscoreFadeSpeed = 0.02f;

    public bool debugHighscore = false;

    public bool ___________________;

    int p1Score = 0;
    int p2Score = 0;
    int p3Score = 0;
    int p4Score = 0;

    public int highscoreHolder = 0; //0 for none, else player number 1-4

    float highScoreTime = 0f;

    Vector3 c1Pos = Vector3.zero;
    Vector3 c2Pos = Vector3.zero;
    Vector3 c3Pos = Vector3.zero;

    private void Awake()
    {
        S = this;
        highScoreText.enabled = false;
    }

    // Use this for initialization
    void Start ()
    {
        if (cannon1 != null) c1Pos = cannon1.transform.position;
        if (cannon2 != null) c2Pos = cannon2.transform.position;
        if (cannon3 != null) c3Pos = cannon3.transform.position;

        if (debugHighscore) recordHighScore(1, 5);
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

        highScoreFade();
    }

    //player is 1-4
    public void Score(int player, int amt)
    {
        switch(player)
        {
            case 1:
                p1Score += amt;
                p1ScoreText.text = "P1: " + p1Score;
                break;
            case 2:
                p2Score += amt;
                p2ScoreText.text = "P2: " + p2Score;
                break;
            case 3:
                p3Score += amt;
                p3ScoreText.text = "P3: " + p3Score;
                break;
            case 4:
                p4Score += amt;
                p4ScoreText.text = "P4: " + p4Score;
                break;
            default:
                Debug.Assert(false);
                break;
        }
    }

    public void recordHighScore(int player, int amt)
    {
        if (highscoreHolder == player) return;
        PlayerPrefs.SetInt("highScore", p4Score);
        highScoreText.enabled = true;
        highScoreTime = Time.time;
        highscoreHolder = player;
    }

    private void highScoreFade()
    {
        if (Time.time - highScoreTime > highscoreFadeDelay && highScoreText.color.a > 0)
        {
            Color c = highScoreText.color;
            c.a = c.a - highscoreFadeSpeed;
            highScoreText.color = c;
            if (highScoreText.color.a < 0)
            {
                c.a = 0;
                highScoreText.color = c;
            }
        }
    }

    public void collectHighScoreName()
    {
        //TODO
    }



}
