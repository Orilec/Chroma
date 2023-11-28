using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlideState : BaseCharacterState
{
    private bool _slopeCoroutineStarted = false;
    public SlideState(PlayerController player, InputReader input) : base(player, input)
    { }

    public override void OnEnter()
    {
        BasicSlide();
        _playerController.Trail.EnableTrail();
    }
    

    public override void Update()
    {
        OnSlide();
        OnJump();
    }

    public override void FixedUpdate()
    {
        _playerController.HandleRotation();
        _playerController.PlayerMove();
        if (_playerController.IsDownSlope && !_slopeCoroutineStarted)
        {
            _playerController.AccelerationCoroutine = _playerController.Accelerate(_playerController.SlopeSlideMaxSpeed, _playerController.SlopeSlideSpeedIncrementAmount);
            _playerController.StartCoroutine(_playerController.AccelerationCoroutine);
            _slopeCoroutineStarted = true;
        }
    }

    public override void OnExit()
    {
        _playerController.StopCoroutine(_playerController.AccelerationCoroutine);
        _slopeCoroutineStarted = false;
        _playerController.StartCoroutine(_playerController.Decelerate(_playerController.MaxMoveSpeed, _playerController.SlideSpeedDecrementAmount));
        _playerController.Trail.DisableTrail();
        _playerController.CoyoteTimeCounter.Start();
        _playerController.SlidingJumpBufferCounter.Start();
    }

    private void BasicSlide()
    {
        _playerController.CurrentSpeed = _playerController.SlideSpeed;
    }
    
    void OnSlide()
    {
        if (!_input.SlideIsPressed)
        {
            _playerController.SlideTimer.Stop();
        }
        _playerController.SlideWasPressedLastFrame = _input.SlideIsPressed;
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
