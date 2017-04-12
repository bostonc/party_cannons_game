using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallDespawn : MonoBehaviour {
	public float despawnSpeed = .1f;

	bool coroutineStarted;

	Rigidbody rb;


	// Use this for initialization
	void Start ()
	{
		rb = gameObject.GetComponent<Rigidbody>();
		coroutineStarted = false;
	}

	// Update is called once per frame
	void Update ()
	{
		gameObject.transform.localScale = gameObject.transform.localScale * 0.99f;
		if (gameObject.transform.position.y < -10 || (rb.velocity - Vector3.zero).magnitude < 0.2f ||
			(Mathf.Abs(rb.velocity.x) < 2e2*despawnSpeed && Mathf.Abs(rb.velocity.y) < 2e2*despawnSpeed && 
				Mathf.Abs(rb.velocity.z) < 1e-5*despawnSpeed))  // + Safety Check Condition
			StartCoroutine(PlayEffect());
	}

	public void OnCollisionEnter(Collision coll) {
		if (coroutineStarted)
			return;
		if (coll.gameObject.name == "Backdrop" || coll.gameObject.name == "Runner" ||
		    	coll.gameObject.name.Contains ("Platform") || (coll.gameObject.transform.parent != null &&
				coll.gameObject.transform.parent.name.Contains("Map"))) {
				StartCoroutine (PlayEffect ());
		}
	}

	IEnumerator PlayEffect() {
		if (coroutineStarted)
			yield break;
		else
			coroutineStarted = true;
		ParticleSystem ps = gameObject.GetComponent<ParticleSystem> ();
		ParticleSystem.EmissionModule em = ps.emission;
		em.enabled = true;
		gameObject.GetComponent<ParticleSystem> ().time = 0.0f;
		gameObject.GetComponent<MeshRenderer>().enabled = false;
		ps.Play ();
		Destroy(gameObject, ps.main.duration);
	}
}
