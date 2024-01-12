using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RespawningState : BaseCharacterState
{
    public RespawningState(PlayerController player, InputReader input, PlayerEventsPublisher playerEvents) : base(player, input, playerEvents) { }

    public override void OnEnter()
    {
        _playerController.IsRespawning = true;
        _playerController.PlayerEventsPublisher.Respawn.Invoke();
    }

    public override void FixedUpdate()
    {
        if (_playerController.IsFadingToBlack)
        {
            _playerController.Rigidbody.position = _playerController.RespawnSystem.CurrentRespawnPoint.transform.position;
        }
    }
}
