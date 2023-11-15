using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;

public class VisualController : MonoBehaviour
{
    Camera cam;
    public Animator handAnims;
    [SerializeField] Slider healthBar;
    [SerializeField] PostProcessVolume ppVol1, ppVol2;
    Vignette leftVignette, rightVignette;


    bool vignetteOn;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        cam.fieldOfView = 90;
        vignetteOn = false;
        ppVol1.profile.TryGetSettings(out leftVignette);
        ppVol2.profile.TryGetSettings(out rightVignette);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddVignette(float val, int side)
    {
        if (side == 0)
        {
            ppVol1.priority = 1;
            leftVignette.opacity.value = val;
        }
        else if (side == 1)
        {
            ppVol2.priority = 1;
            rightVignette.opacity.value = val;
        }
    }

    public void RemoveVignette()
    {
        leftVignette.opacity.value = -0.03f;
        rightVignette.opacity.value = -0.03f;
        ppVol1.priority = 0;
        ppVol2.priority = 0;


    }

    public void TempFovChange(int fov, float time)
    {
        cam.DOFieldOfView(fov, time);
        Invoke(nameof(ResetFOV), time);
    }

    private void ResetFOV()
    {
        cam.DOFieldOfView(90, 0.2f);
    }

    public void UpdateHealth(float value)
    {
        healthBar.value = value;
    }
}
