using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private float _groundDistance = 0.08f;
    [SerializeField] private LayerMask _groundLayers;

    public event UnityAction LeavingGround = delegate {  };
    public event UnityAction EnteringGround = delegate {  };
    
    private bool previousState;
    public bool IsGrounded { get; private set; }

    public RaycastHit GroundCheckHit;

    private void Update()
    {
        IsGrounded = Physics.SphereCast(transform.position, _groundDistance, Vector3.down, out GroundCheckHit, _groundDistance, _groundLayers);
        if (IsGrounded == false && IsGrounded != previousState)
        {
            LeavingGround.Invoke();
        }
        else if (IsGrounded == true && IsGrounded != previousState)
        {
            EnteringGround.Invoke();
        }
    }

    private void LateUpdate()
    {
        previousState = IsGrounded;
    }
}
