using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidekickParticle : MonoBehaviour
{
    [SerializeField] private ThrowSystem _throwSystem;
    [SerializeField] private float _timeBeforeRetrieve = 0.2f;
    private void OnParticleCollision(GameObject other)
    {
        var target = other.transform.GetComponent<Target>();
        if (target != null)
        {
            target.OnActivate();
            _throwSystem.Invoke(nameof(ThrowSystem.RetrieveSidekick), _timeBeforeRetrieve);
        }
    }
}
