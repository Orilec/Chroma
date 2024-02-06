using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    [SerializeField] private PlayerEventsPublisher _playerEvents;
    public static bool IsPaused;

    private void Awake()
    {
        _playerEvents.PauseGame.AddListener(Pause);
    }

    private void Pause(bool paused)
    {
        if (paused)
        {
            IsPaused = true;
            Time.timeScale = 0f;
        }
        else
        {
            IsPaused = false;
            Time.timeScale = 1f;
        }
    }
}
