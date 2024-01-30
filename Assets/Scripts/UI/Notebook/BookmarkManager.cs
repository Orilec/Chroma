using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BookmarkManager : MonoBehaviour
{
    [SerializeField] private List<Bookmark> _bookmarks;
    [SerializeField] private FlippingNotebook _notebook;
    [SerializeField] private InputReader _input;
    [SerializeField] private UIEventsPublisher _uiEvents;
    [SerializeField] private float _bookmarkAnimationOffset = 50f;
    [SerializeField] private float _bookmarkAnimationTime = 0.1f;

    private Bookmark _currentCategory;
    private int _currentIndex;
    private bool _clickWasPressedLastFrame, _nextCatWasPressedLastFrame, _previousCatWasPressedLastFrame;
    
    public float BookmarkAnimationOffset { get { return _bookmarkAnimationOffset; } }
    public float BookmarkAnimationTime { get { return _bookmarkAnimationTime; } }

    private void Awake()
    {
        _uiEvents.NotebookFlipped.AddListener(OnPageFlipped);
    }

    private void Start()
    {
        _currentCategory = _bookmarks[0];
        _currentIndex = 0;
        _currentCategory.SetAsCurrentCategory(true);
    }

    private void Update()
    {
        if (_input.ClickIsPressed && _input.IsKeyboardMouse && EventSystem.current.currentSelectedGameObject != null && !_clickWasPressedLastFrame && !_notebook.IsFlipping)
        {
            OnBookMarkClicked();
        }
        else if (_input.PreviousSectionIsPressed && !_previousCatWasPressedLastFrame && !_notebook.IsFlipping)
        {
            HandleCardSelection(-1);
        }
        else if (_input.NextSectionIsPressed && !_nextCatWasPressedLastFrame && !_notebook.IsFlipping)
        {
            HandleCardSelection(1);
        }

        _clickWasPressedLastFrame = _input.ClickIsPressed;
        _previousCatWasPressedLastFrame = _input.PreviousSectionIsPressed;
        _nextCatWasPressedLastFrame = _input.NextSectionIsPressed;

    }

    private void OnBookMarkClicked()
    {
        var bookmark = EventSystem.current.currentSelectedGameObject.GetComponent<Bookmark>();
        if(bookmark != null) SetNewCategory(bookmark);
            
        for (int i = 0; i < _bookmarks.Count; i++)
        {
            if (bookmark == _bookmarks[i])
            {
                _currentIndex = i;
                return;
            }
        }
    }
    
    private void HandleCardSelection(int addition)
    {
        var newIndex = _currentIndex + addition;
        newIndex = Mathf.Clamp(newIndex, 0, _bookmarks.Count - 1);
        if (_currentCategory != _bookmarks[newIndex])
        {
            SetNewCategory(_bookmarks[newIndex]);
            _currentIndex = newIndex;
        }
        
        _currentCategory._bookmarkButton.onClick.Invoke();
    }

    private void OnPageFlipped(bool startOfBook, bool endOfBook)
    {
        if (startOfBook)
        {
            SetNewCategory(_bookmarks[0]);
            _currentIndex = 0;
        }
        else if (endOfBook)
        {
            SetNewCategory(_bookmarks[2]);
            _currentIndex = 2;
        }
        else
        {
            SetNewCategory(_bookmarks[1]);
            _currentIndex = 1;
        }
    }

    private void SetNewCategory(Bookmark newCat)
    {
        _currentCategory.SetAsCurrentCategory(false);
        _currentCategory = newCat;
        _currentCategory.SetAsCurrentCategory(true);
    }
}
