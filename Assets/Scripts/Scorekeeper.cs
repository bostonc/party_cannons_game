using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

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

    public GameObject endPanel;

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
        endPanel.SetActive(false);
    }

	void setScoreTextColor(InputManager.PlayerID pID, Text scoreText) {
		InputManager.PlayerColor clr = InputManager.S.getPlayerInfoWithPlayerID (pID).playerClr;

		if (clr == InputManager.PlayerColor.Blue)
			scoreText.color = (Resources.Load ("BluePlayer") as Material).color;
		else if (clr == InputManager.PlayerColor.Green)
			scoreText.color = (Resources.Load("GreenPlayer") as Material).color;
		else if (clr == InputManager.PlayerColor.Yellow)
			scoreText.color = (Resources.Load("YellowPlayer") as Material).color;
		else if (clr == InputManager.PlayerColor.Red)
			scoreText.color = (Resources.Load("RedPlayer") as Material).color;
	}

    // Use this for initialization
    void Start ()
    {

        RoundTracker.R.updateRound();

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
		setScoreTextColor (InputManager.PlayerID.Player1, p1ScoreText);
		setScoreTextColor (InputManager.PlayerID.Player2, p2ScoreText);
		setScoreTextColor (InputManager.PlayerID.Player3, p3ScoreText);
		setScoreTextColor (InputManager.PlayerID.Player4, p4ScoreText);

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
		Dictionary<InputManager.PlayerID, int> scores = new Dictionary<InputManager.PlayerID, int>{
			{InputManager.PlayerID.Player1, p1Score}, 
			{InputManager.PlayerID.Player2, p2Score}, 
			{InputManager.PlayerID.Player3, p3Score}, 
			{InputManager.PlayerID.Player4, p4Score}
		};

		KeyValuePair<InputManager.PlayerID, int> playerWithHighestScoreInfoAbsolute = scores.OrderByDescending (x => x.Value).First();

		scores.Remove (pID);

		KeyValuePair<InputManager.PlayerID, int> playerWithHighestScoreInfo = scores.OrderByDescending (x => x.Value).First();


        switch(pID)
        {
		case InputManager.PlayerID.Player1:
			p1Score += amt;
			p1ScoreText.text = "P1: " + p1Score;
			if (p1Score > playerWithHighestScoreInfo.Value &&
			     playerWithHighestScoreInfoAbsolute.Key != InputManager.PlayerID.Player1) {

				GameObject go;
				switch (InputManager.S.getPlayerInfoWithPlayerID (InputManager.PlayerID.Player1).controlID) {
				case InputManager.ControlID.Cannon1:
					go = Scorekeeper.S.spawnPopup ("Player 1 takes the lead!", cannon1.gameObject.transform.TransformPoint (3 * Vector3.up)); 
					go.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
					break;
				case InputManager.ControlID.Cannon2:
					go = Scorekeeper.S.spawnPopup ("Player 1 takes the lead!", cannon1.gameObject.transform.TransformPoint (3 * Vector3.up)); 
					go.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
					break;
				case InputManager.ControlID.Cannon3:
					go = Scorekeeper.S.spawnPopup ("Player 1 takes the lead!", cannon1.gameObject.transform.TransformPoint (3 * Vector3.up)); 
					go.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
					break;
				case InputManager.ControlID.Runner:
					go = Scorekeeper.S.spawnPopup ("Player 1 takes the lead!", runner.transform.position); 
					//go.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
					break;
				}
			}
            break;
		case InputManager.PlayerID.Player2:
            p2Score += amt;
            p2ScoreText.text = "P2: " + p2Score;
			if (p2Score > playerWithHighestScoreInfo.Value && 
				playerWithHighestScoreInfoAbsolute.Key != InputManager.PlayerID.Player2) {
				GameObject go;
				switch (InputManager.S.getPlayerInfoWithPlayerID (InputManager.PlayerID.Player2).controlID) {
				case InputManager.ControlID.Cannon1:
					go = Scorekeeper.S.spawnPopup ("Player 2 takes the lead!", cannon1.gameObject.transform.TransformPoint (3 * Vector3.up)); 
					go.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
					break;
				case InputManager.ControlID.Cannon2:
					go = Scorekeeper.S.spawnPopup ("Player 2 takes the lead!", cannon1.gameObject.transform.TransformPoint (3 * Vector3.up)); 
					go.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
					break;
				case InputManager.ControlID.Cannon3:
					go = Scorekeeper.S.spawnPopup ("Player 2 takes the lead!", cannon1.gameObject.transform.TransformPoint (3 * Vector3.up)); 
					go.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
					break;
				case InputManager.ControlID.Runner:
					go = Scorekeeper.S.spawnPopup ("Player 2 takes the lead!", runner.transform.position); 
					//go.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
					break;
				}
			}
            break;
			case InputManager.PlayerID.Player3:
                p3Score += amt;
                p3ScoreText.text = "P3: " + p3Score;
				if (p3Score > playerWithHighestScoreInfo.Value && 
					playerWithHighestScoreInfoAbsolute.Key != InputManager.PlayerID.Player3) {
				GameObject go;
				switch (InputManager.S.getPlayerInfoWithPlayerID (InputManager.PlayerID.Player3).controlID) {
				case InputManager.ControlID.Cannon1:
					go = Scorekeeper.S.spawnPopup ("Player 3 takes the lead!", cannon1.gameObject.transform.TransformPoint (3 * Vector3.up)); 
					go.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
					break;
				case InputManager.ControlID.Cannon2:
					go = Scorekeeper.S.spawnPopup ("Player 3 takes the lead!", cannon1.gameObject.transform.TransformPoint (3 * Vector3.up)); 
					go.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
					break;
				case InputManager.ControlID.Cannon3:
					go = Scorekeeper.S.spawnPopup ("Player 3 takes the lead!", cannon1.gameObject.transform.TransformPoint (3 * Vector3.up)); 
					go.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
					break;
				case InputManager.ControlID.Runner:
					go = Scorekeeper.S.spawnPopup ("Player 3 takes the lead!", runner.transform.position); 
					//go.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
					break;
				}
				}
                break;
			case InputManager.PlayerID.Player4:
                p4Score += amt;
                p4ScoreText.text = "P4: " + p4Score;
				if (p4Score > playerWithHighestScoreInfo.Value && 
					playerWithHighestScoreInfoAbsolute.Key != InputManager.PlayerID.Player4) {
				GameObject go;
				switch (InputManager.S.getPlayerInfoWithPlayerID (InputManager.PlayerID.Player4).controlID) {
				case InputManager.ControlID.Cannon1:
					go = Scorekeeper.S.spawnPopup ("Player 4 takes the lead!", cannon1.gameObject.transform.TransformPoint (3 * Vector3.up)); 
					go.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
					break;
				case InputManager.ControlID.Cannon2:
					go = Scorekeeper.S.spawnPopup ("Player 4 takes the lead!", cannon1.gameObject.transform.TransformPoint (3 * Vector3.up)); 
					go.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
					break;
				case InputManager.ControlID.Cannon3:
					go = Scorekeeper.S.spawnPopup ("Player 4 takes the lead!", cannon1.gameObject.transform.TransformPoint (3 * Vector3.up)); 
					go.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
					break;
				case InputManager.ControlID.Runner:
					go = Scorekeeper.S.spawnPopup ("Player 4 takes the lead!", runner.transform.position); 
					//go.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
					break;
				}
				}
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

        RoundTracker.R.updateScore(1, p1Score);
        RoundTracker.R.updateScore(2, p2Score);
        RoundTracker.R.updateScore(3, p3Score);
        RoundTracker.R.updateScore(4, p4Score);

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
        endPanel.SetActive(true);
        endGametext.enabled = true;
        endGametext.text = "Round Over\nPlayer " + winner + " wins Round " + RoundTracker.R.num_rounds + "/4 with " + winScore + " points";
        endGametext.text += "\n\nGet ready for another round...";

        Debug.Log(RoundTracker.R.num_rounds);

        /*switch (highscoreHolder)
        {
            case 0:
                break;
            default:
                endGametext.text += "\nas a new HIGH SCORE!";
                break;
        }*/
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
        //RoundTracker.R.restart();
    }

    public GameObject spawnPopup(string msg, Vector3 loc)
    {
        GameObject popup = Instantiate(scorePopupPrefab);
        ScoreTextPopup st = popup.GetComponent<ScoreTextPopup>();
        st.construct(msg, loc);
        //AudioDriver.S.play(SoundType.score);
        return popup;
    }

}
