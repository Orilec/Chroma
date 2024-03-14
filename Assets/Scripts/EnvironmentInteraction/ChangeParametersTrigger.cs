using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeParametersTrigger : MonoBehaviour
{
    [SerializeField] private PlayerEventsPublisher _playerEvents;
    [SerializeField] private float _newBaseSpeed;
    [SerializeField] private float _newMaxSpeed;
    
    private PlayerController _player;
    
    private void Start()
    {
        _player = ChroManager.GetManager<PlayerManager>().GetPlayer();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_player.gameObject == other.gameObject)
        {
            _playerEvents.ChangeBaseSpeed.Invoke(true, _newBaseSpeed, _newMaxSpeed);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_player.gameObject == other.gameObject)
        {
            _playerEvents.ChangeBaseSpeed.Invoke(false, _newBaseSpeed, _newMaxSpeed);
        }
    }
    
    
}
