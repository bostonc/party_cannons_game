using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupJuice : MonoBehaviour {
    public bool move = true; //check whether or not u want there to be up/down juice
    public Vector3 moveVector = Vector3.up;
    public float moveRange = 1.0f; //decides how far range of movement is
    public float moveSpeed = 0.5f; //decide on how fast to move the object

    private PowerupJuice movingObject;

    Vector3 startPosition;

	// Use this for initialization
	void Start () {
        movingObject = this;
        startPosition = movingObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (move) {
            movingObject.transform.position = startPosition + moveVector * (moveRange * Mathf.Sin(Time.timeSinceLevelLoad * moveSpeed));
        }
	}
}