using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndNarratorTextDisplay : MonoBehaviour
{
    private PlayerController _player;
    private NarratorTextDisplay _textDisplay;
    private bool _triggered;
    private void Awake()
    {
        _textDisplay = GetComponentInParent<NarratorTextDisplay>();
        _player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player.gameObject && !_triggered)
        {
            _triggered = true;
            _textDisplay.EndDisplay();
        }
    }
}
