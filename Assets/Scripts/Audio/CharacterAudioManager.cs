using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudioManager : MonoBehaviour
{
    [SerializeField] private PlayerEventsPublisher _playerEvents;
    [SerializeField] private AK.Wwise.Event _jumpAkEvent;
    [SerializeField] private AK.Wwise.Event _landingAkEvent;
    [SerializeField] private AK.Wwise.Event _landingRollAkEvent;
    [SerializeField] private AK.Wwise.Event _footstepsAkEvent;

    private IEnumerator _coroutine;
    private void Start()
    {
        _playerEvents.Jumping.AddListener(FireJumpingAudio);
        _playerEvents.EnteringGround.AddListener(FireLandingAudio);
    }

    private void FireJumpingAudio(bool jumping)
    {
        if(jumping) _jumpAkEvent.Post(this.gameObject);
    }
    private void FireLandingAudio(Transform objectUnderFeet)
    {
        _landingAkEvent.Post(this.gameObject);
    }

   
}
