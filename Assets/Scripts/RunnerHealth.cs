using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerHealth : MonoBehaviour {

    public GameObject runner;
    public int runnerHealth;

	void Awake () {
        // initialize to 10 for now
        runnerHealth = 1;
	}

    void Start() {

    }
	
	void Update () {
		
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
        }

		if(runnerHealth <= 0 && collision.gameObject.name.Contains("CannonBall")) {
			if (InputManager.S.getDebugPlayerNum () == PlayerControl.S.controller) {
				InputManager.S.swapPlayer (collision.gameObject.GetComponent<CannonBallMetadata> ().controllerThatFired ());
			} else {
				InputManager.S.swapPlayer (PlayerControl.S.controller);
			}

			Material runnerMaterial = runner.GetComponent<MeshRenderer> ().material;
			runner.GetComponent<MeshRenderer>().material = collision.gameObject.GetComponent<Material> ();
			collision.gameObject.GetComponent<CannonBallMetadata> ().setCannonControlMaterial (runnerMaterial);
		}

    }
}
