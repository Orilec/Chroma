using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PagesTrigger : MonoBehaviour
{
    [SerializeField] private NarrativeEventsPublisher _narrativeEvents;
    [SerializeField] private int _nbPagesToAdd;
    private bool _pagesAdded;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null && !_pagesAdded)
        {
            _narrativeEvents.AddingPages.Invoke(_nbPagesToAdd);
            _pagesAdded = true;
        }
    }
}
