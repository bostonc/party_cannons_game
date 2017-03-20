using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallDespawn : MonoBehaviour {
    public float despawnSpeed = .1f;

    Rigidbody rb;


	// Use this for initialization
	void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (gameObject.transform.position.y < -10 || (rb.velocity - Vector3.zero).magnitude < 0.2f ||
			(Mathf.Abs(rb.velocity.x) < 2e2*despawnSpeed && Mathf.Abs(rb.velocity.y) < 2e2*despawnSpeed && 
				Mathf.Abs(rb.velocity.z) < 1e-5*despawnSpeed))
        {
            Destroy(gameObject);
        }
	}
}
