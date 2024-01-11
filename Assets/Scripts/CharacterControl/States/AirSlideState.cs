using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirSlideState : BaseCharacterState
{
    private float _forceMultiplier = 1f;
    public AirSlideState(PlayerController player, InputReader input, PlayerEventsPublisher playerEvents) : base(player, input, playerEvents) { }
    
    public override void OnEnter()
    {
        _playerController.CanAirSlide = false;
        _playerController.Trail.EnableAirSlideTrail();
    }
    
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
        _playerController.Trail.DisableAirSlideTrail();
        _forceMultiplier = 1f;
    }
}
