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

    private PostcardManager _postcardManager;
    private Vector3 _startPos;
    
    public void InitCard(Sprite sprite, PostcardManager postcardManager)
    {
        _startPos = _rectTransform.position;
        _postcardManager = postcardManager;
        _image.enabled = true;
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
        _postcardManager.SelectedPostcard = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.selectedObject = null;
        _postcardManager.SelectedPostcard = null;
    }

    public void OnSelect(BaseEventData eventData)
    {
        StartCoroutine(MoveCard(true));
    }

    public void OnDeselect(BaseEventData eventData)
    {
        StartCoroutine(MoveCard(false));
    }
}
