using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingJumpState : BaseCharacterState
{
    public SlidingJumpState(PlayerController player, InputReader input) : base(player, input) { }

    public override void OnEnter()
    {
    }

    public override void FixedUpdate()
    {
        if (_playerController.SlidingJumpTimer.Progress < 0.5)
        {
            Jump();
        }
        else
        {
            Fall();
        }
    }

    private void Jump()
    {
        Vector3 calculatedPlayerMovement = (new Vector3(0f,
            _playerController.SlidingJumpVerticalForce * _playerController.Rigidbody.mass,
            _playerController.SlidingJumpHorizontalForce * _playerController.Rigidbody.mass));
        
        Debug.DrawLine(_playerController.transform.position, calculatedPlayerMovement);
        
        _playerController.PlayerMoveInput = calculatedPlayerMovement;
    }
    
    private void Fall()
    {
        Debug.Log("fall");
    }
}
