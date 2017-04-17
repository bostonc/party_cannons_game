using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class PowerupObjectWithData {
	public GameObject go;
	public float prob; // say probabibility of THIS powerup spawning among all the others, given that a powerup is 
					   // is to be spawned (Posterior Probability, P(This type of Powerup | Powerup spawned.)
					   // Better if these add up to 1.
	public bool isRunnerPowerUp;
}

public class PowerUp : MonoBehaviour {
    //this script determines the interactions with the powerups
    //attach this script to the runner
    public GameObject runner;

    PlayerControl currentRunner;

	Dictionary <string, bool> powerUpState;

	// Probability of a powerup spawning (P(Power Spawning))
	public float probOfPowerupSpawning; 
	public PowerupObjectWithData[] powerups;

	public static PowerUp S; 

	float defaultRunnerSpeed;
	float defaultJumpPower;

    void Start() {
        currentRunner = runner.GetComponent<PlayerControl>();
		defaultRunnerSpeed = currentRunner.speed;
		defaultJumpPower = currentRunner.jumpPower;
		powerUpState = new Dictionary<string, bool> ();
		foreach(PowerupObjectWithData pod in powerups) {
			powerUpState.Add (pod.go.tag, false);
		}
		if (S == null) {
			S = this;
		}
    }

    void Update() {
		runner.layer = powerUpState ["Shield"] ? LayerMask.NameToLayer ("Invincible") :  LayerMask.NameToLayer("Default");

		if (powerUpState ["SlowDown"]) {
			currentRunner.speed = defaultRunnerSpeed * 0.5f;
		} else if (powerUpState ["SpeedUp"]) {
			currentRunner.speed = defaultRunnerSpeed * 2.0f;
		} else {
			currentRunner.speed = defaultRunnerSpeed;
		}

		if (powerUpState ["JumpHigh"]) {
			currentRunner.jumpPower = defaultJumpPower * 2;
		} else {
			currentRunner.jumpPower = defaultJumpPower;
		}


		CannonControl.freeze = powerUpState ["FreezeCannon"];
	}

    void OnCollisionEnter(Collision coll) {
		// Note: The way SlowDown/SpeedUp work is that they are not accumulative. Getting one, negates the other's effects
		// (That is, if SlowDown was collected recently and is still active, collecting SpeedUp removes the SlowDown 
		// effect and vice versa.) All other powerups are in a sense accumulative (except Points of course.)
		if (coll.gameObject.tag == "SlowDown") {
			AudioDriver.S.play (SoundType.powerup);
			powerUpState ["SpeedUp"] = false; // Negate SpeedUp regardless of whether it is active.
			// Give twenty points for deciding to move slower. Isn't this a disadvantageous powerup otherwise?
			Scorekeeper.S.Score (InputManager.S.getPlayerInfoWithControlID (InputManager.ControlID.Runner).playerID, 20);
			Scorekeeper.S.spawnPopup ("Slow Down, +20!", gameObject.transform.position);
			StartCoroutine (EnablePowerup (coll.gameObject.tag));
			Destroy (coll.gameObject);     
		} else if (coll.gameObject.tag == "SpeedUp") {
			AudioDriver.S.play (SoundType.powerup);
			powerUpState ["SlowDown"] = false; // Negate SlowDown regardless of whether it is active.
			Scorekeeper.S.spawnPopup ("Speed Up!", gameObject.transform.position);
			StartCoroutine (EnablePowerup (coll.gameObject.tag));
			Destroy (coll.gameObject);
		} else if (coll.gameObject.tag == "JumpHigh") {
			AudioDriver.S.play (SoundType.powerup);
			Scorekeeper.S.spawnPopup ("Jump Higher!", gameObject.transform.position);
			StartCoroutine (EnablePowerup (coll.gameObject.tag));
			Destroy (coll.gameObject);
		} else if (coll.gameObject.tag == "Points") {
			AudioDriver.S.play (SoundType.powerup);
			Scorekeeper.S.Score (InputManager.S.getPlayerInfoWithControlID (InputManager.ControlID.Runner).playerID, 50);
			Scorekeeper.S.spawnPopup ("+50!", gameObject.transform.position);
			Destroy (coll.gameObject);
			// No need to do call EnablePowerup as coroutine (Points has no side effects on player variables.)
		} else if (coll.gameObject.tag == "Shield") {
			AudioDriver.S.play (SoundType.powerup);
			Scorekeeper.S.spawnPopup ("Shields Up!", gameObject.transform.position);
			StartCoroutine (EnablePowerup (coll.gameObject.tag));
			Destroy (coll.gameObject);
		} else if (coll.gameObject.tag == "FreezeCannon") {
			AudioDriver.S.play (SoundType.powerup);
			Scorekeeper.S.spawnPopup ("Freeze Cannons!", gameObject.transform.position);
			StartCoroutine (EnablePowerup (coll.gameObject.tag));
			Destroy (coll.gameObject);
		}
    }

	public GameObject getPowerUpInstanceForSpawning() {
		float powerUpSpawning = Random.Range (0.0f, 1.0f);
		if (powerUpSpawning > probOfPowerupSpawning) {
			return null;
		} else {
			float x = Random.Range (0.0f, 1.0f);
			List<PowerupObjectWithData> sortedPowerupObjectWithDataList = powerups.OrderBy (o => o.prob).ToList ();
			float cumulativeProb = 0.0f;
			foreach (PowerupObjectWithData pod in sortedPowerupObjectWithDataList) {
				// x is larger than the probability of choosing this item.
				cumulativeProb += pod.prob;
				if (x > cumulativeProb) {
					continue;
				} else {
					return pod.go;
				}
			}
			return null;
		}
	}

	// Default 2 seconds for powerups.
	IEnumerator EnablePowerup(string powerup, float time = 5.0f) {
		Debug.Log (powerup);
		powerUpState [powerup] = true;

		yield return new WaitForSeconds (time);

		powerUpState [powerup] = false;
	}

	public void resetPowerUpStates() {
		foreach (string key in powerUpState.Keys.ToList()){
			powerUpState[key] = false;
		}
	}
}
