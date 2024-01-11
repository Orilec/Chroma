using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BounceState : BaseCharacterState
{
    public BounceState(PlayerController player, InputReader input, PlayerEventsPublisher playerEvents) : base(player, input, playerEvents) { }

    private bool _initialJump;

    
    public override void OnEnter()
    {
        _playerController.CanAirSlide = true;
    }

    public override void Update()
    {
        
    }
    
    public override void FixedUpdate()
    {
        HandleJump();
        _playerController.PlayerMove();
        _playerController.HandleRotation();
    }
    
    public override void OnExit()
    {
        _playerController.BounceFallTimer.Reset(_playerController.CurrentBouncePlatform.BounceMomentum); 
        _playerController.BounceFallTimer.Start();
        _playerController.CoyoteTimeCounter.Stop();
        _playerController.InitialJump = false;
    }
    
    private void HandleJump()
    {
        var calculatedJumpInput = _playerController.PlayerMoveInputY;
        if (!_playerController.InitialJump)
        {
            calculatedJumpInput = _playerController.CurrentBouncePlatform.BounceInitialForce;
            _playerController.InitialJump = true;
        }
        else
        {
            calculatedJumpInput = _playerController.CurrentBouncePlatform.BounceInitialForce * _playerController.CurrentBouncePlatform.BounceContinualForceMultiplier;
        }
        _playerController.PlayerMoveInputY =  calculatedJumpInput; 
    }
    
}
