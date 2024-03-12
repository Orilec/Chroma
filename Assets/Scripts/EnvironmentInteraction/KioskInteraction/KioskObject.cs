using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class KioskObject : ColorableObject
{
    [SerializeField] private NarrativeEventsPublisher _narrativeEvents;
    [SerializeField] private ColorableObject _appearObject;
    [SerializeField] private float _delayBeforeAppear;
    private bool _isActivated;

    public override void SetObjectActive()
    {
        base.SetObjectActive();
        Invoke(nameof(SetAppearObjectActive), _delayBeforeAppear);
    }

    private void SetAppearObjectActive()
    {
        _appearObject.SetObjectActive();
        _isActivated = true;
        _narrativeEvents.KioskObjectColored.Invoke();
    }
}
