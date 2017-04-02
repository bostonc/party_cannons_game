using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallDespawn : MonoBehaviour {
    public float despawnSpeed = .1f;

	bool collisionEffectComplete = false;

    Rigidbody rb;


	// Use this for initialization
	void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (gameObject.GetComponent<ParticleSystem> ().main.duration - gameObject.GetComponent<ParticleSystem> ().time > 0)
			return;
		else if (collisionEffectComplete || // Collision Effect Done + Duration Over, destory cannonball.
			gameObject.transform.position.y < -10 || (rb.velocity - Vector3.zero).magnitude < 0.2f ||
			(Mathf.Abs(rb.velocity.x) < 2e2*despawnSpeed && Mathf.Abs(rb.velocity.y) < 2e2*despawnSpeed && 
				Mathf.Abs(rb.velocity.z) < 1e-5*despawnSpeed))  // + Safety Check Condition
		{
			Destroy(gameObject);
		}
	}

	public void OnCollisionEnter(Collision coll) {
		if (collisionEffectComplete)
			return;
		if (coll.gameObject.name == "Backdrop" || coll.gameObject.name == "Runner" || 
			coll.gameObject.name.Contains("Platform") || (coll.gameObject.transform.parent != null && 
			coll.gameObject.transform.parent.name == "Map")) {
			ParticleSystem ps = gameObject.GetComponent<ParticleSystem> ();
			ParticleSystem.EmissionModule em = ps.emission;
			em.enabled = true;
			ps.Play ();
			collisionEffectComplete = true;
		}
	}
}
