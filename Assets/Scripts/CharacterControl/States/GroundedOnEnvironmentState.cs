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
    }

}
