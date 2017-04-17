using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour {

    public GameObject exitMenu;
    public GameObject instructionList;
    public Button start;
    public Button options;
    public Button instructions;
    public Button exit;
    public GameObject okay;
    public GameObject instructionsGO;
    public GameObject no;
    public GameObject exitGO;

    string curr_location;

    void Awake()
    {
        Cursor.visible = false;
    }

    // Use this for initialization
    void Start () {
        curr_location = "Start";

        // default disable the exit menu
        exitMenu.SetActive(false);
        instructionList.SetActive(false);

		// Horizontal and Vertical Axes work fine cross-platform (moving vertically using joystick on menu tested on Mac.)
		// Submit Axis must be correctly set though (Defaults to Submit which applies to Windows only.)
		if ((Application.platform == RuntimePlatform.OSXEditor) ||
		    (Application.platform == RuntimePlatform.OSXPlayer)) {
			EventSystem.current.GetComponent<StandaloneInputModule>().submitButton = "Submit_OSX";
		}

		// Note: Use of cancel is to go back to main menu (button B). This applies to the instructions and 
		// options menus. Could also be applicable to the pause menu to maintain consistency.
		// Cancel Axis must be correctly set (Defaults to Cancel which applies to Windows only.)
		if ((Application.platform == RuntimePlatform.OSXEditor) ||
			(Application.platform == RuntimePlatform.OSXPlayer)) {
			EventSystem.current.GetComponent<StandaloneInputModule>().cancelButton = "Cancel_OSX";
		}
    }
    /*
    void Update()
    {
        Debug.Log("Current loc: " + curr_location);
        if (Input.GetAxis("Vertical") < 0)
        {
            Debug.Log("PRESS DOWN");
            switch (curr_location)
            {
                case "Start":
                    curr_location = "Options";
                    break;
                case "Options":
                    curr_location = "Instructions";
                    break;
                case "Instructions":
                    curr_location = "Exit";
                    break;
                case "Exit": //do nothing
                    break;
            }
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            Debug.Log("PRESS UP");
            switch (curr_location)
            {
                case "Start":// do nothing
                    break;
                case "Options":
                    curr_location = "Start";
                    break;
                case "Instructions":
                    curr_location = "Options";
                    break;
                case "Exit":
                    curr_location = "Instructions";
                    break;
            }
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            Debug.Log("PRESS LEFT");
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            Debug.Log("PRESS RIGHT");
        }
        if (Input.GetButton("Submit"))
        {
            Debug.Log("PRESSED A");
        }
    }*/

    public void PressInstructions()
    {
        instructionList.SetActive(true);

        start.enabled = false;
        options.enabled = false;
        instructions.enabled = false;
        exit.enabled = false;

        EventSystem.current.SetSelectedGameObject(okay);
    }

    public void PressOkay()
    {
        instructionList.SetActive(false);

        start.enabled = true;
        options.enabled = true;
        instructions.enabled = true;
        exit.enabled = true;

        EventSystem.current.SetSelectedGameObject(instructionsGO);
    }

    public void PressExit() {
        exitMenu.SetActive(true);

        start.enabled = false;
        options.enabled = false;
        instructions.enabled = false;
        exit.enabled = false;

        EventSystem.current.SetSelectedGameObject(no);
    }

    public void PressNo() {
        exitMenu.SetActive(false);

        start.enabled = true;
        options.enabled = true;
        instructions.enabled = true;
        exit.enabled = true;

        EventSystem.current.SetSelectedGameObject(exitGO);
    }

    public void StartGame() {
        SceneManager.LoadScene("Main");
    }

    public void ExitGame() {
        Application.Quit();
    }
}
