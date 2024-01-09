using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedOnEnvironmentState : BaseCharacterState
{
    
    public GroundedOnEnvironmentState(PlayerController player, InputReader input) : base(player, input) { }
    
    
    public override void OnEnter()
    {
        _playerController.PlayerFallTimer.Stop();
        _playerController.PlayerFallTimer.Reset(_playerController.PlayerFallTimeMax);
        _playerController.InitialJump = false;
        _playerController.CoyoteTimeCounter.Stop();
        _playerController.JumpTimer.Reset();
    }
    
    public override void FixedUpdate()
    {
        HandleMovement();
        _playerController.PlayerMove();
        _playerController.HandleRotation();
    }
    
    private void HandleMovement()
    {
        var gravity = -1f;
        _playerController.GravityFallCurrent = _playerController.GravityFallMin;
        _playerController.PlayerMoveInputY = gravity;
        
        if (_input.MoveInput.magnitude > 0f )
        {
            if (_playerController.CurrentSpeed < _playerController.MaxMoveSpeed)
            {
                _playerController.CurrentSpeed += _playerController.MoveSpeedIncrement;
            }
        }
        else
        {
            _playerController.CurrentSpeed = _playerController.BaseMoveSpeed;
        }
    }
    
}
