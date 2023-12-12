
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; } = Vector2.zero;
    public bool MoveIsPressed { get; private set; } = false;
    
    public Vector2 LookInput { get; private set; } = Vector2.zero;
    public bool InvertMouseY { get; private set; } = true;
    
    public bool JumpIsPressed { get; private set; } = false; 
    
    public bool SlideIsPressed { get; private set; } = false; 
    public bool ThrowIsPressed { get; private set; } = false; 
    public bool RecenterCameraIsPressed { get; private set; } = false; 
    public bool DisplayNotebook { get; private set; } = false; 
    public bool ValidateLevel { get; private set; } = false; 
    public bool DebugRespawn { get; private set; } = false; 
    
    
    CharacterInputActions _input;
    
    public CharacterInputActions PlayerInput { get { return _input; } }

    private void OnEnable()
    {
        _input = new  CharacterInputActions();
        _input.CharacterControls.Enable();
        _input.Interaction.Enable();

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
        
        _input.CharacterControls.DisplayNotebook.started += SetNotebook;
        _input.CharacterControls.DisplayNotebook.canceled += SetNotebook;
        
        _input.CharacterControls.DebugRespawn.started += SetRespawn;
        _input.CharacterControls.DebugRespawn.canceled += SetRespawn;
        
        _input.Interaction.ValidateLevel.started += SetValidate;
        _input.Interaction.ValidateLevel.canceled += SetValidate;
    }
    private void OnDisable()
    {
        _input.CharacterControls.Disable();

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
        
        _input.CharacterControls.DisplayNotebook.started -= SetNotebook;
        _input.CharacterControls.DisplayNotebook.canceled -= SetNotebook;
        
        _input.CharacterControls.DebugRespawn.started -= SetRespawn;
        _input.CharacterControls.DebugRespawn.canceled -= SetRespawn;
    }

    public void DisableCharacterControl()
    {
        _input.CharacterControls.Disable();
    }
    public void EnableCharacterControl()
    {
        _input.CharacterControls.Enable();
    }
    
    private void SetMove(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();
        MoveIsPressed = !(MoveInput == Vector2.zero); 
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
