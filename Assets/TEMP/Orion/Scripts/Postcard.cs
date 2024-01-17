using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Postcard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;

    private PostcardManager _postcardManager;
    private Vector3 _startPos;
    
    public void InitCard(Sprite sprite, PostcardManager postcardManager)
    {
        _startPos = _rectTransform.position;
        _postcardManager = postcardManager;
        _image.enabled = true;
        _button.enabled = true;
        //_image.sprite = sprite;
    }

    private IEnumerator MoveCard(bool selected)
    {
        Vector3 endPos;
        float elapsedTime = 0f;
        
        if (selected) endPos = _startPos - new Vector3(_postcardManager.PostcardAnimationOffset, 0, 0);
        else endPos = _startPos;
        
        while (elapsedTime < _postcardManager.PostcardAnimationTime)
        {
            elapsedTime += Time.deltaTime;

            Vector3 lerpedPos = Vector3.Lerp(_rectTransform.position, endPos,
                elapsedTime / _postcardManager.PostcardAnimationTime);

            _rectTransform.position = lerpedPos;
            yield return null;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        eventData.selectedObject = this.gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.selectedObject = null;
    }

    public void OnSelect(BaseEventData eventData)
    {
        StartCoroutine(MoveCard(true));
        _postcardManager.SelectedPostcard = this;
        _postcardManager.LastSelectedPostcard = this;
        
        for (int i = 0; i < _postcardManager.Postcards.Count; i++)
        {
            if (_postcardManager.Postcards[i] == this)
            {
                _postcardManager.LastSelectedIndex = i;
                return;
            }
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        StartCoroutine(MoveCard(false));
        _postcardManager.SelectedPostcard = null;
    }


}
