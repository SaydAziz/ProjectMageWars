using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject levelCanvas;
    public GameObject currentCanvas;

    bool menuOn = false;

    private void Awake()
    {
        currentCanvas = mainCanvas;
    }

    public void ToggleGameMenu()
    {
        menuOn = menuOn ? false : true;
        this.gameObject.SetActive(menuOn);     
        Cursor.visible = menuOn;
        if (menuOn)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;

        }
    }
    public void ToggleGameMenu(bool turnOn)
    {
        menuOn = turnOn;
        this.gameObject.SetActive(menuOn);
        Cursor.visible = menuOn;
        if (menuOn)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;

        }
    }
    public void SwitchToLevelCanvas()
    {
        currentCanvas.SetActive(false);
        currentCanvas = levelCanvas;
        levelCanvas.SetActive(true);
    }
    public void SwitchToMainCanvas()
    {
        currentCanvas.SetActive(false);
        currentCanvas = mainCanvas;
        mainCanvas.SetActive(true);
    }
    public void StartTestLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void StartMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
