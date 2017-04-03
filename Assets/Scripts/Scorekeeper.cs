using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//at end of game, need to get high score name from player

public class Scorekeeper : MonoBehaviour
{
    static public Scorekeeper S;
    public GameObject scorePopupPrefab;

    public Text highScoreText;

    public Text p1ScoreText;
    public Text p2ScoreText;
    public Text p3ScoreText;
    public Text p4ScoreText;
    public Text timerText;
    public Text endGametext;

    public GameObject cannon1;
    public GameObject cannon2;
    public GameObject cannon3;
    public GameObject runner;

    public float gameDuration = 5; //minutes
    public int restartDuration = 5; //seconds

    public float highscoreFadeDelay = 3f;
    public float highscoreFadeSpeed = 0.02f;

    public bool debugHighscore = false;

    public bool ___________________;

    float endgameTime = 0;
    float sceneLoadTime = 0;
    bool restartTimerStarted = false;
    
    public int p1Score = 0;
    public int p2Score = 0;
    public int p3Score = 0;
    public int p4Score = 0;

    public bool gameOver = false;
    public int highscoreHolder = 0; //0 for none, else player number 1-4
    float highScoreTime = 0f;

    Vector3 c1Pos = Vector3.zero;
    Vector3 c2Pos = Vector3.zero;
    Vector3 c3Pos = Vector3.zero;

	float startTime;

	public float fractionOfGameComplete() {
		return (gameDuration * 60 - (Time.time - startTime)) / (60 * gameDuration);
	}

    private void Awake()
    {
        S = this;
        highScoreText.enabled = false;
        endGametext.enabled = false;
    }

	void setScoreTextColor(InputManager.PlayerID pID, Text scoreText) {
		InputManager.ControlID cID = InputManager.S.getPlayerInfoWithPlayerID (pID).controlID;

		if (cID == InputManager.ControlID.Cannon1)
			scoreText.color = cannon1.GetComponent<CannonControl> ().getMaterial ().color;
		else if (cID == InputManager.ControlID.Cannon2)
			scoreText.color = cannon2.GetComponent<CannonControl> ().getMaterial ().color;
		else if (cID == InputManager.ControlID.Cannon3)
			scoreText.color = cannon3.GetComponent<CannonControl> ().getMaterial ().color;
		else if (cID == InputManager.ControlID.Runner)
			scoreText.color = runner.GetComponent<Renderer> ().material.color;
	}

    // Use this for initialization
    void Start ()
    {
        if (cannon1 != null) c1Pos = cannon1.transform.position;
        if (cannon2 != null) c2Pos = cannon2.transform.position;
        if (cannon3 != null) c3Pos = cannon3.transform.position;

        if (debugHighscore) recordHighScore(1, 5);
		startTime = Time.time;

		setScoreTextColor (InputManager.PlayerID.Player1, p1ScoreText);
		setScoreTextColor (InputManager.PlayerID.Player2, p2ScoreText);
		setScoreTextColor (InputManager.PlayerID.Player3, p3ScoreText);
		setScoreTextColor (InputManager.PlayerID.Player4, p4ScoreText);
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
        updateTimer();
        restartCountdown();

        // print("Time: " + Time.time);
    }

    //player is 1-4
	public void Score(InputManager.PlayerID pID, int amt)
    {
        switch(pID)
        {
			case InputManager.PlayerID.Player1:
                p1Score += amt;
                p1ScoreText.text = "P1: " + p1Score;
                break;
			case InputManager.PlayerID.Player2:
                p2Score += amt;
                p2ScoreText.text = "P2: " + p2Score;
                break;
			case InputManager.PlayerID.Player3:
                p3Score += amt;
                p3ScoreText.text = "P3: " + p3Score;
                break;
			case InputManager.PlayerID.Player4:
                p4Score += amt;
                p4ScoreText.text = "P4: " + p4Score;
                break;
            default:
                // Debug.Assert(false);
                break;
        }
    }

    public void recordHighScore(int player, int amt)
    {
        if (highscoreHolder == player) return;
        //AudioDriver.S.play(SoundType.highScore); THIS CReATES BAD AUDIO BUG ON SWAP, NOT SURE WHY
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
        //TODO!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!



    }

    private void updateTimer()
    {
        float timer = (gameDuration * 60) - Time.timeSinceLevelLoad;
        // print("timer: " + timer);

        if (timer > 0)
        {
            string minutes = Mathf.Floor(timer / 60).ToString("00");
            string seconds = Mathf.Floor(timer % 60).ToString("00");
            timerText.text = minutes + ":" + seconds;
        }
        if (timer <= 0 && !gameOver)
        {
            //GAME OVER
            AudioDriver.S.play(SoundType.gameEnd);

            timerText.text = "00:00";
            gameOver = true;
            collectHighScoreName();
            //InputManager.S.gameStop();

            //game over message
            endgame();

            //restart
            endgameTime = Time.time;
            restartTimerStarted = true;

        }

    }//updateTimer

    private void endgame()
    {
        int winner = 0;
        int winScore = 0;

        if (p1Score > p2Score &&
            p1Score > p3Score &&
            p1Score > p4Score)
        {
            winner = 1;
            winScore = p1Score;
        }
        if (p2Score > p1Score &&
            p2Score > p3Score &&
            p2Score > p4Score)
        {
            winner = 2;
            winScore = p2Score;
        }
        if (p3Score > p1Score &&
            p3Score > p2Score &&
            p3Score > p4Score)
        {
            winner = 3;
            winScore = p3Score;
        }
        if (p4Score > p1Score &&
            p4Score > p2Score &&
            p4Score > p3Score)
        {
            winner = 4;
            winScore = p4Score;
        }

        endGametext.enabled = true;
        endGametext.text = "Round Over\nPlayer " + winner + " wins with " + winScore + " points";

        switch (highscoreHolder)
        {
            case 0:
                break;
            default:
                endGametext.text += "\nas a new HIGH SCORE!";
                break;
        }        
    }//endgame

    //called in update
    private void restartCountdown()
    {
        if (Time.time - endgameTime > restartDuration && restartTimerStarted)
        {
            restart();
        }
    }

    private void restart()
    {
        sceneLoadTime = Time.time;
        endgameTime = 0;
        restartTimerStarted = false;
        p1Score = 0;
        p2Score = 0;
        p3Score = 0;
        p4Score = 0;
        gameOver = false;
        highscoreHolder = 0;
        highScoreTime = 0f;

        print("SCENELOADTIME: " + sceneLoadTime);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public GameObject spawnPopup(string msg, Vector3 loc)
    {
        Instantiate(scorePopupPrefab);
        GameObject popup = Instantiate(scorePopupPrefab);
        ScoreTextPopup st = popup.GetComponent<ScoreTextPopup>();
        st.construct(msg, loc);
        //AudioDriver.S.play(SoundType.score);
        return popup;
    }

}
