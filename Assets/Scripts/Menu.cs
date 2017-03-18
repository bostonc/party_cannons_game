using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

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
