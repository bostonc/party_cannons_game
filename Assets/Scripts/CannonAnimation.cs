using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonAnimation : MonoBehaviour {
    static public CannonAnimation C;

    public Sprite[] red;
    public Sprite[] blue;
    public Sprite[] green;
    public Sprite[] yellow;

    public GameObject spritetochange;

    public InputManager.ControlID curr_CID;

    bool moving;

    public GameObject cannon_base;
    public GameObject cannon_barrel;

    Quaternion base_rotation;
    Quaternion barrel_rotation;

    float curr_time;

    void Awake()
    {
        C = this;
    }

    // Use this for initialization
    void Start () {
        curr_time = Time.time;
        moving = false;
        base_rotation = cannon_base.GetComponent<Transform>().rotation;
        barrel_rotation = cannon_barrel.GetComponent<Transform>().rotation;
    }

    // Update is called once per frame
    void Update() {

        Quaternion curr_base_rot = cannon_base.GetComponent<Transform>().rotation;
        Quaternion curr_barrel_rot = cannon_barrel.GetComponent<Transform>().rotation;

        if (curr_base_rot != base_rotation || curr_barrel_rot != barrel_rotation)
        {
            moving = true;

        }
        else if (curr_base_rot == base_rotation && curr_barrel_rot == barrel_rotation)
        {
            moving = false;
        }
        base_rotation = curr_base_rot;
        barrel_rotation = curr_barrel_rot;

        InputManager.PlayerColor pclr = InputManager.S.getPlayerColorWithControlID(curr_CID);
        Sprite changeme = spritetochange.GetComponent<SpriteRenderer>().sprite;
        
        if (moving)
        //if (Input.anyKey || Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0)
        {   
            if (Time.time - curr_time > 0.25f) {
                curr_time = Time.time;
                //Debug.Log("SOMETHING IS BEING PRESSED");
                //Debug.Log(pclr);
                //Debug.Log(changeme == red[0]);
                //yield return new WaitForSeconds(1);
                if (pclr == InputManager.PlayerColor.Red)
                {
                    if (changeme == red[0])
                    {
                        spritetochange.GetComponent<SpriteRenderer>().sprite = red[1];
                    }
                    else if (changeme == red[1])
                    {
                        spritetochange.GetComponent<SpriteRenderer>().sprite = red[2];
                    }
                    else if (changeme == red[2])
                    {
                        spritetochange.GetComponent<SpriteRenderer>().sprite = red[1];
                    }
                }
                else if (pclr == InputManager.PlayerColor.Blue)
                {
                    if (changeme == blue[0])
                    {
                        spritetochange.GetComponent<SpriteRenderer>().sprite = blue[1];
                    }
                    else if (changeme == blue[1])
                    {
                        spritetochange.GetComponent<SpriteRenderer>().sprite = blue[2];
                    }
                    else
                    {
                        spritetochange.GetComponent<SpriteRenderer>().sprite = blue[1];
                    }
                }
                else if (pclr == InputManager.PlayerColor.Green)
                {
                    if (changeme == green[0])
                    {
                        spritetochange.GetComponent<SpriteRenderer>().sprite = green[1];
                    }
                    else if (changeme == green[1])
                    {
                        spritetochange.GetComponent<SpriteRenderer>().sprite = green[2];
                    }
                    else
                    {
                        spritetochange.GetComponent<SpriteRenderer>().sprite = green[1];
                    }
                }
                else if (pclr == InputManager.PlayerColor.Yellow)
                {
                    if (changeme == yellow[0])
                    {
                        spritetochange.GetComponent<SpriteRenderer>().sprite = yellow[1];
                    }
                    else if (changeme == yellow[1])
                    {
                        spritetochange.GetComponent<SpriteRenderer>().sprite = yellow[2];
                    }
                    else
                    {
                        spritetochange.GetComponent<SpriteRenderer>().sprite = yellow[1];
                    }
                }
            }
        }
        else
        {
            if (pclr == InputManager.PlayerColor.Red)
            {
                spritetochange.GetComponent<SpriteRenderer>().sprite = red[0];
            }
            else if (pclr == InputManager.PlayerColor.Blue)
            {
                spritetochange.GetComponent<SpriteRenderer>().sprite = blue[0];
            }
            else if (pclr == InputManager.PlayerColor.Green)
            {
                spritetochange.GetComponent<SpriteRenderer>().sprite = green[0];
            }
            else if (pclr == InputManager.PlayerColor.Yellow)
            {
                spritetochange.GetComponent<SpriteRenderer>().sprite = yellow[0];
            }
        }
    }
}
