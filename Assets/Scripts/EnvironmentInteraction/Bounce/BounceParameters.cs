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
    
}
