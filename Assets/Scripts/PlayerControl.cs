using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	static public PlayerControl S;


	public static bool freeze;

	public float speed = 10;
	public float jumpPower = 10;

	//player variable jump
	private float timeHeld = 0.0f;
	private int zerosFromInput = 0;
	private bool falling = false;
	public float maxJump = 3.0f;

	private Rigidbody rb;

	//For AI control
	private bool onAI = false;
	private int currentX = 0;
	private int desiredX = 0;
	private float moveCount = 0;

	private bool beingReset = false;
	private Vector3 resetPos;

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
		if (InputManager.S.paused || freeze)
			return;
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
						desiredX = Mathf.Clamp (currentX + 5, -19, 19);
					} else {
						desiredX = Mathf.Clamp (currentX - 5, -19, 19);
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
			if(rb.position.x + Time.deltaTime * speed * f < 19.0f && rb.position.x + Time.deltaTime * speed * f > -19.0f)
				transform.Translate (Time.deltaTime * speed * f, 0, 0);
		}
	}

	public void jump(float f)
	{

        
        if (!beingReset) {
            

            // Variable Jump
            if (f == 0) {
				zerosFromInput++;
			} else {
				zerosFromInput = 0;
			}

			if (rb.velocity.y == 0) {
				falling = false;
			}

			if ((zerosFromInput > 3 || timeHeld > maxJump) && rb.velocity.y > 0) {
				
				//start falling
				falling = true;
				rb.velocity = new Vector3 (rb.velocity.x, -rb.velocity.y, 0);

			} else {

				//keep going higher
				if (f == 1 && falling == false) {

					falling = true;
					rb.velocity = new Vector3 (rb.velocity.x, jumpPower, 0);
					if (gameObject.transform.parent != null)
						gameObject.transform.parent = null;
				}
			}


			/* - Old jump system
			if (rb.velocity.y == 0 && f == 1) {
				AudioDriver.S.play (SoundType.jump);
				rb.velocity = new Vector3 (rb.velocity.x, jumpPower, 0);
				if (gameObject.transform.parent != null)
					gameObject.transform.parent = null;
			}
			*/
		}
	}

	public void runReset() {
		beingReset = true;

	    rb.velocity = Vector3.zero;
		//put runner back on map, with randomization on the X-axis.
		resetPos = new Vector3(Random.Range(-15.0f, 15.0f), 10f, 20f);
		rb.transform.position = resetPos;
	}

	private void checkForResetCompletion() {
		if((rb.transform.position - resetPos).magnitude < 0.1f) 
			beingReset = false;
	}
}
