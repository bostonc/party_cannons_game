using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trailer_CatMovement : MonoBehaviour
{
    float speed = .1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 oldOPos = gameObject.transform.position;
        Vector3 pos = new Vector3(oldOPos.x + speed, oldOPos.y, oldOPos.z);
        gameObject.transform.position = pos;
    }
}
