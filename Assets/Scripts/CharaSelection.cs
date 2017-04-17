using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharaSelection : MonoBehaviour {
    public InputManager.PlayerColor p1clr;
    public InputManager.PlayerColor p2clr;
    public InputManager.PlayerColor p3clr;
    public InputManager.PlayerColor p4clr;

    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;

	// Use this for initialization
	void Start () {
        string[] Joysticks = Input.GetJoystickNames();
        int i = 0;
        for (; i < Joysticks.Length; ++i)
        {

        }
        for (; i < 4; ++i)
        {
            if (i == 0)
            {
                SceneManager.LoadScene(1);
            }
            else if (i == 1)
            {
                p2.SetActive(false);
            }
            else if (i == 2)
            {
                p3.SetActive(false);
            }
            else if (i == 3)
            {
                p4.SetActive(false);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        print(Input.GetAxis("Rotate_1"));
		if (Input.GetAxis("Rotate_1") == 1)
        {
            Vector3 position = p1.GetComponent<Transform>().position;
            print("ROTATING");
            if (position.x < -187.6f)
            {
                p1.GetComponent<Transform>().position = new Vector3(-187.6f, -148.7f, 0);
            }
            else if (position.x > 187.6f)
            {
                p1.GetComponent<Transform>().position = new Vector3(187.6f, -148.7f, 0);
            }
            p1.GetComponent<Transform>().position += new Vector3(100, 0, 0);
        }
        if (Input.GetAxis("Rotate_1") == -1)
        {
            Vector3 position = p1.GetComponent<Transform>().position;
            print("ROTATING");
            if (position.x < -187.6f)
            {
                p1.GetComponent<Transform>().position = new Vector3(-187.6f, -148.7f, 0);
            }
            else if (position.x > 187.6f)
            {
                p1.GetComponent<Transform>().position = new Vector3(187.6f, -148.7f, 0);
            }
            p1.GetComponent<Transform>().position -= new Vector3(100, 0, 0);
        }
        if (Input.GetAxis("Rotate_2") != 0)
        {
            print("ROTATING");
        }
        if (Input.GetAxis("Rotate_3") != 0)
        {
            print("ROTATING");
        }
        if (Input.GetAxis("Rotate_4") != 0)
        {
            print("ROTATING");
        }
    }
}
