using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private Sprite _sprite ;
    [SerializeField] private NarrativeEventsPublisher _narrativeEvents ;

    private PlayerController _player;

    private void Awake()
    {
        _player = ChroManager.GetManager<PlayerManager>().GetPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player.gameObject)
        {
            _narrativeEvents.CardCollected.Invoke(_sprite);
            Destroy(this.gameObject);
        }
    }
}
