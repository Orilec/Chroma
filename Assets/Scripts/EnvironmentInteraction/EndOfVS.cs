using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfVS : MonoBehaviour
{
    [SerializeField] private PlayerEventsPublisher _playerEvents;
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            _playerEvents.EndOfVSLevel.Invoke();
        }
    }
}
