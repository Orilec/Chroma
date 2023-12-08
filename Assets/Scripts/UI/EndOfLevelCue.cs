using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevelCue : MonoBehaviour
{
    [SerializeField] private PlayerEventsPublisher _playerEvents;
    private Transform _text;
    private void Awake()
    {
        _text = transform.GetChild(0);
        _playerEvents.EnterValidateLevel.AddListener(DisplayCue);
    }

    private void DisplayCue(bool display)
    {
        _text.gameObject.SetActive(display);
    }
}
