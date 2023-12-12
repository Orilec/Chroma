using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrailScript : MonoBehaviour
{
    [SerializeField] private TrailRenderer _slideTrail;
    [SerializeField]private TrailRenderer _airSlideTrail;
    [SerializeField]private ParticleSystem _dustParticles;
    [SerializeField]private ParticleSystem _landParticles;

    private void Awake()
    {
        DisableSlideTrail();
        DisableAirSlideTrail();
    }

    public void EnableSlideTrail()
    {
        _slideTrail.emitting = true;
    }
    
    public void DisableSlideTrail()
    {
        _slideTrail.emitting = false;
    }
    public void EnableAirSlideTrail()
    {
        _airSlideTrail.emitting = true;
    }
    
    public void DisableAirSlideTrail()
    {
        _airSlideTrail.emitting = false;
    }

    public void EnableDustParticles()
    {
        if (!_dustParticles.isPlaying)
        {
            _dustParticles.Play();
        }
        
    }

    public void DisableDustParticles()
    {
        _dustParticles.Stop();
    }

    public void EnableLandParticles()
    {
        _landParticles.Play(); 
    }

    public void DisableLandParticles()
    {
        _dustParticles.Stop();
    }
}
