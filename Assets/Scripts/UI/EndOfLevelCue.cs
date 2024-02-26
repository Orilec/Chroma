using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EndOfLevelCue : MonoBehaviour
{
    [SerializeField] private NarrativeEventsPublisher _narrativeEvents;
    private Transform _text;
    private void Awake()
    {
        _text = transform.GetChild(0);
        _narrativeEvents.EnterValidateLevel.AddListener(DisplayCue);
    }

    private void DisplayCue(bool display)
    {
        _text.gameObject.SetActive(display);
    }
}
