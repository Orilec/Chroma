using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class JumpState : BaseCharacterState
{
    public JumpState(PlayerController player, InputReader input) : base(player, input) { }

    private bool _initialJump;

    
    public override void OnEnter()
    {
        
    }

    public override void Update()
    {
        OnJump();
        OnSlide();
    }
    
    public override void FixedUpdate()
    {
        HandleJump();
        _playerController.PlayerMove();
        _playerController.HandleRotation();
    }
    
    public override void OnExit()
    {
        _playerController.PlayerFallTimer.Start();
        _playerController.CoyoteTimeCounter.Stop();
        _playerController.InitialJump = false;
    }
    
    private void HandleJump()
    {
        var calculatedJumpInput = _playerController.PlayerMoveInputY;
        if (!_playerController.InitialJump)
        {
            calculatedJumpInput = _playerController.InitialJumpForce;
            _playerController.InitialJump = true;
        }
        else
        {
            calculatedJumpInput = _playerController.InitialJumpForce * _playerController.ContinualJumpForceMultiplier;
        }
        _playerController.PlayerMoveInputY =  calculatedJumpInput; 
    }
    
    void OnJump()
    {
        if (!_input.JumpIsPressed)
        {
            _playerController.JumpTimer.Stop();
        }
    }
    
    void OnSlide()
    {
        if (_input.SlideIsPressed)
        {
            _playerController.AirSlideTimer.Start();
        }
    }
}
