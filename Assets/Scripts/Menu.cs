using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject levelCanvas;
    public GameObject currentCanvas;

    private void Awake()
    {
        currentCanvas = mainCanvas;
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
}
