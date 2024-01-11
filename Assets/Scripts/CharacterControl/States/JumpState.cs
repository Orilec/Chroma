using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class JumpState : BaseCharacterState
{
    public JumpState(PlayerController player, InputReader input, PlayerEventsPublisher playerEvents) : base(player, input, playerEvents) { }

    private bool _initialJump;

    
    public override void OnEnter()
    {
        _playerEvents.Jumping.Invoke(true);
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
        _playerEvents.Jumping.Invoke(false);
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
        if (_input.SlideIsPressed && _playerController.CanAirSlide && !_playerController.SlideWasPressedLastFrame)
        {
            _playerController.AirSlideTimer.Start();
        }
        _playerController.SlideWasPressedLastFrame = _input.SlideIsPressed;
    }
}
