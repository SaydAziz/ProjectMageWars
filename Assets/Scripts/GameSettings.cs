using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    
    SettingsData settingsData;
    [SerializeField] TMP_Text fpsCounter;
    [SerializeField] Toggle fpsToggle;

    private float pollingTime = 0.1f;
    private float time;
    private int frameCount;



    private static GameSettings _instance;
    public static GameSettings Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameSettings>();
            }

            return _instance;
        }
    }



    string filePath;
    private void Awake()
    {
        Application.targetFrameRate = -1;
        filePath = Application.persistentDataPath + "/gameSettings.json";
        settingsData = new SettingsData();
        if (File.Exists(filePath))
        {
            settingsData = FunctionalUtility.LoadData<SettingsData>(filePath);
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        fpsToggle.isOn = settingsData.showFPS;
             
    }

    private void Update()
    {
        if(settingsData.showFPS)
        {
            time += Time.deltaTime;
            frameCount++;

            if (time >= pollingTime)
            {
                int frameRate = Mathf.RoundToInt(frameCount / pollingTime);
                fpsCounter.text = frameRate.ToString() + " FPS";

                time -= pollingTime;
                frameCount = 0;
            }
        }
        
    }

    public void Apply()
    {
        FunctionalUtility.SaveData(filePath, settingsData);
    }

    public void ToggleFPS()
    {
        if (fpsToggle.isOn)
        {
            settingsData.showFPS = true;
            fpsCounter.enabled = true;
        }
        else
        {
            settingsData.showFPS = false;
            fpsCounter.enabled = false;
        }
    }
}
