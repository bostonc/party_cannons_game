using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public int speed = 10;
	public int jumpPower = 10;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void move(float f)
	{
		transform.Translate (Time.deltaTime * speed * f, 0, 0);
	}

	public void jump(float f)
	{
		if (rb.velocity.y == 0 && f == 1) {
			rb.velocity = new Vector3 (rb.velocity.x, jumpPower, 0);
		}
	}
}
