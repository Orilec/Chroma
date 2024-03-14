using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PagesTrigger : MonoBehaviour
{
    [SerializeField] private NarrativeEventsPublisher _narrativeEvents;
    [SerializeField] private int _nbPagesToAdd;
    private bool _pagesAdded;

    private PlayerController _player;
    
    private void Start()
    {
        _player = ChroManager.GetManager<PlayerManager>().GetPlayer();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player.gameObject && !_pagesAdded)
        {
            _narrativeEvents.AddingPages.Invoke(_nbPagesToAdd);
            _pagesAdded = true;
        }
    }
}
