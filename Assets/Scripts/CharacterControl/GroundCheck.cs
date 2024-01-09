using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private float _groundDistance = 0.08f;
    [SerializeField] private LayerMask _groundLayers;
    [SerializeField] private LayerMask _environmentLayers;
    [SerializeField] private PlayerEventsPublisher _playerEvents;
    
    private bool previousState;
    public bool IsGrounded { get; private set; }
    public bool IsOnEnvironment { get; private set; }

    public RaycastHit GroundCheckHit;

    private void Update()
    {
        IsGrounded = Physics.SphereCast(transform.position, _groundDistance, Vector3.down, out GroundCheckHit, _groundDistance, _groundLayers);
        IsOnEnvironment = Physics.SphereCast(transform.position, _groundDistance, Vector3.down, out GroundCheckHit, _groundDistance, _environmentLayers);
        if (IsGrounded == false && IsGrounded != previousState)
        {
            _playerEvents.LeavingGround.Invoke();
        }
        else if (IsGrounded == true && IsGrounded != previousState)
        {
            _playerEvents.EnteringGround.Invoke();
        }
    }

    private void LateUpdate()
    {
        previousState = IsGrounded;
    }
}
