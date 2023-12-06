using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlatform : MonoBehaviour
{
    [SerializeField]private BounceParameters _parameters;

    [SerializeField]private Animator _animator;
    public float BounceTime { get { return _parameters.bounceTime; } }
    public float BounceInitialForce { get { return _parameters.bounceInitialForce; } }
    public float BounceContinualForceMultiplier { get { return _parameters.bounceContinualForceMultiplier; } }
    public float BounceMomentum { get { return _parameters.bounceMomentum; } }
    public float BounceGravityFallMin { get { return _parameters.bounceGravityFallMin; } }
    public float BounceGravityFallMax { get { return _parameters.bounceGravityFallMax; } }
    public float BounceGravityFallIncrementAmount { get { return _parameters.bounceGravityFallIncrementAmount; } }
    public float BounceGravityFallIncrementTime { get { return _parameters.bounceGravityFallIncrementTime; } }

    private void OnCollisionEnter(Collision other)
    {
        var player = other.transform.GetComponent<PlayerController>();
        if (player != null)
        {
            player.UpdateBouncePlatform(this);

            //Play bounce animation
            _animator.SetTrigger("isUsed"); 
            
            
            
        }
    }
}

