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
    [SerializeField] private LayerMask _allElementLayers;
    [SerializeField] private PlayerEventsPublisher _playerEvents;
    
    private bool previousState;
    public bool IsGrounded { get; set; }
    public bool IsOnElement { get; set; }
    public bool GroundBelow { get; private set; }
    public bool IsOnEnvironment { get; private set; }

    public RaycastHit GroundCheckHit;
    public RaycastHit ElementCheckHit;
    public Transform CurrentObjectUnderFeet;

    private void Update()
    {
        IsGrounded = Physics.SphereCast(transform.position, _groundDistance, Vector3.down, out GroundCheckHit, _groundDistance, _groundLayers);
        IsOnElement = Physics.SphereCast(transform.position, _groundDistance, Vector3.down, out ElementCheckHit, _groundDistance, _allElementLayers);
        GroundBelow = Physics.Raycast(transform.position, Vector3.down, _groundBelowDistance, _groundLayers);
        CurrentObjectUnderFeet = ElementCheckHit.transform;
        
        if (IsOnElement == false && IsOnElement != previousState)
        {
            _playerEvents.LeavingGround.Invoke();
        }
        else if (IsOnElement == true && IsOnElement != previousState)
        {
            _playerEvents.EnteringGround.Invoke(CurrentObjectUnderFeet);
        }
    }

    private void LateUpdate()
    {
        previousState = IsGrounded;
    }
}
