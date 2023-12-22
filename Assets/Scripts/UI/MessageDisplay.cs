using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _contentText;
    [SerializeField] private Image _icon;
    [SerializeField] private UIEventsPublisher _uiEvents;
    [SerializeField] private Animator _animator;
    private int _disappearingHash = Animator.StringToHash("Disappearing");

    public void InitMessage(Message messageInfo, string senderName)
    {
        _nameText.text = senderName;
        _contentText.text = messageInfo.Content;
        _icon.sprite = messageInfo.Icon;
    }

    public void FadeOutMessage()
    {
        _animator.SetBool(_disappearingHash, true);
    } 


    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    public void FadeOutDone()
    {
        _uiEvents.MessadeFadeOutFinished.Invoke();
    }
    
}
