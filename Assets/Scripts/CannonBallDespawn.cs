using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallDespawn : MonoBehaviour {
    public float despawnSpeed = .05f;

    Rigidbody rb;


	// Use this for initialization
	void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (gameObject.transform.position.y < -10 || rb.velocity == Vector3.zero ||
            (rb.velocity.x < despawnSpeed && rb.velocity.y < despawnSpeed && rb.velocity.z < despawnSpeed))
        {
            Destroy(gameObject);
        }
	}
}
