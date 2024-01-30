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

    private Queue<Message> _messagesToSpawn;
    private Queue<MessageDisplay> _messagesOnScreen;
    private Queue<MessageDisplay> _messagesSpawned;
    private IEnumerator _waitCoroutine;

    private bool _dialogueEnded;

    private void Awake()
    {
        _waitCoroutine = WaitBeforeNextMessage(null);
        _uiEvents.MessageFadeOutFinished.AddListener(DestroySpawnedMessages);
        _messagesToSpawn = new Queue<Message>();
        _messagesOnScreen = new Queue<MessageDisplay>();
        _messagesSpawned = new Queue<MessageDisplay>();
        _narrativeEvents.TriggerDialogue.AddListener(StartDialogue);
    }

    private void StartDialogue(Message[] messages)
    {
        if (_messagesToSpawn.Count > 0)
        {
            StopCoroutine(_waitCoroutine);
            EndDialogue();
        }
        _messagesToSpawn.Clear();
        _messagesOnScreen.Clear();
        
        foreach (Message message in messages)
        {
            _messagesToSpawn.Enqueue(message);
        }
        
        SendNextMessage();
    }

    private void SendNextMessage()
    {
        if (_messagesToSpawn.Count == 0)
        {
            EndDialogue();
            return;
        }
        else
        {
            _dialogueEnded = false;
        }
        var currentMessage = _messagesToSpawn.Dequeue();
        SpawnMessage(currentMessage);
        _waitCoroutine = WaitBeforeNextMessage(currentMessage);
        StartCoroutine(_waitCoroutine);
    }

    private void SpawnMessage(Message messageToSpawn)
    {
        _messagesContainer.GetComponent<VerticalLayoutGroup>().enabled = false;
        _messagesContainer.GetComponent<VerticalLayoutGroup>().enabled = true;
        
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
