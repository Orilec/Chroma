using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    [SerializeField] private PlayerEventsPublisher _playerEvents;

    private void Awake()
    {
        _playerEvents.PauseGame.AddListener(Pause);
    }

    private void Pause(bool paused)
    {
        if (paused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
