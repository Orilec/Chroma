using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingJumpState : BaseCharacterState
{
    private float _jumpMultiplier = 1f;
    private float _fallMultiplier;
    public SlidingJumpState(PlayerController player, InputReader input, PlayerEventsPublisher playerEvents) : base(player, input, playerEvents) { }

    public override void OnEnter()
    {
        _playerEvents.SlideJumping.Invoke(true);
        _playerController.CanAirSlide = false;
        _fallMultiplier = _playerController.SlidingJumpBaseFallGravity;
        _playerController.CoyoteTimeCounter.Stop();
    }

    public override void FixedUpdate()
    {
        if (_playerController.SlidingJumpTimer.Progress < _playerController.SlidingJumpHalfPointTime)
        {
            Fall();
        }
        else
        {
            Jump();
        }
    }

    private void Jump()
    {
        Vector3 calculatedPlayerYMovement = (new Vector3(0f,
            _playerController.SlidingJumpVerticalForce * _playerController.Rigidbody.mass * _jumpMultiplier,
            0f));

        _jumpMultiplier *= _playerController.SlidingJumpContinualMultiplier;

        _playerController.PlayerMoveInput =(_playerController.transform.forward * (_playerController.SlidingJumpHorizontalForce * _playerController.Rigidbody.mass)) + calculatedPlayerYMovement;
    }
    
    private void Fall()
    {
        Vector3 calculatedPlayerYMovement = (new Vector3(0f,
            _playerController.SlidingJumpVerticalForce * _playerController.Rigidbody.mass * _fallMultiplier,
            0f));

        _fallMultiplier *= _playerController.SlidingJumpFallMultiplier;
        
        _playerController.PlayerMoveInput = (_playerController.transform.forward * (_playerController.SlidingJumpHorizontalForce * _playerController.Rigidbody.mass)) + calculatedPlayerYMovement;
    }
    
    public override void OnExit()
    {
        _jumpMultiplier = 1f;
        _fallMultiplier = _playerController.SlidingJumpBaseFallGravity;
        _playerEvents.SlideJumping.Invoke(false);
    }
}
