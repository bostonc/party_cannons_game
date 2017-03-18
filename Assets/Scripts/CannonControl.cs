using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Left stick (horizontal) to adjust rotation
    Right stick (vertical) to adjust pitch
    right trigger (hold for power adjust) to fire


    joy1-3 are launchers
    joy 4 is runner // NOT USED IN THIS FILE
*/

public class CannonControl : MonoBehaviour
{
	private float maxYaw = 45;
	private float minYaw = -45;

	private float minPitch = 30;
	private float maxPitch = 90;

	private GameObject _barrel;
	private GameObject _base;

	private float current_pitch;
	private float current_yaw;

	private float firing_rate = 120; // per minute

	private float lastFired = 0;

    private bool onAI = false;

	public InputManager.ControlID cID;

	private float desired_yaw;
	private float desired_pitch;
	private int desired_shots_fired;
	private int shots_fired;

	private float paused_ai_start_time = 0.0f;

	private GameObject predictiveLine;

    void checkIfAIControlled() {
		if (InputManager.S.getPlayerInfoWithControlID(cID).aiMode == InputManager.AIMode.On)
			onAI = true;
		else
			onAI = false;
    }

    // Use this for initialization
    void Start () {
		_barrel = transform.Find ("Barrel").gameObject;
		_base = transform.Find ("Base").gameObject;

		current_yaw = 0;
		current_pitch = 45;

		desired_yaw = current_yaw;
		desired_pitch = current_pitch;
		shots_fired = 0;
		desired_shots_fired = 0;

		predictiveLine = MonoBehaviour.Instantiate (Resources.Load ("PredictiveLine") as GameObject,
			_barrel.transform.TransformPoint (new Vector3 (0, 1, 0)), Quaternion.identity);
	}

	// Update is called once per frame
	void Update () {
		checkIfAIControlled ();
		if (onAI) {
			if (Time.time - paused_ai_start_time < 0.5f) {
				return;
			}
			paused_ai_start_time = 0.0f;
			if (Mathf.Approximately(current_pitch, desired_pitch) && Mathf.Approximately(current_yaw, desired_yaw) && shots_fired == desired_shots_fired) {
				int action = Random.Range (0, 4);
				shots_fired = 0;
//				PlayerControl.S.getRunnerPos () - _barrel.transform.position;
				switch (action) {
				case 0:
					desired_pitch = Random.Range ((int) minPitch, (int) maxPitch);
					Debug.Log (cID + " Pitch, " + desired_pitch);
					break;
				case 1:
					desired_yaw = Random.Range ((int) minYaw, (int) maxYaw);
					Debug.Log (cID + " Yaw, " + desired_yaw); 
					break;
				case 2:
					desired_shots_fired = Random.Range (1, 6);
					Debug.Log (cID + " Shots, " + desired_shots_fired); 
					break;
				}
			} else {
				if (Random.value < 0.2) {
					paused_ai_start_time = Time.time;
					return;
				}
				if (shots_fired < desired_shots_fired) {
					fire (1.0f);
					shots_fired += 1;
					//Debug.Log (shots_fired - desired_shots_fired);
				} else if (desired_pitch > current_pitch) {
					pitch (1.0f);
					//Debug.Log (current_pitch - desired_pitch);
				} else if (desired_pitch < current_pitch) {
					pitch (-1.0f);
					//Debug.Log (current_pitch - desired_pitch);
				} else if (desired_yaw > current_yaw) {
					rotate (1.0f);
					//Debug.Log (current_yaw - desired_yaw);
				} else if (desired_yaw < current_yaw) {
					rotate (-1.0f);
					//Debug.Log (current_yaw - desired_yaw);
				}
			}
		}
		_barrel.transform.localRotation = Quaternion.Euler (current_pitch, current_yaw, 0);
		_base.transform.localRotation = Quaternion.Euler(0, current_yaw, 0);

		UpdateTrajectory (predictiveLine, _barrel.transform.TransformPoint (new Vector3 (0, 1, 0)), 
			(_barrel.transform.TransformPoint (new Vector3 (0, 1, 0)) - _barrel.transform.position) * 15, 
			new Vector3 (0f, -9.8f, 0f)); 
	}

	void UpdateTrajectory(GameObject go, Vector3 initialPosition, Vector3 initialVelocity, Vector3 gravity)
	{
		int numSteps = 20; // for example
		float timeDelta = 1.0f / initialVelocity.magnitude; // for example

		LineRenderer lineRenderer = go.GetComponent<LineRenderer>();
		lineRenderer.numPositions = numSteps;

		Vector3 position = initialPosition;
		Vector3 velocity = initialVelocity;
		for (int i = 0; i < numSteps; ++i)
		{
			lineRenderer.SetPosition(i, position);

			position += velocity * timeDelta + 0.5f * gravity * timeDelta * timeDelta;
			velocity += gravity * timeDelta;
		}
	}

    public void rotate(float f)
    {
		if ((current_yaw >= maxYaw && f > 0) || (current_yaw <= minYaw && f < 0)) {
			return;
		} else {
			current_yaw += f;
            //DO NOT REMOVE THIS LINE
            AudioDriver.S.play(SoundType.rotate);
        }
    }

    public void pitch(float f)
    {
		if ((current_pitch >= maxPitch && f > 0) || (current_pitch <= minPitch && f < 0)) {
			return;
		} else {
			current_pitch += f;
		}
    }

    public void fire(float f)
    {
		// TODO: Currently thresholding input to start/stop firing. Can modify to synchronize firing rate in 
		// proportion to given float in range which is in range [0.0f, 1.0f].
		if (Time.time - lastFired > 60 / firing_rate && f > 0.5f) {
  			Vector3 barrel_top = _barrel.transform.TransformPoint (new Vector3 (0, 1, 0));

  			GameObject go = MonoBehaviour.Instantiate (Resources.Load ("CannonBall") as GameObject,
  				               barrel_top, Quaternion.identity);

			go.GetComponent<Renderer> ().material = _barrel.GetComponent<Renderer> ().material;

  			go.GetComponent<Rigidbody> ().velocity =
  				(_barrel.transform.TransformPoint (new Vector3 (0, 1, 0)) - _barrel.transform.position) * 15;
			go.GetComponent<CannonBallMetadata> ().setMetadata (cID, this);
  			lastFired = Time.time;
            //DO NOT REMOVE THIS LINE
            AudioDriver.S.play(SoundType.launch);
        }
    }

    public void stopFire()
    {
        print("cease firing " + gameObject.name);
    }

	public void setMaterial(Material mat) {
		_barrel.GetComponent<MeshRenderer> ().material = mat;
		_base.GetComponent<MeshRenderer> ().material = mat;

		MeshRenderer[] childrenGOrenderers = GetComponentsInChildren<MeshRenderer> ();
		foreach(MeshRenderer rend in childrenGOrenderers) {
			rend.material = mat;
		}
	}

	public Material getMaterial() {
		return _base.GetComponent<MeshRenderer> ().material;
	}
}
