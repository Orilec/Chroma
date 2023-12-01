using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RespawningState : BaseCharacterState
{
    public RespawningState(PlayerController player, InputReader input) : base(player, input) { }

    public override void OnEnter()
    {
        _playerController.IsRespawning = true;
        _playerController.Rigidbody.position = _playerController.CurrentRespawnPoint.transform.position;
    }

    public override void FixedUpdate()
    {
        if (_playerController.Rigidbody.position == _playerController.CurrentRespawnPoint.transform.position)
        {
            _playerController.IsRespawning = false;
        }
    }
}
