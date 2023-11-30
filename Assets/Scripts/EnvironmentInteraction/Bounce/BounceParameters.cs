using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BounceParameters : ScriptableObject
{
    [Header("Bounce Parameters")] 
    public float bounceInitialForce = 1500f;
    public float bounceContinualForceMultiplier = 0.1f;
    public float bounceTime = 0.3f;
    
    [Header("Bounce Gravity")]
    public float bounceGravityFallMin = -10f;
    public float bounceGravityFallMax = -150f;
    public float bounceGravityFallIncrementAmount = -20.0f;
    public float bounceGravityFallIncrementTime = 0.05f;
    public float bounceMomentum = 0.3f;
    
}
