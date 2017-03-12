using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Menu : MonoBehaviour {

    public Canvas exitMenu;
    public Button start;
    public Button options;
    public Button exit;

	// Use this for initialization
	void Start () {
        // default disable the exit menu
        exitMenu.enabled = false;
	}
	
	public void PressExit() {
        exitMenu.enabled = true;

        start.enabled = false;
        options.enabled = false;
        exit.enabled = false;
    }

    public void PressNo() {
        exitMenu.enabled = false;

        start.enabled = true;
        options.enabled = true;
        exit.enabled = true;
    }

    public void StartGame() {
        Application.LoadLevel("Main");
    }

    public void ExitGame() {
        Application.Quit();
    }
}
