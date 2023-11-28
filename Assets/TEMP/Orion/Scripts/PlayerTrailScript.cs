using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrailScript : MonoBehaviour
{
    private TrailRenderer _trail;

    private void Awake()
    {
        _trail = GetComponent<TrailRenderer>();
        DisableTrail();
    }

    public void EnableTrail()
    {
        _trail.emitting = true;
    }
    
    public void DisableTrail()
    {
        _trail.emitting = false;
    }
}
