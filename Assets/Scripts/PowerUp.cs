using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    //this script determines the interactions with the powerups
    //attach this script to the runner
    public GameObject runner;

    PlayerControl currentRunner;

    float startTime;
    public float shieldTime;

    bool shieldsUp;

    public int points;

    void Start() {
        shieldsUp = false;
        currentRunner = runner.GetComponent<PlayerControl>();
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
}
