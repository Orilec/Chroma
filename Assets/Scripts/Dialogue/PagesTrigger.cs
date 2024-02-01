using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PagesTrigger : MonoBehaviour
{
    [SerializeField] private PlayerEventsPublisher _playerEvents;
    [SerializeField] private int _nbPagesToAdd;
    private bool _pagesAdded;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null && !_pagesAdded)
        {
            _playerEvents.AddingPages.Invoke(_nbPagesToAdd);
            _pagesAdded = true;
        }
    }
}
