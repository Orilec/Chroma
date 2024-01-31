using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiasmaParticleCollision : MonoBehaviour
{
    [SerializeField] private Collider _miasmaColliderPrefab;
    [SerializeField] private Transform _collisionHolder;
    private ParticleSystem _particleSystem;
    private ParticleSystem.Particle[] _particles; 

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _particles = new ParticleSystem.Particle[_particleSystem.main.maxParticles];
    }

    private void Start()
    {
        Invoke(nameof(SpawnColliders), 1f);
    }

    private void SpawnColliders()
    {
        var count = _particleSystem.GetParticles(_particles);
        foreach (var particle in _particles)
        {
            Instantiate(_miasmaColliderPrefab.gameObject, particle.position, Quaternion.identity, _collisionHolder);
        }
    }
}
