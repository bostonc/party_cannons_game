using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerHealth : MonoBehaviour {

    public GameObject runner;
    public int runnerHealth;

	int scoreAccumulation = 0;

	float lastAccumulationTime = -1;

	void Awake () {
        // initialize to 1 for now
        runnerHealth = 1;
	}

    void Start() {

    }
	
	void Update () {
		if (Scorekeeper.S.gameOver)
			return; // Don't update scores anymore if game is over.
		scoreAccumulation += (int)(10 * Scorekeeper.S.fractionOfGameComplete ());
		if(Time.time - lastAccumulationTime > 0.1f) {
			Scorekeeper.S.Score (InputManager.S.getPlayerJoyNumForController (4), scoreAccumulation);
			scoreAccumulation = 0;
			lastAccumulationTime = Time.time;
		}
	}

    // when the runner collides with something
    void OnCollisionEnter(Collision collision) {

        // the runner collides with a powerup
        // TODO: customize the different types of powerups...
        if (collision.gameObject.tag == "PowerUP") {
            // destroy the gameobject
            Destroy(collision.gameObject);
        }

        // the runner collides with a cannon projectile
        // TODO: customize the different types of projectiles that the player can be hit with...
        if (collision.gameObject.tag == "Projectile") {
            // destroy the gameobject
            Destroy(collision.gameObject);

            //reduce player health
            runnerHealth--;

            AudioDriver.S.play(SoundType.hitPlayer);
        }

		if(runnerHealth <= 0 && collision.gameObject.name.Contains("CannonBall")) {

			// Destroy all cannonballs when runner is killed
			var cannonBalls = GameObject.FindGameObjectsWithTag ("Projectile");
			for (var i = 0; i < cannonBalls.Length; i++) {
				Destroy (cannonBalls [i]);
			}

			Debug.Log ("OnCollisionEnter" + Time.time);
			Debug.Log (InputManager.S.getDebugPlayerNum () + " " + PlayerControl.S.controller);
			InputManager.S.swapPlayer (collision.gameObject.GetComponent<CannonBallMetadata> ().controllerThatFired ());

			Material runnerMaterial = new Material (runner.GetComponent<SpriteRenderer> ().material);
			runner.GetComponent<SpriteRenderer> ().material = collision.gameObject.GetComponent<CannonBallMetadata> ().getCannonControlMaterial ();
			collision.gameObject.GetComponent<CannonBallMetadata> ().setCannonControlMaterial (runnerMaterial);
		}

		runnerHealth = 1;
    }
}
