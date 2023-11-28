using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirSlideState : BaseCharacterState
{
    private float _forceMultiplier = 1f;
    public AirSlideState(PlayerController player, InputReader input) : base(player, input) { }
    
    public override void FixedUpdate()
    {
        AirSlide();
        _playerController.HandleRotation();
    }
    private void AirSlide()
    {
        _playerController.PlayerMoveInput =(_playerController.transform.forward * (_playerController.AirSlideBaseForce * _playerController.Rigidbody.mass * _forceMultiplier));
        _forceMultiplier *= _playerController.AirSlideForceMultiplier;
    }

    public override void OnExit()
    {
        _forceMultiplier = 1f;
    }
}
