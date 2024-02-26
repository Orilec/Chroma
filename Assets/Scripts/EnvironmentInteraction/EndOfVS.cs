using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EndOfVS : MonoBehaviour
{
    [SerializeField] private NarrativeEventsPublisher _narrativeEvents;
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            _narrativeEvents.EndOfVSLevel.Invoke();
        }
    }
}
