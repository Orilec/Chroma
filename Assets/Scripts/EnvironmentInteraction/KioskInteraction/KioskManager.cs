using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KioskManager : MonoBehaviour
{
    [SerializeField] private NarrativeEventsPublisher _narrativeEvents;
    private int _requiredObjectsCount, _objectsColored;
    private bool _isInArea;
    private PlayerController _player;
    private void OnEnable()
    {
        _player = FindObjectOfType<PlayerController>();
        _requiredObjectsCount = transform.childCount;
        _narrativeEvents.KioskObjectColored.AddListener(AddColoredObject);
        _narrativeEvents.KioskAreaCompleted.AddListener(DebugAreaCompleted);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_player.gameObject == other.gameObject)
        {
            _isInArea = true;
            _narrativeEvents.KioskAreaEntered.Invoke(true, _requiredObjectsCount);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_player.gameObject == other.gameObject)
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
                _narrativeEvents.KioskAreaCompleted.Invoke();
            }
        }
    }

    private void DebugAreaCompleted()
    {
        _isInArea = false;
        _narrativeEvents.KioskAreaEntered.Invoke(false, _requiredObjectsCount);
        Debug.Log("Area Completed");
    }
}
