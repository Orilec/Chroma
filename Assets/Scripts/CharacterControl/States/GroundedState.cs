using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundedState : BaseCharacterState
{
    public GroundedState(PlayerController player, InputReader input, PlayerEventsPublisher playerEvents) : base(player, input, playerEvents) { }

    public override void OnEnter()
    {
        _playerController.PlayerFallTimer.Stop();
        _playerController.PlayerFallTimer.Reset(_playerController.PlayerFallTimeMax);
        _playerController.InitialJump = false;
        _playerController.CoyoteTimeCounter.Stop();
        _playerController.JumpTimer.Reset();
        _playerEvents.GroundedState.Invoke(true);

    }
    
    public override void Update()
    {
        OnSlide();
        OnJump();
    }


    public override void FixedUpdate()
    {
        HandleMovement();
        _playerController.PlayerMove();
        _playerController.HandleRotation();
    }
    
    private void HandleMovement()
    {
         var gravity = 0f;
        _playerController.GravityFallCurrent = _playerController.GravityFallMin;
        _playerController.PlayerMoveInputY = gravity;
        
        if (_input.MoveInput.magnitude > 0f )
        {
            //_playerController.Trail.EnableDustParticles(); 

            if (_playerController.CurrentSpeed < _playerController.MaxMoveSpeed)
            {
                _playerController.CurrentSpeed += _playerController.MoveSpeedIncrement;
            }
        }
        else
        {
            //_playerController.Trail.DisableDustParticles();
            _playerController.CurrentSpeed = _playerController.BaseMoveSpeed;
        }
    }

    public override void OnExit()
    {
        _playerController.CoyoteTimeCounter.Start();
        _playerController.CanAirSlide = true;
        _playerEvents.GroundedState.Invoke(false);
        //_playerController.Trail.DisableDustParticles();
    }

    void OnSlide()
    {
        if (_input.SlideIsPressed && !_playerController.SlideWasPressedLastFrame && !_playerController.SlideCooldownTimer.IsRunning)
        {
            _playerController.SlideTimer.Start();
        }
        _playerController.SlideWasPressedLastFrame = _input.SlideIsPressed;
    }
    
    void OnJump()
    {
        if (_input.JumpIsPressed && !_playerController.JumpWasPressedLastFrame || _playerController.JumpBufferTimeCounter.IsRunning)
        {
            if (_playerController.SlidingJumpBufferCounter.IsRunning)
            {
                _playerController.SlidingJumpTimer.Start();
            }
            else
            {
                _playerController.JumpTimer.Start();
            }
        }
        _playerController.JumpWasPressedLastFrame = _input.JumpIsPressed;
    }
}
