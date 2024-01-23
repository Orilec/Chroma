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

    private void Update()
    {
        HandleShadowPosition();
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
