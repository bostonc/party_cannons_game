using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
    Left stick (horizontal) to adjust rotation
    Right stick (vertical) to adjust pitch
    right trigger (hold for power adjust) to fire 


    joy1-3 are launchers
    joy 4 is runner
*/

public class CannonControl : MonoBehaviour
{
    public int maxRotation = 45;
    public int minRotation = 45;
    public float rotationMultiplier = 1f;

    public bool _______________;




    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void rotate(float f)
    {
        print("rotating " + gameObject.name + " by " + f);
        if (f > 0)
        {
            gameObject.transform.Rotate(Vector3.up * Time.deltaTime, f * rotationMultiplier);
        }
        if (f < 0)
        {
            gameObject.transform.Rotate(Vector3.up * Time.deltaTime, f * rotationMultiplier);
        }
    }

    public void pitch(float f)
    {
        print("changing pitch of " + gameObject.name);
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

