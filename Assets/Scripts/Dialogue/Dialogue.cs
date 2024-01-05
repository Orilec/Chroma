using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Dialogue : ScriptableObject
{
    [SerializeField] private Message[] _messages;
    public Message[] Messages { get { return _messages; } }
}


[System.Serializable]
public class Message
{
    [SerializeField] private bool _fromSidekick;
    [SerializeField] private Sprite _icon;
    [SerializeField][TextArea(3,10)] private string _content;
    [SerializeField] private float _timeOnScreen;
    
    public bool FromSidekick { get { return _fromSidekick; } }
    public Sprite Icon { get { return _icon; } }
    public string Content { get { return _content; } }
    public float TimeOnScreen { get { return _timeOnScreen; } }
    
}
