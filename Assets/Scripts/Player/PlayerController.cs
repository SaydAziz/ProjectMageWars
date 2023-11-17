using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] VisualController vController;
    [SerializeField] ViewModel viewModel;
    [SerializeField] PlayerData playerData;
    [SerializeField] Transform leftHand, rightHand;
    Camera cam;
    LayerMask groundLayer;
    Collider[] hitWallsR, hitWallsL, currentHitWall;
    Collider cachedWall;

    //Look Values
    float mouseSensitivity = 0.04f;
    float xRot, yRot;

    //Move Values
    float moveSpeed = 8.5f;
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

    bool isQueuedRight;
    bool isPreppedRight;
    bool rightBuffered;

    bool canJump = true;
    bool canDash = true;
    bool canShootRight = true;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        
        cam = Camera.main;
        groundLayer = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Look Controls (maybe in fixed? idk we will see)
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

        hitWallsR = Physics.OverlapCapsule(transform.position + transform.right * 0.4f - transform.forward * 0.3f, transform.position + transform.right * 0.3f - transform.forward * 0.3f, .3f, groundLayer);
        hitWallsL = Physics.OverlapCapsule(transform.position - transform.right * 0.4f - transform.forward * 0.3f, transform.position - transform.right * 0.3f - transform.forward * 0.3f, .3f, groundLayer);
        //nearWall = Physics.CheckCapsule(transform.position + transform.right * 0.4f - transform.forward * 0.3f, transform.position - transform.right * 0.4f - transform.forward * 0.3f, .3f, groundLayer);

        if (CheckHitWallColliders() && canJump && !CheckWall(currentHitWall[0]) && !isGrounded) //this is a redundant check with walljump input check
        {
            if (hitWallsL.Length > 0)
            {
                vController.AddVignette(1f, 0);
            }
            else if (hitWallsR.Length > 0)
            {
                vController.AddVignette(1f, 1);
            }
        }
        else
        {
            vController.RemoveVignette();
        }


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
        transform.rotation = Quaternion.Euler(0, yRot, 0);

        if (rb.velocity.magnitude > 1) viewModel.doBOB();
            

        Debug.Log(isGrounded);

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
        viewModel.setInputVal(value);
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
        else if (CheckHitWallColliders() && canJump && !CheckWall(currentHitWall[0]))
        {
            cachedWall = currentHitWall[0];
            canJump = false;
            rb.velocity = Vector3.zero;
            rb.AddForce((transform.up * jumpHeight * 0.95f) + transform.forward * jumpHeight, ForceMode.Impulse);
            vController.TempFovChange(92, 0.5f);

            Invoke(nameof(ResetJump), jumpCD);
        }
    }
    public void GetDashInput()
    {
        if (canDash && rb.velocity.magnitude > 0.5f)
        {
            canDash = false;
            rb.drag = 0;
            rb.velocity = Vector3.zero;
            rb.AddForce(((transform.forward * yDir + transform.right * xDir) + transform.up * .4f) * dashSpeed, ForceMode.Impulse);
            vController.TogglePpWeight(.3f);
            vController.TempFovChange(105, 0.2f);
            Invoke(nameof(ResetDash), dashCD);
        }
        
    }

    public void QueueRight()
    {
        if (canShootRight)
        {
            rightBuffered = false;
            canShootRight = false;
            isQueuedRight = true;
            vController.handAnims.SetBool("Queued", true);
            Invoke(nameof(DORightQueue), playerData.rightHand.queueTime);

        }
        else
        {
            rightBuffered = true;
        }
    }

    private void DORightQueue()
    {
        playerData.rightHand.Queue(rightHand.transform);
        isPreppedRight = true;
    }

    public void UseRight()
    {
        rightBuffered = false;
        if (isQueuedRight && isPreppedRight)
       {
            playerData.rightHand.Use(cam.transform.forward);
            vController.handAnims.SetBool("Queued", false);
            Invoke(nameof(ResetShootRight), playerData.rightHand.useCooldown);
       }
       else
       {
            ResetShootRight();
            CancelInvoke(nameof(DORightQueue));
            vController.handAnims.SetBool("Queued", false);
            vController.handAnims.SetTrigger(1);    
       }
        
    }
    private void ResetShootRight()
    {
        canShootRight = true;
        isQueuedRight = false;
        isPreppedRight = false;
        if (rightBuffered)
        {
            QueueRight();
        }
        rightBuffered = false; //need to figure out if all of these buffer resets are needed lol
    }

    bool CheckHitWallColliders()
    {
        if (hitWallsL.Length > 0)
        {
            currentHitWall = hitWallsL;
        }
        else if (hitWallsR.Length > 0)
        {
            currentHitWall = hitWallsR;
        }
        else
        {
            return false;
        }    
        return true;

    }

}
