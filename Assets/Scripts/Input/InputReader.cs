
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private string _previousControlScheme = "";
    private const string _gamepadScheme = "Gamepad";
    private const string _mouseScheme = "KeyboardMouse";
    public Vector2 MoveInput { get; private set; } = Vector2.zero;
    public Vector2 NavigateInput { get; private set; } = Vector2.zero;
    public bool MoveIsPressed { get; private set; } = false;
    public bool ClickIsPressed { get; private set; } = false;
    
    public Vector2 LookInput { get; private set; } = Vector2.zero;
    public bool InvertMouseY { get; private set; } = true;
    
    public bool JumpIsPressed { get; private set; } = false; 
    
    public bool SlideIsPressed { get; private set; } = false; 
    public bool ThrowIsPressed { get; private set; } = false; 
    public bool RecenterCameraIsPressed { get; private set; } = false; 
    public bool DisplayNotebook { get; private set; } = false; 
    public bool ValidateLevel { get; private set; } = false; 
    public bool DebugRespawn { get; private set; } = false;
    public bool IsInUI { get; private set; } = false; 
    
    
    private CharacterInputActions _input;
    private PlayerInput _playerInput;
    
    public CharacterInputActions PlayerInput { get { return _input; } }

    private void Awake()
    {
        _input = new  CharacterInputActions();
        _input.Enable();
    }

    private void OnEnable()
    {
        _input.CharacterControls.Enable();
        _input.Interaction.Enable();
        _input.UI.Enable();
        
        //_playerInput.onControlsChanged += OnControlsChanged;

        _input.CharacterControls.Move.performed += SetMove;
        _input.CharacterControls.Move.canceled += SetMove;

        _input.CharacterControls.Look.performed += SetLook;
        _input.CharacterControls.Look.canceled += SetLook;

        _input.CharacterControls.Jump.started += SetJump;
        _input.CharacterControls.Jump.canceled += SetJump;

        _input.CharacterControls.Slide.started += SetSlide;
        _input.CharacterControls.Slide.canceled += SetSlide;
        
        _input.CharacterControls.ThrowSidekick.started += SetThrow;
        _input.CharacterControls.ThrowSidekick.canceled += SetThrow;

        _input.CharacterControls.RecenterCamera.started += SetRecenter;
        _input.CharacterControls.RecenterCamera.canceled += SetRecenter;
        
        _input.Interaction.DisplayNotebook.started += SetNotebook;
        _input.Interaction.DisplayNotebook.canceled += SetNotebook;
        
        _input.CharacterControls.DebugRespawn.started += SetRespawn;
        _input.CharacterControls.DebugRespawn.canceled += SetRespawn;
        
        _input.Interaction.ValidateLevel.started += SetValidate;
        _input.Interaction.ValidateLevel.canceled += SetValidate;
        
        _input.UI.Navigate.started += SetNavigate;
        _input.UI.Navigate.canceled += SetNavigate;
        
        _input.UI.Click.started += SetClick;
        _input.UI.Click.canceled += SetClick;
        
    }
    private void OnDisable()
    {
        _input.CharacterControls.Disable();
        _input.Interaction.Disable();
        _input.UI.Disable();
        
        _input.CharacterControls.Move.performed -= SetMove;
        _input.CharacterControls.Move.canceled -= SetMove;

        _input.CharacterControls.Look.performed -= SetLook;
        _input.CharacterControls.Look.canceled -= SetLook;

        _input.CharacterControls.Jump.started -= SetJump;
        _input.CharacterControls.Jump.canceled -= SetJump;
        
        _input.CharacterControls.Slide.started -= SetSlide;
        _input.CharacterControls.Slide.canceled -= SetSlide;
        
        _input.CharacterControls.ThrowSidekick.started -= SetThrow;
        _input.CharacterControls.ThrowSidekick.canceled -= SetThrow;
        
        _input.CharacterControls.RecenterCamera.started -= SetRecenter;
        _input.CharacterControls.RecenterCamera.canceled -= SetRecenter;
        
        _input.Interaction.DisplayNotebook.started -= SetNotebook;
        _input.Interaction.DisplayNotebook.canceled -= SetNotebook;
        
        _input.CharacterControls.DebugRespawn.started -= SetRespawn;
        _input.CharacterControls.DebugRespawn.canceled -= SetRespawn;
        
        _input.UI.Navigate.started -= SetNavigate;
        _input.UI.Navigate.canceled -= SetNavigate;
        
        _input.UI.Click.started -= SetClick;
        _input.UI.Click.canceled -= SetClick;
    }

    public void DisableCharacterControl()
    {
        _input.CharacterControls.Disable();
    }
    public void EnableCharacterControl()
    {
        _input.CharacterControls.Enable();
    }
    public void EnableUIControl()
    {
        _input.UI.Enable();
        IsInUI = true;
    }
    public void DisableUIControl()
    {
        _input.UI.Disable();
        IsInUI = false;
    }
    
    private void OnControlsChanged(PlayerInput input)
    {
        Debug.Log("controls changed");
        if (IsInUI)
        {
            if (_playerInput.currentControlScheme == _mouseScheme && _previousControlScheme != _mouseScheme)
            {
                Cursor.visible = true;
                _previousControlScheme = _mouseScheme;
            }
            else if (_playerInput.currentControlScheme == _gamepadScheme && _previousControlScheme != _gamepadScheme)
            {
                Cursor.visible = false;
                _previousControlScheme = _gamepadScheme;
            }
        }
    }
    
    private void SetMove(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();
        MoveIsPressed = !(MoveInput == Vector2.zero); 
    }
    
    private void SetNavigate(InputAction.CallbackContext ctx)
    {
        NavigateInput = ctx.ReadValue<Vector2>();
    }
    private void SetClick(InputAction.CallbackContext ctx)
    {
        ClickIsPressed = ctx.started;
    }

    private void SetLook(InputAction.CallbackContext ctx)
    {
        LookInput = ctx.ReadValue<Vector2>();
    }

    private void SetJump(InputAction.CallbackContext ctx)
    {
        JumpIsPressed = ctx.started;
    } 
    private void SetSlide(InputAction.CallbackContext ctx)
    {
        SlideIsPressed = ctx.started;
    }
    private void SetThrow(InputAction.CallbackContext ctx)
    {
        ThrowIsPressed = ctx.started;
    }
    
    private void SetRecenter(InputAction.CallbackContext ctx)
    {
        RecenterCameraIsPressed = ctx.started;
    }
    
    private void SetNotebook(InputAction.CallbackContext ctx)
    {
        DisplayNotebook = ctx.started;
    }
    private void SetRespawn(InputAction.CallbackContext ctx)
    {
        DebugRespawn = ctx.started;
    }
    
    private void SetValidate(InputAction.CallbackContext ctx)
    {
        ValidateLevel = ctx.started;
    }
}
