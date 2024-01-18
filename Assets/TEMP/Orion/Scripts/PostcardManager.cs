using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PostcardManager : MonoBehaviour
{
    [SerializeField] private List<Postcard> _postcards;
    [SerializeField] private Postcard _postcardPrefab;
    [SerializeField] private Transform _postcardContainer;
    [SerializeField] private PlayerEventsPublisher _playerEvents;
    [SerializeField] private float _postcardAnimationOffset = 50f;
    [SerializeField] private float _postcardAnimationTime = 0.1f;
    
    private Postcard _selectedPostcard;
    private int _currentIndex = 0;
    
    public float PostcardAnimationOffset { get { return _postcardAnimationOffset; } }
    public float PostcardAnimationTime { get { return _postcardAnimationTime; } }
    public Postcard SelectedPostcard { get { return _selectedPostcard; } set { _selectedPostcard = value; } }

    private void Awake()
    {
        _playerEvents.CardCollected.AddListener(AddNewPostCard);
    }

    private void AddNewPostCard(Sprite sprite)
    {
        if (_currentIndex < _postcards.Count)
        {
            _postcards[_currentIndex].InitCard(sprite, this);
            _currentIndex++;
        }
    }
}
