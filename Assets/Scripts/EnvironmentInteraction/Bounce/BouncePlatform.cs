using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlatform : MonoBehaviour
{
    [SerializeField]private BounceParameters _parameters;
    public float BounceTime { get { return _parameters.bounceTime; } }
    public float BounceInitialForce { get { return _parameters.bounceInitialForce; } }
    public float BounceContinualForceMultiplier { get { return _parameters.bounceContinualForceMultiplier; } }

    private void OnCollisionEnter(Collision other)
    {
        var player = other.transform.GetComponent<PlayerController>();
        if (player != null)
        {
            player.UpdateBouncePlatform(this);
        }
    }
}
