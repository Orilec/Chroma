using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDisplay : MonoBehaviour
{
    [SerializeField] private NarrativeEventsPublisher _narrativeEvents;
    [SerializeField] private MessageDisplay _messagePrefab;
    [SerializeField] private MessageDisplay _messageFromSidekickPrefab;
    [SerializeField] private RectTransform _messagesContainer;
    [SerializeField] private UIEventsPublisher _uiEvents;
    
    [SerializeField] private string _playerName;
    [SerializeField] private string _sidekickName;
    
    [SerializeField] private int _maxMessagesOnScreen;

    private Queue<Message> _messages;
    private Queue<MessageDisplay> _messagesOnScreen;
    private Queue<MessageDisplay> _messagesSpawned;

    private bool _dialogueEnded;

    private void Awake()
    {
        _uiEvents.MessadeFadeOutFinished.AddListener(DestroySpawnedMessages);
        _messages = new Queue<Message>();
        _messagesOnScreen = new Queue<MessageDisplay>();
        _messagesSpawned = new Queue<MessageDisplay>();
        _narrativeEvents.TriggerDialogue.AddListener(StartDialogue);
    }

    private void StartDialogue(Message[] messages)
    {
        _dialogueEnded = false;
        _messages.Clear();
        _messagesOnScreen.Clear();
        _messagesSpawned.Clear();
        
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
        SpawnMessage(currentMessage);
        StartCoroutine(WaitBeforeNextMessage(currentMessage));
    }

    private void SpawnMessage(Message messageToSpawn)
    {
        MessageDisplay spawnedMessage = null;
        if (messageToSpawn.FromSidekick)
        {
            spawnedMessage = Instantiate(_messageFromSidekickPrefab, _messagesContainer);
            spawnedMessage.InitMessage(messageToSpawn, _sidekickName);
        }
        else
        {
            spawnedMessage = Instantiate(_messagePrefab, _messagesContainer);
            spawnedMessage.InitMessage(messageToSpawn, _playerName);
        }
        
        _messagesOnScreen.Enqueue(spawnedMessage);
        _messagesSpawned.Enqueue(spawnedMessage);

        if (_messagesOnScreen.Count > _maxMessagesOnScreen)
        {
            MessageDisplay messageToDestroy = _messagesOnScreen.Dequeue();
            messageToDestroy.FadeOutMessage();
        }
    }

    private IEnumerator WaitBeforeNextMessage(Message sentMessage)
    {
        yield return new WaitForSecondsRealtime(sentMessage.TimeOnScreen);
        SendNextMessage();
    }
    
    private void EndDialogue()
    {
        foreach (var message in _messagesOnScreen)
        {
            message.FadeOutMessage();
        }

        _dialogueEnded = true;
    }

    private void DestroySpawnedMessages()
    {
        if (_dialogueEnded)
        {
            foreach (var message in _messagesSpawned)
            {
                message.DestroyGameObject();
            }
        }
    }
    
}
