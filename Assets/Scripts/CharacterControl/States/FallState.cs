using UnityEngine;
using UnityEngine.Events;

public class FallState : BaseCharacterState
{
    public FallState(PlayerController player, InputReader input) : base(player, input) {}


    public override void OnEnter()
    {

    }
    
    public override void Update()
    {
        OnJump();
        OnSlide();
    }

    
    public override void FixedUpdate()
    {
        HandleFallGravity();
        _playerController.PlayerMove();
        _playerController.HandleRotation();
    }

    public override void OnExit()
    {
        if (!_playerController.InitialJump)
        {
            _playerController.InitialJump = true;
        }
        _playerController.PlayerFallTimer.Stop();
        _playerController.PlayerFallTimer.Reset(_playerController.PlayerFallTimeMax);
    }
    
    private void HandleFallGravity()
    {
        if (_playerController.PlayerFallTimer.IsRunning)
        {
            _playerController.PlayerMoveInputY = 0f;
            return;
        }
        
        if (_playerController.GravityFallCurrent > _playerController.GravityFallMax)
        {
            _playerController.GravityFallCurrent += _playerController.GravityFallIncrementAmount;
        }
        
        _playerController.PlayerFallTimer.Reset(_playerController.GravityFallIncrementTime);
        _playerController.PlayerMoveInputY = _playerController.GravityFallCurrent;
    }
    
    void OnJump()
    {
        if (_input.JumpIsPressed && _playerController.CoyoteTimeCounter.IsRunning)
        {
            _playerController.JumpTimer.Start();
        }
        if (_input.JumpIsPressed && !_playerController.JumpBufferTimeCounter.IsRunning && !_playerController.JumpWasPressedLastFrame)
        {
            _playerController.JumpBufferTimeCounter.Start();
        }
        _playerController.JumpWasPressedLastFrame = _input.JumpIsPressed;
    }
    void OnSlide()
    {
        if (_input.SlideIsPressed && _playerController.CanAirSlide && !_playerController.SlideWasPressedLastFrame)
        {
            _playerController.AirSlideTimer.Start();
        }

        _playerController.SlideWasPressedLastFrame = _input.SlideIsPressed;
    }
}
