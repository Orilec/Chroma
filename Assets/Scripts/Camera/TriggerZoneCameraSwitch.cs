using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class TriggerZoneCameraSwitch : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private NarrativeEventsPublisher _narrativeEvents;

    private PlayerController _player;
    private void OnTriggerEnter(Collider other)
    {
        if(_player == null) _player = other.GetComponent<PlayerController>();
        
        if (_player.gameObject == other.gameObject)
        {
            _narrativeEvents.TriggerCamera.Invoke(_camera, true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (_player.gameObject == other.gameObject)
        {
            _narrativeEvents.TriggerCamera.Invoke(_camera, false);
        }
    }
    

}
