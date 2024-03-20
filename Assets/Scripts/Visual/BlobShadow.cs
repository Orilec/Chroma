using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BlobShadow : MonoBehaviour
{
    [SerializeField] private Transform _blobShadowTransform;
    [SerializeField] private float _maxDistance;    
    [SerializeField] private LayerMask _groundLayers;
    [SerializeField] private GroundCheck _groundCheck;
    [SerializeField] private PlayerEventsPublisher _playerEvents;
    private float _originalYpos;
    private void Awake()
    {
        _playerEvents.EnteringGround.AddListener(ResetYPos);
        _originalYpos = _blobShadowTransform.localPosition.y;
    }

    private void Update()
    {
        if(!_groundCheck.IsGrounded) HandleShadowPosition();
    }

    private void ResetYPos(Transform objectUnderFeet)
    {
        _blobShadowTransform.localPosition = new Vector3(_blobShadowTransform.localPosition.x, _originalYpos, _blobShadowTransform.localPosition.z);
    }
    

    private void HandleShadowPosition()
    {
        RaycastHit rayCastHit;
        var raycast = Physics.Raycast(transform.position, Vector3.down, out rayCastHit , _maxDistance, _groundLayers);
        if (raycast)
        {
            _blobShadowTransform.position = new Vector3(_blobShadowTransform.position.x, rayCastHit.point.y, _blobShadowTransform.position.z);
        }
    }
}
