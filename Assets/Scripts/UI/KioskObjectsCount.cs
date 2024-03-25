using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KioskObjectsCount : MonoBehaviour
{
    [SerializeField] private NarrativeEventsPublisher _narrativeEvents;
    [SerializeField] private TextMeshProUGUI _count;
    [SerializeField] private TextMeshProUGUI _total;
    private Transform _cueTransform;
    private int _currentCount;

    private void OnEnable()
    {
        _cueTransform = transform.GetChild(0);
        _narrativeEvents.KioskAreaEntered.AddListener(DisplayCount);
        _narrativeEvents.KioskObjectColored.AddListener(AddObjectToCount);
        _narrativeEvents.KioskAreaCompleted.AddListener(ResetCount);
        ResetCount();
    }

    private void DisplayCount(bool display, int total)
    {
        _total.text = total.ToString();
        _cueTransform.gameObject.SetActive(display);
    }

    private void AddObjectToCount()
    {
        _currentCount++;
        _count.text = _currentCount.ToString();
    }

    private void ResetCount()
    {
        _currentCount = 0;
        _count.text = _currentCount.ToString();
    }
}
