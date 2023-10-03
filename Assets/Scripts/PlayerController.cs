using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    Camera cam;

    //Look Controls
    float mouseSensitivity = 0.1f;
    float xRot, yRot;



    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, yRot, 0);
        cam.transform.rotation = Quaternion.Euler(xRot, yRot, 0);
    }

    public void GetLookInput(Vector2 value)
    {
        yRot += value.x * mouseSensitivity; 

        xRot -= value.y * mouseSensitivity;
        xRot = Mathf.Clamp(xRot, -90f, 90f);
    }
}
