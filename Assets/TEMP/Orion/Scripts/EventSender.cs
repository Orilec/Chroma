using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSender : MonoBehaviour
{
    public EventPublisher eventPublisher;

    private void Start()
    {
        eventPublisher.Event.Invoke();
    }
}
