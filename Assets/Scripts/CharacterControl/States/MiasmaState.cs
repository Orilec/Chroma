using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiasmaState : BaseCharacterState
{
    public MiasmaState(PlayerController player, InputReader input, PlayerEventsPublisher playerEvents) : base(player, input, playerEvents) { }

    public override void OnEnter()
    {
        _playerEvents.InMiasma.Invoke(true);
    }

    public override void FixedUpdate()
    {
        HandleMiasma();
        _playerController.PlayerMove();
        _playerController.HandleRotation();
    }
    
    public override void OnExit()
    {
        _playerEvents.InMiasma.Invoke(false);
    }
    
    private void HandleMiasma()
    {
        _playerController.PlayerMoveInputY = _playerController.MiasmaGravity;
        _playerController.CurrentSpeed = _playerController.MiasmaSpeed;
    }
    
}
