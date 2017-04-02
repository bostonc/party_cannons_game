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

	// Use this for initialization
	void Start () {
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

    public void PressInstructions()
    {
        instructionList.SetActive(true);

        start.enabled = false;
        options.enabled = false;
        instructions.enabled = false;
        exit.enabled = false;
    }

    public void PressOkay()
    {
        instructionList.SetActive(false);

        start.enabled = true;
        options.enabled = true;
        instructions.enabled = true;
        exit.enabled = true;
    }

    public void PressExit() {
        exitMenu.SetActive(true);

        start.enabled = false;
        options.enabled = false;
        instructions.enabled = false;
        exit.enabled = false;
    }

    public void PressNo() {
        exitMenu.SetActive(false);

        start.enabled = true;
        options.enabled = true;
        instructions.enabled = true;
        exit.enabled = true;
    }

    public void StartGame() {
        SceneManager.LoadScene("Main");
    }

    public void ExitGame() {
        Application.Quit();
    }
}
