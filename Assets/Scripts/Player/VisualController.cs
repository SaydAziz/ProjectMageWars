using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class VisualController : MonoBehaviour
{
    Camera cam;
    public Animator handAnims;
    [SerializeField] Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        cam.fieldOfView = 90;
    }

    // Update is called once per frame
    void Update()
    {
        
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
