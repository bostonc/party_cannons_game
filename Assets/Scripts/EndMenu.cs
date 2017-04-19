using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PressPlayAgain()
    {
        RoundTracker.R.resetEverything();
        SceneManager.LoadScene("Main");
    }
    
    public void PressReturnMenu()
    {
        SceneManager.LoadScene("StartUpMenu");
    }
}
