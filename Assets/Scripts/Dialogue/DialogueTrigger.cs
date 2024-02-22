using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private NarrativeEventsPublisher _narrativeEvents;
    [SerializeField] private Dialogue _dialogue;

    private bool _dialogueTriggered;

    private BoxCollider boxCollider;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null && !_dialogueTriggered)
        {
            _dialogueTriggered = true;
            _narrativeEvents.TriggerDialogue.Invoke(_dialogue.Messages);
        }
    }

    private void OnDrawGizmos()
    {
        boxCollider = GetComponent<BoxCollider>();
        // Green
        Gizmos.color = new Color(0.58f, 1.0f, 0.82f, 0.5f);
        Gizmos.DrawWireCube(boxCollider.bounds.center, boxCollider.bounds.size);
        Gizmos.DrawCube(boxCollider.bounds.center, boxCollider.bounds.size);
    }


}
