using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerEventsPublisher _playerEvents;
    private int _jumpingHash = Animator.StringToHash("IsJumping");
    private int _groundedHash = Animator.StringToHash("IsGrounded");
    private int _currentSpeedHash = Animator.StringToHash("MoveSpeed");

    private void Awake()
    {
        _playerEvents.Jumping.AddListener(Jumping);
        _playerEvents.LocomotionSpeed.AddListener(CurrentSpeed);
        _playerEvents.GroundedState.AddListener(Grounded);
    }

    private void Jumping(bool isJumping)
    {
        _animator.SetBool(_jumpingHash, isJumping);
    }
    
    private void Grounded(bool isGrounded)
    {
        _animator.SetBool(_groundedHash, isGrounded);
    }

    private void CurrentSpeed(float speed)
    {
        _animator.SetFloat(_currentSpeedHash, speed);
    }
    
}
