using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoslideTrigger : MonoBehaviour
{
    [SerializeField] private PlayerEventsPublisher _playerEvents;
    private PlayerController _player;
    private Transform _slopeDirection;
    
    private void Start()
    {
        _player = ChroManager.GetManager<PlayerManager>().GetPlayer();
        _slopeDirection = transform.GetChild(0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_player.gameObject == other.gameObject)
        {
            _playerEvents.SetAutoslide.Invoke(true,_slopeDirection);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_player.gameObject == other.gameObject)
        {
            _playerEvents.SetAutoslide.Invoke(false,_slopeDirection);
        }
    }
}
