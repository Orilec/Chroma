using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideBoostState : BaseCharacterState
{
    public SlideBoostState(PlayerController player, InputReader input, PlayerEventsPublisher playerEvents) : base(player, input, playerEvents) { }

    private Vector3 _direction, _calculatedJumpInput;
    public override void OnEnter()
    {
        _direction = _playerController.GroundCheckHitNormal.normalized;
    }

    public override void FixedUpdate()
    {
        HandleJump();
        _playerController.HandleRotation();
    }
    
    public override void OnExit()
    {
        _playerController.PlayerFallTimer.Start();
        _playerController.CoyoteTimeCounter.Stop();
        _playerController.InitialJump = false;
        _playerController.WasSlideJumping = true;
    }
    
    private void HandleJump()
    {
        if (!_playerController.InitialJump)
        {
            _calculatedJumpInput = _direction * _playerController.InitialSlideBoostForce;
            _playerController.InitialJump = true;
        }
        else
        {
            _calculatedJumpInput *= _playerController.ContinualSlideBoostForceMultiplier;
        }
        _playerController.PlayerMoveInput =  _calculatedJumpInput; 
    }
    
    
}
