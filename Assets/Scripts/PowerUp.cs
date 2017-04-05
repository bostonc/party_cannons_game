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

    float startTime;
    public float shieldTime;

    bool shieldsUp;

    public int points;

	// Probability of a powerup spawning (P(Power Spawning))
	public float probOfPowerupSpawning; 
	public PowerupObjectWithData[] powerups;

	public static PowerUp S; 

    void Start() {
        shieldsUp = false;
        currentRunner = runner.GetComponent<PlayerControl>();
		if (S == null) {
			S = this;
		}
    }

    void Update() {
        if (shieldsUp) {
            if (Time.time - startTime >= shieldTime) {
                //remove "shield"
                runner.layer = LayerMask.NameToLayer("Default");
            }
        }
    }

    void OnCollisionEnter(Collision coll) {
        if (coll.gameObject.tag == "SlowDown") {
            AudioDriver.S.play(SoundType.powerup);
            Scorekeeper.S.spawnPopup("+50!", gameObject.transform.position);
            Destroy(coll.gameObject);            
            currentRunner.speed *= 0.5f;
        }
        else if (coll.gameObject.tag == "SpeedUp") {
            AudioDriver.S.play(SoundType.powerup);
            Scorekeeper.S.spawnPopup("+50!", gameObject.transform.position);
            Destroy(coll.gameObject);
            currentRunner.speed *= 2;
        }
        else if (coll.gameObject.tag == "JumpHigh") {
            AudioDriver.S.play(SoundType.powerup);
            Scorekeeper.S.spawnPopup("+50!", gameObject.transform.position);
            Destroy(coll.gameObject);
            currentRunner.jumpPower *= 2;
        }
        else if (coll.gameObject.tag == "Points") {
            AudioDriver.S.play(SoundType.powerup);
            Scorekeeper.S.spawnPopup("+50!", gameObject.transform.position);
            Destroy(coll.gameObject);
            Scorekeeper.S.Score(InputManager.S.getPlayerInfoWithControlID(InputManager.ControlID.Runner).playerID, points);
        }
        else if (coll.gameObject.tag == "Shield") {
            AudioDriver.S.play(SoundType.powerup);
            Scorekeeper.S.spawnPopup("+50!", gameObject.transform.position);
            Destroy(coll.gameObject);
            runner.layer = LayerMask.NameToLayer("Invincible");
            startTime = Time.time;
            shieldsUp = true;
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
}
