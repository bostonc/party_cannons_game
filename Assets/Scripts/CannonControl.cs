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
	private float maxPitch = 70;

	private GameObject _barrel;
	private GameObject _base;

	private float current_pitch;
	private float current_yaw;

	private float firing_rate = 120; // per minute

	private float lastFired = 0;

  private bool onAI = false;

  public int controller;

    void checkIfAIControlled() {
		if(InputManager.S.getDebugPlayerNum() != controller)
      		onAI = true;
    }

    // Use this for initialization
    void Start () {
		_barrel = transform.Find ("Barrel").gameObject;
		_base = transform.Find ("Base").gameObject;

		current_yaw = 0;
		current_pitch = 45;
	}

	// Update is called once per frame
	void Update () {
		_barrel.transform.localRotation = Quaternion.Euler (current_pitch, current_yaw, 0);
		_base.transform.localRotation = Quaternion.Euler(0, current_yaw, 0);
	}

    public void rotate(float f)
    {
		if ((current_yaw >= maxYaw && f > 0) || (current_yaw <= minYaw && f < 0)) {
			return;
		} else {
			current_yaw += f;
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
  		if (Time.time - lastFired > 60 / firing_rate) {
  			Vector3 barrel_top = _barrel.transform.TransformPoint (new Vector3 (0, 1, 0));

  			GameObject go = MonoBehaviour.Instantiate (Resources.Load ("CannonBall") as GameObject,
  				               barrel_top, Quaternion.identity);

  			go.GetComponent<Rigidbody> ().velocity =
  				(_barrel.transform.TransformPoint (new Vector3 (0, 1, 0)) - _barrel.transform.position) * 15;
			go.GetComponent<CannonBallMetadata> ().setMetadata (controller, this);
  			lastFired = Time.time;
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
