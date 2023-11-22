using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListener : MonoBehaviour
{
    public EventPublisher eventPublisher;

    private void OnEnable()
    {
        eventPublisher.Event.AddListener(OnEventReceived);
    }

    private void OnEventReceived()
    {
        Debug.Log("Event received");
    }
}
