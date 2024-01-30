
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private bool _isKeyboardMouse;

    public Vector2 MoveInput { get; private set; } = Vector2.zero;
    public Vector2 NavigateInput { get; private set; } = Vector2.zero;
    public bool MoveIsPressed { get; private set; } = false;
    public bool ClickIsPressed { get; private set; } = false;
    public bool NextSectionIsPressed { get; private set; } = false;
    public bool PreviousSectionIsPressed { get; private set; } = false;
    public bool BackIsPressed { get; private set; } = false;
    
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
    public bool IsKeyboardMouse { get { return _isKeyboardMouse;}}
    private CharacterInputActions _input;
    private PlayerInput _playerInput;
    
    public CharacterInputActions PlayerInputActions { get { return _input; } }

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

        _input.Interaction.ExitNotebook.started += SetBack;
        _input.Interaction.ExitNotebook.canceled += SetBack;
        
        _input.UI.Navigate.started += SetNavigate;
        _input.UI.Navigate.canceled += SetNavigate;
        
        _input.UI.Click.started += SetClick;
        _input.UI.Click.canceled += SetClick;

        _input.UI.NextCategory.started += SetNextCategory;
        _input.UI.NextCategory.canceled += SetNextCategory;
        
        _input.UI.PreviousCategory.started += SetPreviousCategory;
        _input.UI.PreviousCategory.canceled += SetPreviousCategory;
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
        
        _input.Interaction.ExitNotebook.started -= SetBack;
        _input.Interaction.ExitNotebook.canceled -= SetBack;
        
        _input.UI.Navigate.started -= SetNavigate;
        _input.UI.Navigate.canceled -= SetNavigate;
        
        _input.UI.Click.started -= SetClick;
        _input.UI.Click.canceled -= SetClick;
        
        _input.UI.NextCategory.started -= SetNextCategory;
        _input.UI.NextCategory.canceled -= SetNextCategory;
        
        _input.UI.PreviousCategory.started -= SetPreviousCategory;
        _input.UI.PreviousCategory.canceled -= SetPreviousCategory;
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
    
    private void SetMove(InputAction.CallbackContext ctx)
    {
        if ( ctx.control.device is Keyboard or Mouse ) _isKeyboardMouse = true;
        else _isKeyboardMouse = false;
        MoveInput = ctx.ReadValue<Vector2>();
        MoveIsPressed = !(MoveInput == Vector2.zero); 
    }
    
    private void SetNavigate(InputAction.CallbackContext ctx)
    {
        if ( ctx.control.device is Keyboard or Mouse ) _isKeyboardMouse = true;
        else _isKeyboardMouse = false;
        NavigateInput = ctx.ReadValue<Vector2>();
    }
    private void SetClick(InputAction.CallbackContext ctx)
    {
        if ( ctx.control.device is Keyboard or Mouse ) _isKeyboardMouse = true;
        else _isKeyboardMouse = false;
        ClickIsPressed = ctx.started;
    }
    private void SetNextCategory(InputAction.CallbackContext ctx)
    {
        _isKeyboardMouse = false;
        NextSectionIsPressed = ctx.started;
    }
    private void SetPreviousCategory(InputAction.CallbackContext ctx)
    {
        _isKeyboardMouse = false;
        PreviousSectionIsPressed = ctx.started;
    }
    private void SetBack(InputAction.CallbackContext ctx)
    {
        _isKeyboardMouse = false;
        BackIsPressed = ctx.started;
    }

    private void SetLook(InputAction.CallbackContext ctx)
    {
        if ( ctx.control.device is Keyboard or Mouse ) _isKeyboardMouse = true;
        else _isKeyboardMouse = false;
        LookInput = ctx.ReadValue<Vector2>();
    }

    private void SetJump(InputAction.CallbackContext ctx)
    {
        if ( ctx.control.device is Keyboard or Mouse ) _isKeyboardMouse = true;
        else _isKeyboardMouse = false;
        JumpIsPressed = ctx.started;
    } 
    private void SetSlide(InputAction.CallbackContext ctx)
    {
        if ( ctx.control.device is Keyboard or Mouse ) _isKeyboardMouse = true;
        else _isKeyboardMouse = false;
        SlideIsPressed = ctx.started;
    }
    private void SetThrow(InputAction.CallbackContext ctx)
    {
        if ( ctx.control.device is Keyboard or Mouse ) _isKeyboardMouse = true;
        else _isKeyboardMouse = false;
        ThrowIsPressed = ctx.started;
    }
    
    private void SetRecenter(InputAction.CallbackContext ctx)
    {
        if ( ctx.control.device is Keyboard or Mouse ) _isKeyboardMouse = true;
        else _isKeyboardMouse = false;
        RecenterCameraIsPressed = ctx.started;
    }
    
    private void SetNotebook(InputAction.CallbackContext ctx)
    {
        if ( ctx.control.device is Keyboard or Mouse ) _isKeyboardMouse = true;
        else _isKeyboardMouse = false;
        DisplayNotebook = ctx.started;
    }
    private void SetRespawn(InputAction.CallbackContext ctx)
    {
        if ( ctx.control.device is Keyboard or Mouse ) _isKeyboardMouse = true;
        else _isKeyboardMouse = false;
        DebugRespawn = ctx.started;
    }
    
    private void SetValidate(InputAction.CallbackContext ctx)
    {
        if ( ctx.control.device is Keyboard or Mouse ) _isKeyboardMouse = true;
        else _isKeyboardMouse = false;
        ValidateLevel = ctx.started;
    }
}
