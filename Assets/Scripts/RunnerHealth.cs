﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerHealth : MonoBehaviour {

    public GameObject runner;
    public int runnerHealth;
    public float invincibilityPeriod = 3f; //seconds

	int scoreAccumulation = 0;

	float lastAccumulationTime = -1;

	static bool inSwitch;
	int switchFrame = 0;
	int totalSwitchFrames = 50;
    float timeLastSwap = 0; //time since runner was last swapped

	public static bool isInSwitch() {
		return inSwitch;
	}

	void Awake () {
        // initialize to 1 for now
        runnerHealth = 1;
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

			inSwitch = true;

			// Destroy all cannonballs when runner is killed
			var cannonBalls = GameObject.FindGameObjectsWithTag ("Projectile");
			for (var i = 0; i < cannonBalls.Length; i++) {
				Destroy (cannonBalls [i]);
			}

			Debug.Log ("OnCollisionEnter" + Time.time);
			// Debug.Log (InputManager.S.getDebugPlayerNum () + " " + PlayerControl.S.controller);
			InputManager.S.swapPlayer (collision.gameObject.GetComponent<CannonBallMetadata> ().controllerThatFired ());
            timeLastSwap = Time.time;

			Material runnerMaterial = new Material (runner.GetComponent<SpriteRenderer> ().material);
			setMaterial(collision.gameObject.GetComponent<CannonBallMetadata> ().getCannonControlMaterial ());
			collision.gameObject.GetComponent<CannonBallMetadata> ().setCannonControlMaterial (runnerMaterial);
		}

		runnerHealth = 1;
    }

	private void setMaterial(Material mat) {
		StartCoroutine(Blink(1.0f, runner.GetComponent<SpriteRenderer>().material, mat));
	}

	private void trueSetMaterial(Material mat) {
		runner.GetComponent<SpriteRenderer> ().material = mat;
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
}
