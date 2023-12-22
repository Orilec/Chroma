using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private NarrativeEventsPublisher _narrativeEvents;
    [SerializeField] private Dialogue _dialogue;

    private bool _dialogueTriggered;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null && !_dialogueTriggered)
        {
            _dialogueTriggered = true;
            _narrativeEvents.TriggerDialogue.Invoke(_dialogue.Messages);
        }
    }
}
