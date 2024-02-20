using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Bookmark : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button _bookmarkButton;
    
    [SerializeField] private BookmarkManager _bookmarkManager;
    [SerializeField] private RectTransform _rectTransform;

    private Vector3 _startPos;
    private bool _currentCategory;
    
    private void Start()
    {
        _startPos = _rectTransform.anchoredPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        eventData.selectedObject = this.gameObject;
        StartCoroutine(MoveCard(true));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!_currentCategory) StartCoroutine(MoveCard(false));
    }

    public void SetAsCurrentCategory(bool current)
    {
        StartCoroutine(MoveCard(current));
        _currentCategory = current;
    }
    
    private IEnumerator MoveCard(bool selected)
    {
        Vector3 endPos;
        float elapsedTime = 0f;
        
        if (selected) endPos = _startPos - new Vector3(_bookmarkManager.BookmarkAnimationOffset, 0, 0);
        else endPos = _startPos;
        
        while (elapsedTime < _bookmarkManager.BookmarkAnimationTime)
        {
            elapsedTime += Time.unscaledDeltaTime;

            Vector3 lerpedPos = Vector3.Lerp(_rectTransform.anchoredPosition, endPos,
                elapsedTime / _bookmarkManager.BookmarkAnimationTime);

            _rectTransform.anchoredPosition = lerpedPos;
            yield return null;
        }
    }
}
