using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] VisualController vController;
    [SerializeField] PlayerData playerData;
    [SerializeField] Transform leftHand, rightHand;
    Camera cam;
    LayerMask groundLayer;
    Collider[] hitWalls;
    Collider cachedWall;

    //Look Values
    float mouseSensitivity = 0.04f;
    float xRot, yRot;

    //Move Values
    float moveSpeed = 10f;
    float xDir, yDir;
    Vector3 moveDir;

    float jumpHeight = 12f;
    float jumpCD = 0.25f;
    float airMultiplier;

    //Ability Values
    float dashSpeed = 20f;
    float dashCD = 2f;


    //State Info
    bool isGrounded;
    bool canJump = true;
    bool canDash = true;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        
        cam = Camera.main;
        groundLayer = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        //Look Controls (maybe in fixed? idk we will see)
        transform.rotation = Quaternion.Euler(0, yRot, 0);
        cam.transform.rotation = Quaternion.Euler(xRot, yRot, 0);
    }

    private void OnDrawGizmos()
    {
        //Draw a cube at the maximum distance
        Gizmos.DrawWireCube(transform.position - transform.up * 0.7f, transform.localScale);
    }

    private void FixedUpdate()
    {
        isDead = playerData.isDead;
        Debug.DrawRay(transform.position, Vector3.down * 1.2f, Color.red, .1f);
        isGrounded = Physics.CheckBox(transform.position - transform.up * .7f, new Vector3(.3f, .4f, .3f), Quaternion.identity, groundLayer);
        //isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.2f, groundLayer);

        hitWalls = Physics.OverlapCapsule(transform.position + transform.right * 0.4f - transform.forward * 0.3f, transform.position - transform.right * 0.4f - transform.forward * 0.3f, .3f, groundLayer);
        //nearWall = Physics.CheckCapsule(transform.position + transform.right * 0.4f - transform.forward * 0.3f, transform.position - transform.right * 0.4f - transform.forward * 0.3f, .3f, groundLayer);

        if (isGrounded)
        {
            rb.drag = 6f;
            airMultiplier = 1f;
            cachedWall = null;
        }
        else
        {
            rb.drag = 0f;
            airMultiplier = 0.1f;
        }

        
        //Move Controls
        moveDir = transform.forward * yDir + transform.right * xDir;
        rb.AddForce(moveDir.normalized *  moveSpeed * airMultiplier * 10f, ForceMode.Force);

        //Debug.Log(isGrounded);

    }

    private void ResetJump()
    {
        canJump = true;
    }
    private void ResetDash()
    {
        canDash = true;
    }
    private bool CheckWall(Collider currentWall)
    {
        if (currentWall != cachedWall)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void GetLookInput(Vector2 value)
    {
        yRot += value.x * mouseSensitivity; 

        xRot -= value.y * mouseSensitivity;
        xRot = Mathf.Clamp(xRot, -90f, 90f);
    }
    public void GetMoveInput(Vector2 value)
    {
        //Debug.Log(value);
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
        else if ((hitWalls.Length > 0) && canJump && !CheckWall(hitWalls[0]))
        {
            cachedWall = hitWalls[0];
            canJump = false;
            rb.velocity = Vector3.zero;
            rb.AddForce((transform.up * jumpHeight) + transform.forward * jumpHeight, ForceMode.Impulse);
            //vController.TempFovChange(92, 0.5f);

            Invoke(nameof(ResetJump), jumpCD);
        }
    }
    public void GetDashInput()
    {
        if (canDash)
        {
            canDash = false;
            rb.velocity = Vector3.zero;
            rb.AddForce(((transform.forward * yDir + transform.right * xDir) + transform.up * .2f) * dashSpeed, ForceMode.Impulse);
            //vController.TempFovChange(92, 0.5f);
            Invoke(nameof(ResetDash), dashCD);
        }
        
    }

    public void QueueRight()
    {
        playerData.rightHand.Queue(rightHand.transform);
        vController.handAnims.SetBool("Queued", true);
    }

    public void UseRight()
    {
        playerData.rightHand.Use();
        vController.handAnims.SetBool("Queued", false);

    }

    

}
