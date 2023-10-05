using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LookInput(InputAction.CallbackContext context)
    {
        controller.GetLookInput(context.ReadValue<Vector2>());
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        controller.GetMoveInput(context.ReadValue<Vector2>());
    }
    public void JumpInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            controller.GetJumpInput();
        }
    }
    public void DashInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            controller.GetDashInput();
        }
    }
}
