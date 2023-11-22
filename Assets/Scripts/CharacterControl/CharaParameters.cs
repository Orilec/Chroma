using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharaParameters : ScriptableObject
{
    [Header("Movement Parameters")] 
    [SerializeField] private float _maxMoveSpeed = 6f;
    [SerializeField] private float _baseMoveSpeed = 3f;
    [SerializeField] private float _speedIncrement = 0.5f;
    [SerializeField] private float _currentMoveSpeed = 0f;
    [SerializeField] private float _rotationSpeed = 15f;
    private Vector3 _playerMoveInput, _appliedMovement, _cameraRelativeMovement ;
    
    [Header("Gravity")]
    [SerializeField] private float _gravityFallCurrent = -100.0f;
    [SerializeField] private float _gravityFallMin = -100.0f;
    [SerializeField] private float _gravityFallMax = -500.0f;
    [SerializeField] [Range(-5f, -35f)] private float _gravityFallIncrementAmount = -20.0f;
    [SerializeField] private float _gravityFallIncrementTime = 0.05f;
    [SerializeField] private float _playerFallTimeMax = 0.3f;

    [Header("Jump Parameters")] 
    [SerializeField] float _initialJumpForce = 750.0f;
    [SerializeField] float _continualJumpForceMultiplier = 0.1f;
    [SerializeField] float _jumpTime = 0.175f;
    [SerializeField] float _coyoteTime = 0.15f;
    [SerializeField] float _jumpBufferTime = 0.2f;
}
