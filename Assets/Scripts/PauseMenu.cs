using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour

{
    private void Start()
    {

    }

    public void PressResume()
    {
        unpause();
        gameObject.SetActive(false);
    }

    public void PressRestart()
    {
        unpause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PressQuit()
    {
        unpause();
        SceneManager.LoadScene("StartUpMenu");
    }

    private void unpause()
    {
        Time.timeScale = 1;
        InputManager.S.paused = false;
        EventSystem.current.SetSelectedGameObject(null);
    }
}
