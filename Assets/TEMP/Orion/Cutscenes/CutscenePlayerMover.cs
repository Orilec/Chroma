using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutscenePlayerMover : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private PlayerEventsPublisher _playerEvents;
    private PlayerController _player;

    private void Start()
    {
        ChroManager.GetManager<PlayerManager>().GetPlayer();
    }

    public void MovePlayer()
    {
        _player.Rigidbody.transform.position = _transform.position + Vector3.up;
    }

    public void EndOfCutscene()
    {
        _playerEvents.GroundedState.Invoke(true);
    }
}
