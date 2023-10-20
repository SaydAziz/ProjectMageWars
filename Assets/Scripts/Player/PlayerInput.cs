using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    [SerializeField] UnityEngine.InputSystem.PlayerInput pi;
    [SerializeField] Menu menu;

    bool contEnabled = true;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isDead)
        {
            if (contEnabled)
            {
                ToggleMenu(true);
            }
        }
    }


    private void ToggleMenu()
    {
        menu.ToggleGameMenu();

        contEnabled = contEnabled ? false : true;

        if (contEnabled)
        {
            pi.actions.FindAction("Look").Enable();
        }
        else
        {
            pi.actions.FindAction("Look").Disable();
        }
    }
    private void ToggleMenu(bool turnOn)
    {
        menu.ToggleGameMenu(turnOn);

        contEnabled = !turnOn;

        if (contEnabled)
        {
            pi.actions.FindAction("Look").Enable();
        }
        else
        {
            pi.actions.FindAction("Look").Disable();
        }
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

    public void RightSpell(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            controller.QueueRight();
        }
        else if (context.canceled)
        {
            controller.UseRight();
        }
    }

    public void MenuInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ToggleMenu();
        }
    }
}
