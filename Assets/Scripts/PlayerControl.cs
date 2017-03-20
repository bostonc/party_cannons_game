using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	static public PlayerControl S;

	public float speed = 10;
	public float jumpPower = 10;

	private Rigidbody rb;

	//For AI control
	private bool onAI = false;
	private int currentX = 0;
	private int desiredX = 0;
	private float moveCount = 0;

	private bool beingReset = false;

	public InputManager.ControlID cID;

	void checkIfAIControlled() {
		if (InputManager.S.getPlayerInfoWithControlID (S.cID).aiMode == InputManager.AIMode.On)
			onAI = true;
		else
			onAI = false;
	}

	void Awake() {
		S = this;
	}

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		beingReset = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (beingReset) {
			checkForResetCompletion ();
		} else {
			checkIfAIControlled ();

			if (onAI) {
				// AI Jump
				int aiJump = Random.Range (0, 50);
				if (aiJump == 0) {
					jump (1.0f);
				}

				// AI Movement
				currentX = (int)transform.position.x;
				int direction = Random.Range (0, 2);
				if (currentX == desiredX || moveCount == 0) {
					moveCount = 50;
					if (direction == 0) {
						desiredX = currentX + 5;
					} else {
						desiredX = currentX - 5;
					}
				} else {
					moveCount = moveCount - 1;
					if (currentX > desiredX) {
						move (-1.0f);
					} else {
						move (1.0f);
					}
				}
			}
		}
	}

	public Vector3 getRunnerPos() {
		return rb.position;
	}

	public void move(float f)
	{
		if (!beingReset) {
			transform.Translate (Time.deltaTime * speed * f, 0, 0);
		}
	}

	public void jump(float f)
	{
		if (!beingReset) {
			if (rb.velocity.y == 0 && f == 1) {
				AudioDriver.S.play (SoundType.jump);
				rb.velocity = new Vector3 (rb.velocity.x, jumpPower, 0);
			}
		}
	}

	public void runReset() {
		beingReset = true;

	    rb.velocity = Vector3.zero;
		//put runner back on map - WHY THE HELL DOESN'T THIS WORK?????????????
		rb.transform.position = new Vector3(0f, 10f, 20f);
	}

	private void checkForResetCompletion() {
		if((rb.transform.position - new Vector3(0f, 10f, 20f)).magnitude < 0.1f) 
			beingReset = false;
	}
}
