using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CliffState : BaseCharacterState
{
    public CliffState(PlayerController player, InputReader input, PlayerEventsPublisher playerEvents) : base(player, input, playerEvents) { }
    
    public override void OnEnter()
    {
        _playerEvents.OnCliff.Invoke(true);
    }

    public override void FixedUpdate()
    {
        CliffWalk();
        _playerController.PlayerMove();
        _playerController.HandleRotation();
    }
    
    public override void OnExit()
    {
        _playerEvents.OnCliff.Invoke(false);
    }
    
    private void CliffWalk()
    {
        _playerController.PlayerMoveInputY = _playerController.MiasmaGravity;
        _playerController.CurrentSpeed = _playerController.MiasmaSpeed;
    }
}
