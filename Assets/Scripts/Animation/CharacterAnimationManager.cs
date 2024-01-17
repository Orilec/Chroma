using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerEventsPublisher _playerEvents;
    private int _jumpingHash = Animator.StringToHash("IsJumping");
    private int _slidingHash = Animator.StringToHash("IsSliding");
    private int _airSlidingHash = Animator.StringToHash("IsAirDashing");
    private int _slideJumpingHash = Animator.StringToHash("IsSlideJumping");
    private int _dyingHash = Animator.StringToHash("IsDying");
    private int _groundedHash = Animator.StringToHash("IsGrounded");
    private int _currentSpeedHash = Animator.StringToHash("MoveSpeed");

    private void Awake()
    {
        _playerEvents.Jumping.AddListener(Jumping);
        _playerEvents.Sliding.AddListener(Sliding);
        _playerEvents.AirSliding.AddListener(AirSliding);
        _playerEvents.SlideJumping.AddListener(SlideJumping);
        _playerEvents.Dying.AddListener(Dying);
        _playerEvents.LocomotionSpeed.AddListener(CurrentSpeed);
        _playerEvents.GroundedState.AddListener(Grounded);
    }

    private void Jumping(bool isJumping)
    {
        _animator.SetBool(_jumpingHash, isJumping);
    }
    
    private void Sliding(bool isSliding)
    {
        _animator.SetBool(_slidingHash, isSliding);
    }
    private void AirSliding(bool isAirSliding)
    {
        _animator.SetBool(_airSlidingHash, isAirSliding);
    }
    private void SlideJumping(bool isSlideJumping)
    {
        _animator.SetBool(_slideJumpingHash, isSlideJumping);
    }
    private void Dying(bool isDying)
    {
        _animator.SetBool(_dyingHash, isDying);
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
