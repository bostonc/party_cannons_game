using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerHealth : MonoBehaviour {
    static public RunnerHealth rh;

    public GameObject runner;
    public int runnerHealth;
    public float invincibilityPeriod = 3f; //seconds

	int scoreAccumulation = 0;

	float lastAccumulationTime = -1;

	static bool inSwitch;
	int switchFrame = 0;
	int totalSwitchFrames = 50;
    float timeLastSwap = 0; //time since runner was last swapped

    public GameObject cannon1Sprite;
    public GameObject cannon2Sprite;
    public GameObject cannon3Sprite;

    public Material player1Material;
    public Material player2Material;
    public Material player3Material;
    public Material player4Material;

    public Sprite p1;
    public Sprite p2;
    public Sprite p3;
    public Sprite p4;

    public Sprite p1_l;
    public Sprite p2_l;
    public Sprite p3_l;
    public Sprite p4_l;


    public static bool isInSwitch() {
		return inSwitch;
	}

	void Awake () {
        rh = this;
        // initialize to 1 for now
        runnerHealth = 1;

        //initialize cannon color
        //get pID from cID
        
        //check cannon1
        

        //check cannon2

        //check cannon3
	}

    void Start() {
		inSwitch = false;
    }
	
	void Update () {

		if (inSwitch) {
			if (switchFrame == totalSwitchFrames) {
				inSwitch = false;
				switchFrame = 0;
				Time.timeScale = 1.0f;
				Time.fixedDeltaTime = 0.02f ;
			} else {
				Time.timeScale = 0.25f;
				Time.fixedDeltaTime = 0.02f * Time.timeScale;
				switchFrame++; 
			}
		}

		if (Scorekeeper.S.gameOver)
			return; // Don't update scores anymore if game is over.
		scoreAccumulation = (int)(1);
		if(Time.time - lastAccumulationTime > 0.5f) {
			Scorekeeper.S.Score (InputManager.S.getPlayerInfoWithControlID(InputManager.ControlID.Runner).playerID, scoreAccumulation);
			scoreAccumulation = 0;
			lastAccumulationTime = Time.time;
		}
	}

    // when the runner collides with something
    void OnCollisionEnter(Collision collision) {

		if (inSwitch && collision.gameObject.name.Contains("CannonBall")) {
			Destroy (collision.gameObject);
			return;
		}

        // the runner collides with a cannon projectile
        // TODO: customize the different types of projectiles that the player can be hit with...
        if (collision.gameObject.tag == "Projectile") {
            // destroy the gameobject
            Destroy(collision.gameObject);

            //reduce player health if not invincible
            if (Time.time - timeLastSwap > invincibilityPeriod)
            {
                runnerHealth--;
            }
            AudioDriver.S.play(SoundType.hitPlayer);
        }

		if(runnerHealth <= 0 && collision.gameObject.name.Contains("CannonBall")) {
			swapHelper(collision.gameObject);

			runnerHealth = 1; 
		}
    }

	private void swapHelper(GameObject CannonballGO) {

		inSwitch = true;

		// Destroy all cannonballs when runner is killed
		var cannonBalls = GameObject.FindGameObjectsWithTag ("Projectile");
		for (var i = 0; i < cannonBalls.Length; i++) {
			Destroy (cannonBalls [i]);
		}

		Debug.Log ("OnCollisionEnter" + Time.time);
		// Debug.Log (InputManager.S.getDebugPlayerNum () + " " + PlayerControl.S.controller);
		// swaps the player controlling the cannon
		InputManager.ControlID cID = CannonballGO.GetComponent<CannonBallMetadata>().controllerThatFired();
		InputManager.PlayerID newRunnerID = InputManager.S.getPlayerIDWithControlID(cID);
		InputManager.PlayerID oldRunnerID = InputManager.S.getCurrentRunnerID();


		//Debug.Log("NEW" + newRunnerID);
		//Debug.Log("OLD" + oldRunnerID);


		//swaps the material of the player - CHANGED TO SWAP SPRITE
		//Material runnerMaterial = new Material (runner.GetComponent<SpriteRenderer> ().material);
		//setMaterial(collision.gameObject.GetComponent<CannonBallMetadata> ().getCannonControlMaterial ());
		//collision.gameObject.GetComponent<CannonBallMetadata> ().setCannonControlMaterial (runnerMaterial);

		//change color of cannon
		switch(oldRunnerID) { //old runner becomes new cannon
		case InputManager.PlayerID.Player1:
			CannonballGO.GetComponent<CannonBallMetadata>().setCannonControlMaterial(player1Material);
			break;
		case InputManager.PlayerID.Player2:
			CannonballGO.GetComponent<CannonBallMetadata>().setCannonControlMaterial(player2Material);
			break;
		case InputManager.PlayerID.Player3:
			CannonballGO.GetComponent<CannonBallMetadata>().setCannonControlMaterial(player3Material);
			break;
		case InputManager.PlayerID.Player4:
			CannonballGO.GetComponent<CannonBallMetadata>().setCannonControlMaterial(player4Material);
			break;
		}

		switch (newRunnerID) { //old runner becomes new cannon
		case InputManager.PlayerID.Player1:
			RunnerAnimation.R.currentColor = RunnerAnimation.ColorSprite.RED;
			break;
		case InputManager.PlayerID.Player2:
			RunnerAnimation.R.currentColor = RunnerAnimation.ColorSprite.BLUE;
			break;
		case InputManager.PlayerID.Player3:
			RunnerAnimation.R.currentColor = RunnerAnimation.ColorSprite.GREEN;
			break;
		case InputManager.PlayerID.Player4:
			RunnerAnimation.R.currentColor = RunnerAnimation.ColorSprite.YELLOW;
			break;
		}

		//Debug.Log (cID);
		//Debug.Log (oldRunnerID);
		//Debug.Log (newRunnerID);

		switch (cID) {
		case InputManager.ControlID.Cannon1:
			switch (oldRunnerID) { //old runner becomes new cannon
			case InputManager.PlayerID.Player1:
				cannon1Sprite.GetComponent<SpriteRenderer>().sprite = p1;
				break;
			case InputManager.PlayerID.Player2:
				cannon1Sprite.GetComponent<SpriteRenderer>().sprite = p2;
				break;
			case InputManager.PlayerID.Player3:
				cannon1Sprite.GetComponent<SpriteRenderer>().sprite = p3;
				break;
			case InputManager.PlayerID.Player4:
				cannon1Sprite.GetComponent<SpriteRenderer>().sprite = p4;
				break;
			}
			break;
		case InputManager.ControlID.Cannon2:
			switch (oldRunnerID) { //old runner becomes new cannon
			case InputManager.PlayerID.Player1:
				cannon2Sprite.GetComponent<SpriteRenderer>().sprite = p1;
				break;
			case InputManager.PlayerID.Player2:
				cannon2Sprite.GetComponent<SpriteRenderer>().sprite = p2;
				break;
			case InputManager.PlayerID.Player3:
				cannon2Sprite.GetComponent<SpriteRenderer>().sprite = p3;
				break;
			case InputManager.PlayerID.Player4:
				cannon2Sprite.GetComponent<SpriteRenderer>().sprite = p4;
				break;
			}
			break;
		case InputManager.ControlID.Cannon3:
			switch (oldRunnerID) { //old runner becomes new cannon
			case InputManager.PlayerID.Player1:
				cannon3Sprite.GetComponent<SpriteRenderer>().sprite = p1_l;
				break;
			case InputManager.PlayerID.Player2:
				cannon3Sprite.GetComponent<SpriteRenderer>().sprite = p2_l;
				break;
			case InputManager.PlayerID.Player3:
				cannon3Sprite.GetComponent<SpriteRenderer>().sprite = p3_l;
				break;
			case InputManager.PlayerID.Player4:
				cannon3Sprite.GetComponent<SpriteRenderer>().sprite = p4_l;
				break;
			}
			break;
		}

		//change sprite related to cannon
		//setSprite()

		//change cat
		//change cat depending on new playerid
		setSprite(RunnerAnimation.R.getSprite(newRunnerID));


		InputManager.S.swapPlayer(cID);
		timeLastSwap = Time.time;
	}

	private void setMaterial(Material mat) {
		StartCoroutine(Blink(1.0f, runner.GetComponent<SpriteRenderer>().material, mat));
	}

	private void trueSetMaterial(Material mat) {
		runner.GetComponent<SpriteRenderer> ().material = mat;
	}

    private void setSprite(Sprite spr) {
        StartCoroutine(Blink(1.0f, runner.GetComponent<SpriteRenderer>().sprite, spr));
    }

    private void trueSetSprite(Sprite spr) {
        runner.GetComponent<SpriteRenderer>().sprite = spr;
    }

    IEnumerator Blink(float waitTime, Material currMaterial, Material nextMaterial) {
		float endTime = Time.time + waitTime;
		while(Time.time < endTime) {
			yield return new WaitForSeconds(0.1f);
			trueSetMaterial (nextMaterial);
			yield return new WaitForSeconds(0.1f);
			trueSetMaterial (currMaterial);
		}
		trueSetMaterial (nextMaterial);
	}

    public IEnumerator Blink(float waitTime, Sprite currSprite, Sprite nextSprite) {
        float endTime = Time.time + waitTime;
        while (Time.time < endTime) {
            yield return new WaitForSeconds(0.1f);
            trueSetSprite(nextSprite);
            yield return new WaitForSeconds(0.1f);
            trueSetSprite(currSprite);
        }
        trueSetSprite(nextSprite);
    }

	void OnParticleCollision(GameObject other) {
		swapHelper (other.transform.parent.gameObject);
	}
}
