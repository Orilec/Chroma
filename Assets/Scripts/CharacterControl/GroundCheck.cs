using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _groundDistance = 0.08f;
    [SerializeField] private float _groundBelowDistance = 1f;
    [SerializeField] private LayerMask _groundLayers;
    [SerializeField] private LayerMask _environmentLayers;
    [SerializeField] private PlayerEventsPublisher _playerEvents;
    
    private bool previousState;
    public bool IsGrounded { get; set; }
    public bool GroundBelow { get; private set; }
    public bool IsOnEnvironment { get; private set; }

    public RaycastHit GroundCheckHit;
    public RaycastHit EnvironmentCheckHit;
    public bool AutoSlide;
    public bool OnCliff;
    public Transform slopeDirection;

    private void Update()
    {
        IsGrounded = Physics.SphereCast(transform.position, _groundDistance, Vector3.down, out GroundCheckHit, _groundDistance, _groundLayers);
        GroundBelow = Physics.Raycast(transform.position, Vector3.down, _groundBelowDistance, _groundLayers);
        if (GroundCheckHit.transform != null)
        {
            AutoSlide = GroundCheckHit.transform.CompareTag("AutoSlide");
            OnCliff = GroundCheckHit.transform.CompareTag("Cliff");
            if (AutoSlide) slopeDirection = GroundCheckHit.transform.GetChild(0);
        }
        IsOnEnvironment = Physics.SphereCast(transform.position, _groundDistance, Vector3.down, out EnvironmentCheckHit, _groundDistance, _environmentLayers);
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
