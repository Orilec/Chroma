using UnityEngine;
using UnityEngine.Events;

public class BounceFallState : BaseCharacterState
{
    public BounceFallState(PlayerController player, InputReader input, PlayerEventsPublisher playerEvents) : base(player, input, playerEvents) {}


    public override void OnEnter()
    {
        ;
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
        _playerController.PlayerFallTimer.Reset(_playerController.CurrentBouncePlatform.BounceMomentum);
    }
    
    private void HandleFallGravity()
    {
        if (_playerController.BounceFallTimer.IsRunning)
        {
            _playerController.PlayerMoveInputY = 0f;
            return;
        }
        
        if (_playerController.GravityFallCurrent > _playerController.CurrentBouncePlatform.BounceGravityFallMax)
        {
            _playerController.GravityFallCurrent += _playerController.CurrentBouncePlatform.BounceGravityFallIncrementAmount;
        }
        
        _playerController.BounceFallTimer.Reset(_playerController.CurrentBouncePlatform.BounceGravityFallIncrementTime);
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
