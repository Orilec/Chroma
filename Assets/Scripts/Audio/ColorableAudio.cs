using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorableAudio : MonoBehaviour
{
    [SerializeField] private InteractorEvent _interactorEvent;
    [SerializeField] private AK.Wwise.Event _colorableDeployEvent;

    private void Start()
    {
        _interactorEvent.OnColorationFinished += DeployAudio;
    }

    private void DeployAudio(InteractorEvent interactorEvent, InteractorEventArgs args)
    {
        _colorableDeployEvent.Post(this.gameObject);
    }
}
