using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private Sprite _sprite ;
    [SerializeField] private NarrativeEventsPublisher _narrativeEvents ;
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            _narrativeEvents.CardCollected.Invoke(_sprite);
            Destroy(this.gameObject);
        }
    }
}
