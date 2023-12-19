using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDisplay : MonoBehaviour
{
    [SerializeField] private NarrativeEventsPublisher _narrativeEvents;

    private Queue<Message> _messages;

    private void Awake()
    {
        _messages = new Queue<Message>();
        _narrativeEvents.TriggerDialogue.AddListener(StartDialogue);
    }

    private void StartDialogue(Message[] messages)
    {
        _messages.Clear();
        foreach (Message message in messages)
        {
            _messages.Enqueue(message);
        }
        
        SendNextMessage();
    }

    private void SendNextMessage()
    {
        if (_messages.Count == 0)
        {
            EndDialogue();
            return;
        }

        var currentMessage = _messages.Dequeue();
    }

    private void EndDialogue()
    {
        
    }
    
    
    
    
}
