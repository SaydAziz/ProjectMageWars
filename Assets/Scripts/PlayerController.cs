using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    Camera cam;
    LayerMask groundLayer;

    //Look Values
    float mouseSensitivity = 0.1f;
    float xRot, yRot;

    //Move Values
    float moveSpeed = 10f;
    float xDir, yDir;
    Vector3 moveDir;

    float jumpHeight = 12f;
    float jumpCD = 0.25f;
    float airMultiplier;

    //State Info
    bool isGrounded;
    bool canJump = true;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        groundLayer = LayerMask.GetMask("Ground");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, Vector3.down * 1.2f, Color.red, 1);
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.2f, groundLayer);

        if (isGrounded)
        {
            rb.drag = 6f;
            airMultiplier = 1f;
        }
        else
        {
            rb.drag = 0f;
            airMultiplier = 0.1f;
        }

        //Look Controls
        transform.rotation = Quaternion.Euler(0, yRot, 0);
        cam.transform.rotation = Quaternion.Euler(xRot, yRot, 0);

        //Move Controls
        moveDir = transform.forward * yDir + transform.right * xDir;
        rb.AddForce(moveDir.normalized *  moveSpeed * airMultiplier * 10f, ForceMode.Force);

        

    }

    private void ResetJump()
    {
        canJump = true;
    }


    public void GetLookInput(Vector2 value)
    {
        yRot += value.x * mouseSensitivity; 

        xRot -= value.y * mouseSensitivity;
        xRot = Mathf.Clamp(xRot, -90f, 90f);
    }
    public void GetMoveInput(Vector2 value)
    {
        Debug.Log(value);
        xDir = value.x;
        yDir = value.y;
    }
    public void GetJumpInput()
    {
        if (isGrounded && canJump)
        {
            canJump = false;
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);

            Invoke(nameof(ResetJump), jumpCD);
        }
    }
}
