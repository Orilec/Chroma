using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudioManager : MonoBehaviour
{
    [SerializeField] private PlayerEventsPublisher _playerEvents;
    [SerializeField] private AkEvent _jumpAkEvent;
    [SerializeField] private AkEvent _landingAkEvent;
    [SerializeField] private AkEvent _landingRollAkEvent;
    [SerializeField] private AkEvent _footstepsAkEvent;

    private void Start()
    {

    }

    private void fireJumpingAudio()
    {

    }
}
