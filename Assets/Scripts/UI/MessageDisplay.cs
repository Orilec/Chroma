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
    [SerializeField] private Animator _animator;
    private int _disappearingHash = Animator.StringToHash("Disappearing");

    public void InitMessage(Message messageInfo, string senderName)
    {
        _nameText.text = senderName;
        _contentText.text = messageInfo.Content;
        _icon.sprite = messageInfo.Icon;
        RefreshLayout();
    }

    public void FadeOutMessage()
    {
        _animator.SetBool(_disappearingHash, true);
    } 


    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
    
    private void RefreshLayout()
    {
        for (var i = 0; i < transform.childCount; i++)
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform.GetChild(i).GetComponent<RectTransform>());
    }
}
