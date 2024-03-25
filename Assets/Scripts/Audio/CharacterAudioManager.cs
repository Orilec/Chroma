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
    [SerializeField] private float _footstepsSpeed = 0.5f;

    private bool _isGrounded = true;
    private float _movingSpeed;
    private void Start()
    {
        _playerEvents.Jumping.AddListener(FireJumpingAudio);
        _playerEvents.EnteringGround.AddListener(FireLandingAudio);
        _playerEvents.LocomotionSpeed.AddListener(SetMovingSpeed);
        _playerEvents.GroundedState.AddListener(SetIsGrounded);
        StartCoroutine(FootstepsCoroutine());
    }

    private void FireJumpingAudio(bool jumping)
    {
        if(jumping) _jumpAkEvent.Post(this.gameObject);
    }
    private void FireLandingAudio(Transform objectUnderFeet)
    {
        _landingAkEvent.Post(this.gameObject);
    }

    private void SetMovingSpeed(float relativeMoveSpeed)
    {
        _movingSpeed = relativeMoveSpeed;
    }

    private void SetIsGrounded(bool isGrounded)
    {
        _isGrounded = isGrounded;
    }

    private IEnumerator FootstepsCoroutine()
    {
        while (true)
        {
            while (_movingSpeed > 0 && _isGrounded)
            {
                Debug.Log("footstep");
                _footstepsAkEvent.Post(this.gameObject);
                yield return new WaitForSeconds(_footstepsSpeed / _movingSpeed);
            }
            yield return new WaitForEndOfFrame();
        }

    }

   
}
