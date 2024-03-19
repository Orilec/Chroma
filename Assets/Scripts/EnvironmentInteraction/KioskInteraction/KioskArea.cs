using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KioskArea : MonoBehaviour
{
    [SerializeField] private NarrativeEventsPublisher _narrativeEvents;
    private int _requiredObjectsCount, _objectsColored;
    private bool _isInArea, _completed;
    private PlayerController _player;
    private void OnEnable()
    {
        _requiredObjectsCount = transform.childCount;
        _narrativeEvents.KioskObjectColored.AddListener(AddColoredObject);
    }

    private void Start()
    {
        _player = ChroManager.GetManager<PlayerManager>().GetPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_player.gameObject == other.gameObject && !_completed)
        {
            _isInArea = true;
            _narrativeEvents.KioskAreaEntered.Invoke(true, _requiredObjectsCount);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_player.gameObject == other.gameObject && !_completed)
        {
            _isInArea = false;
            _narrativeEvents.KioskAreaEntered.Invoke(false, _requiredObjectsCount);
        }
    }

    private void AddColoredObject()
    {
        if (_isInArea)
        {
            _objectsColored++;
            if (_objectsColored == _requiredObjectsCount)
            {
                DebugAreaCompleted();
                _narrativeEvents.KioskAreaCompleted.Invoke();
            }
        }
    }

    private void DebugAreaCompleted()
    {
        _completed = true;
        _isInArea = false;
        _narrativeEvents.KioskAreaEntered.Invoke(false, _requiredObjectsCount);
        Debug.Log("Area Completed" + this.name);
    }
}
