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
    public float maxYaw = 45;
    public float minYaw = -45;

	public float minPitch = 0;
	public float maxPitch = 60;

	private GameObject _barrel;
	private GameObject _base;

	private float current_pitch;
	private float current_yaw;

    // Use this for initialization
    void Start () {
		_barrel = transform.Find ("Barrel").gameObject;
		_base = transform.Find ("Base").gameObject;

		current_yaw = _base.transform.eulerAngles.y;
		current_pitch = _barrel.transform.eulerAngles.x;
	}
	
	// Update is called once per frame
	void Update () {
		_barrel.transform.rotation = Quaternion.Euler (current_pitch, current_yaw, 0);
		_base.transform.rotation = Quaternion.Euler(0, current_yaw, 0);
	}

    public void rotate(float f)
    {
		Debug.Log (current_yaw);
		if ((current_yaw >= maxYaw && f > 0) || (current_yaw <= minYaw && f < 0)) {
			return;
		} else {
			current_yaw += f;
		}
    }

    public void pitch(float f)
    {
		Debug.Log (current_pitch);
		if ((current_pitch >= maxPitch && f > 0) || (current_pitch <= minPitch && f < 0)) {
			return;
		} else {
			current_pitch += f;
		}
    }

    public void fire(float f)
    {
        print("firing " + gameObject.name);
    }

    public void stopFire()
    {
        print("cease firing " + gameObject.name);
    }
}

