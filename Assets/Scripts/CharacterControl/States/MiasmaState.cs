using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiasmaState : BaseCharacterState
{
    public MiasmaState(PlayerController player, InputReader input) : base(player, input) { }
    
    public override void FixedUpdate()
    {
        HandleMiasma();
        _playerController.PlayerMove();
        _playerController.HandleRotation();
    }
    
    private void HandleMiasma()
    {
        _playerController.PlayerMoveInputY = _playerController.MiasmaGravity;
        _playerController.CurrentSpeed = _playerController.MiasmaSpeed;
    }
    
}
