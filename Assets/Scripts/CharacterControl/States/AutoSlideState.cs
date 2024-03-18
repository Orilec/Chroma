using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSlideState : BaseCharacterState
{
    public AutoSlideState(PlayerController player, InputReader input, PlayerEventsPublisher playerEvents) : base(player, input, playerEvents) { }
    private bool _slopeCoroutineStarted = false;
    public override void OnEnter()
    {
        _playerController.IsAutoSliding = true;
        _playerEvents.Sliding.Invoke(true);
        _playerController.Trail.EnableSlideTrail();
    }
    
    public override void Update()
    {
        OnJump();
    }
    public override void FixedUpdate()
    {
        _playerController.PlayerMoveInputZ = 1;
        
        _playerController.PlayerMove();
        
        if (!_slopeCoroutineStarted)
        {
            _playerController.StopCoroutine(_playerController.DecelerationCoroutine);
            _playerController.AccelerationCoroutine = _playerController.Accelerate(_playerController.SlopeSlideMaxSpeed, _playerController.SlopeSlideSpeedIncrementAmount);
            _playerController.StartCoroutine(_playerController.AccelerationCoroutine);
            _slopeCoroutineStarted = true;
        }
        
        _playerController.HandleRotation();
    }
    
    public override void OnExit()
    {
        _playerController.IsAutoSliding = false;
        _playerController.StopCoroutine(_playerController.AccelerationCoroutine);
        _slopeCoroutineStarted = false;
        _playerController.DecelerationCoroutine = _playerController.Decelerate(_playerController.CurrentMaxSpeed, _playerController.SlideSpeedDecrementAmount);
        _playerController.StartCoroutine(_playerController.DecelerationCoroutine);
        _playerController.Trail.DisableSlideTrail();
        _playerEvents.Sliding.Invoke(false);
    }
    
    void OnJump()
    {
        if (_input.JumpIsPressed && !_playerController.JumpWasPressedLastFrame)
        {
            _playerController.SlidingJumpTimer.Start();
        }
        _playerController.JumpWasPressedLastFrame = _input.JumpIsPressed;
    }
}
