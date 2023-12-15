using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject levelCanvas;
    public GameObject settingsCanvas;
    GameObject currentCanvas;

    GameSettings gameSettings;

    bool menuOn = false;
    float timeCache;

    private void Awake()
    {
        currentCanvas = mainCanvas;
        gameSettings = GameObject.FindObjectOfType<GameSettings>();
    }

    public void ToggleGameMenu()
    {
        menuOn = menuOn ? false : true;
        this.gameObject.SetActive(menuOn);     
        Cursor.visible = menuOn;
        if (menuOn)
        {
            timeCache = Time.timeScale;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = timeCache;

        }
    }
    public void ToggleGameMenu(bool turnOn)
    {
        menuOn = turnOn;
        this.gameObject.SetActive(menuOn);
        Cursor.visible = menuOn;
        if (menuOn)
        {
            timeCache = Time.timeScale;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = timeCache;

        }
    }
    public void SwitchToLevelCanvas()
    {
        currentCanvas.SetActive(false);
        currentCanvas = levelCanvas;
        levelCanvas.SetActive(true);

    }
    public void SwitchToSettingsCanvas()
    {
        currentCanvas.SetActive(false);
        currentCanvas = settingsCanvas;
        settingsCanvas.SetActive(true);
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
    public void StartCampaignLevel()
    {
        SceneManager.LoadScene(2);

    }
    public void StartMainMenu()
    {
        Destroy(gameSettings.gameObject);
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
