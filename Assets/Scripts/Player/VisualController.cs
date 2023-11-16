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
    [SerializeField] PostProcessVolume ppVol0, ppVol1, ppVol2;
    Vignette leftVignette, rightVignette;
    ChromaticAberration chrome;
    Grain gran;
    Bloom bloom;
    LensDistortion lensDistortion;


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

    public void TogglePpWeight(float time)
    {
        DOTween.To(() => ppVol0.weight, x => ppVol0.weight = x, 1f, time);
        Invoke(nameof(ResetWeight), time);
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
    private void ResetWeight()
    {
        DOTween.To(() => ppVol0.weight, x => ppVol0.weight = x, 0.01f, 0.1f);
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
