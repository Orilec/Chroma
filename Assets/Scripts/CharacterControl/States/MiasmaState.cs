using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiasmaState : BaseCharacterState
{
    public MiasmaState(PlayerController player, InputReader input) : base(player, input) { }

    public override void OnEnter()
    {
        Debug.Log("entering miasma");
    }

    public override void FixedUpdate()
    {
        HandleMiasma();
        _playerController.PlayerMove();
        _playerController.HandleRotation();
    }
    
    private void HandleMiasma()
    {
        var gravity = -5f;
        _playerController.GravityFallCurrent = _playerController.GravityFallMin;
        _playerController.PlayerMoveInputY = gravity;
        _playerController.CurrentSpeed = _playerController.MiasmaSpeed;
    }

    public override void OnExit()
    {
        Debug.Log("exiting miasma");
    }
}
