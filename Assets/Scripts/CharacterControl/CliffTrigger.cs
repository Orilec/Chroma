using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CliffTrigger : MonoBehaviour
{
    [SerializeField] private PlayerEventsPublisher _playerEvents;
    private PlayerController _player;
    
    private void Start()
    {
        _player = ChroManager.GetManager<PlayerManager>().GetPlayer();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_player.gameObject == other.gameObject)
        {
            _playerEvents.SetOnCliff.Invoke(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_player.gameObject == other.gameObject)
        {
            _playerEvents.SetOnCliff.Invoke(false);
        }
    }
}
