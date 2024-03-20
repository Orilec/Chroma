using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class KioskObject : ColorableObject
{
    [Header("KioskObject")]

    [SerializeField] private NarrativeEventsPublisher _narrativeEvents;
    [SerializeField] private ColorableObject _appearObject;
    [SerializeField] private GameObject _disappearObject;
    [SerializeField] private float _delayBeforeAppear;
    [SerializeField] private Trail _associatedTrail;
    private bool _isActivated;



    public float DelayBeforeAppear { get { return _delayBeforeAppear; } }

    public override void SetObjectActive()
    {
        if (_associatedTrail != null)
        {
            _associatedTrail.SetTrailActive(); 
        }


        base.SetObjectActive();
        Invoke(nameof(SetAppearObjectActive), _delayBeforeAppear);
    }

    private void SetAppearObjectActive()
    {
        _appearObject.SetObjectActive();
        _isActivated = true;
        _narrativeEvents.KioskObjectColored.Invoke();
    }

    void OnDrawGizmosSelected()
    {

        // Draw a line between appear and disappear objects
        Gizmos.color = new Color(1, 0, 0, 1);
        Gizmos.DrawLine(_disappearObject.transform.position, _appearObject.transform.position);

    }
}
