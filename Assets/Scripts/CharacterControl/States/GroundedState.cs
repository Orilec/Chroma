using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundedState : BaseCharacterState
{
    public GroundedState(PlayerController player, InputReader input) : base(player, input) { }

    public override void OnEnter()
    {
        _playerController.PlayerFallTimer.Stop();
        _playerController.PlayerFallTimer.Reset(_playerController.PlayerFallTimeMax);
        _playerController.InitialJump = false;
        _playerController.CoyoteTimeCounter.Stop();
        _playerController.JumpTimer.Reset();
    }
    
    public override void Update()
    {
        OnSlide();
        OnJump();
    }


    public override void FixedUpdate()
    {
        _playerController.HandleRotation();
        HandleGravity();
        _playerController.PlayerMove();
    }
    
    private void HandleGravity()
    {
         var gravity = 0f;
        _playerController.GravityFallCurrent = _playerController.GravityFallMin;
        _playerController.PlayerMoveInputY = gravity;
    }

    public override void OnExit()
    {
        _playerController.CoyoteTimeCounter.Start();
        
    }

    void OnSlide()
    {
        if (_input.SlideIsPressed && !_playerController.SlideWasPressedLastFrame)
        {
            _playerController.SlideTimer.Start();
        }
        _playerController.SlideWasPressedLastFrame = _input.SlideIsPressed;
    }
    
    void OnJump()
    {
        if (_input.JumpIsPressed && !_playerController.JumpWasPressedLastFrame || _playerController.JumpBufferTimeCounter.IsRunning)
        {
            _playerController.JumpTimer.Start();
        }
        //if slidingJumpBuffer counter is running, start slidingJump Timer
        _playerController.JumpWasPressedLastFrame = _input.JumpIsPressed;
    }
}
