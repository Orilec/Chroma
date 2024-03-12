using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UntargetedSidekickParticle : MonoBehaviour
{
    [SerializeField] private ThrowSystem _throwSystem;
    [SerializeField] private CameraSwitchManager _cameraSwitch;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private float _timeBeforeRetrieve = 0.2f;

    [SerializeField] private InteractorScript _temporaryInteractor; 
    
    private List<ParticleCollisionEvent> _collisionEvents;

    private Quaternion _baseRotation;

    private void Awake()
    {
        _particle = GetComponent<ParticleSystem>();
        _collisionEvents = new List<ParticleCollisionEvent>();
        _baseRotation = transform.localRotation;
    }

    private void FixedUpdate()
    {
        if(_cameraSwitch.IsFlCamera()) transform.rotation = Camera.main.transform.rotation;
        else
        {
            transform.localRotation = _baseRotation;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(_particle, other, _collisionEvents);
        _throwSystem.RetrievePos.position = _collisionEvents[0].intersection;
        Instantiate(_temporaryInteractor.gameObject, _throwSystem.RetrievePos.position, Quaternion.identity);
        _throwSystem.Invoke(nameof(ThrowSystem.RetrieveSidekick), _timeBeforeRetrieve);
    }
}
